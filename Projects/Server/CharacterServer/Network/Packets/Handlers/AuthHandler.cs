/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Linq;
using CharacterServer.Constants.Authentication;
using CharacterServer.Managers;
using CharacterServer.Packets.Client.Authentication;
using CharacterServer.Packets.Server.Authentication;
using Framework.Attributes;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Cryptography;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace CharacterServer.Network.Packets.Handlers
{
    class AuthHandler
    {
        [GlobalMessage(GlobalClientMessage.AuthSession)]
        public static void AuthSessionHandler(AuthSession authSession, CharacterSession session)
        {
            var accountParts = authSession.Account.Split(new[] { '#' });
            var authResult = AuthResult.Ok;

            if (accountParts.Length == 2)
            {
                var accountId = int.Parse(accountParts[0]);
                var gameIndex = byte.Parse(accountParts[1]);

                var account = DB.Auth.Single<Account>(a => a.Id == accountId);

                if (account != null)
                    session.GameAccount = account.GameAccounts.SingleOrDefault(ga => ga.Index == gameIndex);

                if (session.GameAccount != null)
                    session.Crypt = new WoWCrypt(session.GameAccount.SessionKey.ToByteArray());
                else
                    authResult = AuthResult.Failed;
            }

            session.Realm = DB.Auth.Single<Realm>(r => r.Id == authSession.RealmID);

            if (authSession.LoginServerType != (sbyte)LoginServerTypes.Battlenet || session.Realm == null)
                authResult = AuthResult.Reject;

            if (authResult == AuthResult.Ok)
            {
                var sha1 = new Sha1();

                sha1.Process(authSession.Account);
                sha1.Process(0u);
                sha1.Process(authSession.LocalChallenge);
                sha1.Process(session.Challenge);
                sha1.Finish(session.GameAccount.SessionKey.ToByteArray(), 40);

                // Check the password digest.
                if (sha1.Digest.Compare(authSession.Digest))
                    session.GameAccount = session.GameAccount;
                else
                    authResult = AuthResult.Failed;
            }

            var authResponse = new AuthResponse
            {
                Result = authResult,
                HasSuccessInfo = authResult == AuthResult.Ok,
            };

            if (authResponse.HasSuccessInfo)
            {
                var addonData = AddonHandler.GetAddonInfoData(session, authSession.AddonInfo, authSession.CompressedAddonInfoSize, authSession.UncompressedAddonInfoSize);

                if (addonData != null && addonData.Length != authSession.UncompressedAddonInfoSize)
                {
                    Log.Message(LogType.Error, "Addon Info data size mismatch.");

                    session.Dispose();

                    return;
                }

                authResponse.SuccessInfo.AvailableRaces   = Manager.GameAccount.GetAvailableRaces(session.GameAccount, session.Realm);
                authResponse.SuccessInfo.AvailableClasses = Manager.GameAccount.GetAvailableClasses(session.GameAccount, session.Realm);
                authResponse.SuccessInfo.Templates        = Manager.GameAccount.GetAvailableCharacterTemplates(session.GameAccount, session.Realm);

                session.Send(authResponse);

                AddonHandler.HandleAddonInfo(session, addonData);
            }
            else
                session.Send(authResponse);
        }
    }
}
