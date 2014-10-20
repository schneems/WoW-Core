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
using CharacterServer.Constants.Net;
using CharacterServer.Managers;
using Framework.Attributes;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Cryptography;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Network.Packets.Handlers
{
    class AuthHandler
    {
        public static void HandleAuthChallenge(CharacterSession session)
        {
            var authChallenge = new Packet(ServerMessage.AuthChallenge);

            // Part of the header
            authChallenge.Write<ushort>(0);

            authChallenge.Write(session.Challenge);

            for (int i = 0; i < 8; i++)
                authChallenge.Write<uint>(0);

            authChallenge.Write<byte>(1);

            session.Send(authChallenge);
        }

        [GlobalMessage(GlobalClientMessage.AuthSession)]
        public static void OnAuthSession(Packet packet, CharacterSession session)
        {
            // Part of the header
            packet.Read<ushort>();

            var loginServerId   = packet.Read<int>();
            var build           = packet.Read<short>();
            var regionId        = packet.Read<uint>();
            var siteId          = packet.Read<uint>();
            var realmId         = packet.Read<uint>();
            var loginServerType = packet.Read<LoginServerTypes>();
            var buildType       = packet.Read<sbyte>();
            var localChallenge  = packet.Read<uint>();
            var dosResponse     = packet.Read<ulong>();
            var digest          = packet.ReadBytes(20);
            var accountName     = packet.ReadString(11);
            var useIPv6         = packet.GetBit();

            // AddonInfo stuff
            var compressedAddonInfoSize   = packet.Read<int>();
            var uncompressedAddonInfoSize = packet.Read<int>();
            var compressedAddonData       = packet.ReadBytes(compressedAddonInfoSize - 4);

            var accountParts = accountName.Split(new[] { '#' });
            var authResult = AuthResult.Ok;

            session.Realm = DB.Auth.Single<Realm>(r => r.Id == realmId);

            if (loginServerType != LoginServerTypes.Battlenet || session.Realm == null)
                authResult = AuthResult.Reject;

            if (authResult == AuthResult.Ok)
            {
                if (accountParts.Length == 2)
                {
                    var accountId = int.Parse(accountParts[0]);
                    var gameIndex = byte.Parse(accountParts[1]);

                    var account = DB.Auth.Single<Account>(a => a.Id == accountId);

                    if (account != null)
                    {
                        var gameAccount = account.GameAccounts.SingleOrDefault(ga => ga.Index == gameIndex);

                        if (gameAccount != null)
                        {
                            var sha1 = new Sha1();

                            sha1.Process(accountName);
                            sha1.Process(0u);
                            sha1.Process(localChallenge);
                            sha1.Process(session.Challenge);
                            sha1.Finish(gameAccount.SessionKey.ToByteArray(), 40);

                            // Check the password digest.
                            if (sha1.Digest.Compare(digest))
                            {
                                session.Crypt = new WoWCrypt(gameAccount.SessionKey.ToByteArray());
                                session.GameAccount = gameAccount;
                            }
                            else
                                authResult = AuthResult.Failed;
                        }
                        else
                            authResult = AuthResult.UnknownAccount;
                    }
                    else
                        authResult = AuthResult.UnknownAccount;
                }
                else
                    authResult = AuthResult.UnknownAccount;
            }

            HandleAuthResponse(authResult, session);

            if (authResult == AuthResult.Ok)
            {
                var addonData = AddonHandler.GetAddonInfoData(session, compressedAddonData, compressedAddonInfoSize, uncompressedAddonInfoSize);

                if (addonData != null && addonData.Length != uncompressedAddonInfoSize)
                {
                    Log.Message(LogType.Error, "Addon Info data size mismatch.");

                    session.Dispose();
                }
                else
                    AddonHandler.HandleAddonInfo(session, addonData);
            }

            //TODO [partially done] Implement security checks & field handling.
            //TODO [partially done] Implement AuthResponse.
        }

        public static void HandleAuthResponse(AuthResult result, CharacterSession session)
        {
            var gameAccount = session.GameAccount;
            var realm = session.Realm;

            var authResponse = new Packet(ServerMessage.AuthResponse);

            var hasSuccessInfo = result == AuthResult.Ok;
            var hasWaitInfo    = result == AuthResult.WaitQueue;

            authResponse.Write(result);

            authResponse.PutBit(hasSuccessInfo);
            authResponse.PutBit(hasWaitInfo);
            authResponse.Flush();

            if (hasSuccessInfo)
            {
                var allowedRaces = Manager.GameAccount.GetAvailableRaces(gameAccount, realm);
                var allowedClasses = Manager.GameAccount.GetAvailableClasses(gameAccount, realm);
                var charTemplates = Manager.GameAccount.GetAvailableCharacterTemplates(gameAccount, realm);

                authResponse.Write<uint>(0);
                authResponse.Write<uint>(0);
                authResponse.Write<uint>(0);
                authResponse.Write<uint>(0);
                authResponse.Write<uint>(0);
                authResponse.Write(gameAccount.BoxLevel);
                authResponse.Write(gameAccount.BoxLevel);
                authResponse.Write<uint>(0);
                authResponse.Write(allowedRaces.Count);
                authResponse.Write(allowedClasses.Count);
                authResponse.Write(charTemplates.Count);
                authResponse.Write<uint>(0);

                foreach (var r in allowedRaces)
                {
                    authResponse.Write(r.Key);
                    authResponse.Write(r.Value);
                }

                foreach (var c in allowedClasses)
                {
                    authResponse.Write(c.Key);
                    authResponse.Write(c.Value);
                }

                foreach (var set in charTemplates)
                { 
                    authResponse.Write(set.Id);
                    authResponse.Write(set.CharacterTemplateClasses.Count);

                    foreach (var c in set.CharacterTemplateClasses)
                    {
                        authResponse.Write(c.ClassId);
                        authResponse.Write(c.FactionGroup);
                    }

                    authResponse.PutBits(set.Name.Length, 7);
                    authResponse.PutBits(set.Description.Length, 10);

                    authResponse.Flush();

                    authResponse.Write(set.Name);
                    authResponse.Write(set.Description);
                }

                authResponse.PutBit(0);
                authResponse.PutBit(0);
                authResponse.PutBit(0);
                authResponse.PutBit(0);
                authResponse.PutBit(0);

                authResponse.Flush();
            }

            session.Send(authResponse);
        }
    }
}
