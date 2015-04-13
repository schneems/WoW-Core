// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using CharacterServer.Constants.Authentication;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.Packets.Client.Authentication;
using CharacterServer.Packets.Server.Authentication;
using CharacterServer.Packets.Server.Misc;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Cryptography;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace CharacterServer.Packets.Handlers
{
    class AuthHandler
    {
        [GlobalMessage(GlobalClientMessage.AuthSession, SessionState.Initiated)]
        public static async void AuthSessionHandler(AuthSession authSession, CharacterSession session)
        {
            var accountParts = authSession.Account.Split(new[] { '#' });
            var authResult = AuthResult.Ok;

            if (accountParts.Length == 2)
            {
                var accountId = int.Parse(accountParts[0]);
                var gameIndex = byte.Parse(accountParts[1]);

                session.Account = DB.Auth.Single<Account>(a => a.Id == accountId);

                if (session.Account != null)
                    session.GameAccount = session.Account.GameAccounts.SingleOrDefault(ga => ga.Index == gameIndex);

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
                if (!sha1.Digest.Compare(authSession.Digest))
                    authResult = AuthResult.Failed;
            }

            var authResponse = new AuthResponse
            {
                Result = authResult,
                HasSuccessInfo = authResult == AuthResult.Ok,
            };

            if (authResponse.HasSuccessInfo)
            {
                session.State = SessionState.Authenticated;

                var addonData = AddonHandler.GetAddonInfoData(session, authSession.AddonInfo, authSession.CompressedAddonInfoSize, authSession.UncompressedAddonInfoSize);

                if (addonData != null && addonData.Length != authSession.UncompressedAddonInfoSize)
                {
                    Log.Error("Addon Info data size mismatch.");

                    session.Dispose();

                    return;
                }

                authResponse.SuccessInfo.ActiveExpansionLevel = session.GameAccount.BoxLevel;
                authResponse.SuccessInfo.AccountExpansionLevel = session.GameAccount.BoxLevel;
                authResponse.SuccessInfo.AvailableRaces = Manager.GameAccount.GetAvailableRaces(session.GameAccount, session.Realm);
                authResponse.SuccessInfo.AvailableClasses = Manager.GameAccount.GetAvailableClasses(session.GameAccount, session.Realm);
                authResponse.SuccessInfo.Templates = Manager.GameAccount.GetAvailableCharacterTemplates(session.GameAccount, session.Realm);

                await session.Send(authResponse);

                AddonHandler.HandleAddonInfo(session, addonData);

                await session.Send(new TutorialFlags());
            }
            else
                await session.Send(authResponse);
        }
    }
}
