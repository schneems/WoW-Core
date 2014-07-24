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
using Framework.Cryptography;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Misc;
using Framework.Network.Packets;
using RealmServer.Attributes;
using RealmServer.Constants.Authentication;
using RealmServer.Constants.Net;
using RealmServer.Managers;

namespace RealmServer.Network.Packets.Handlers
{
    class AuthHandler
    {
        public static void HandleAuthChallenge(RealmSession session)
        {
            var authChallenge = new Packet(ServerMessages.AuthChallenge);

            // Part of the header
            authChallenge.Write<ushort>(0);

            authChallenge.Write(session.Challenge);

            for (int i = 0; i < 8; i++)
                authChallenge.Write<uint>(0);

            authChallenge.Write<byte>(1);

            session.Send(authChallenge);
        }

        [Message(ClientMessages.AuthSession)]
        public static void OnAuthSession(Packet packet, RealmSession session)
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
            var authResult = AuthResults.Ok;

            session.Realm = DB.Auth.Single<Realm>(r => r.Id == realmId);

            if (loginServerType != LoginServerTypes.Battlenet || session.Realm == null)
                authResult = AuthResults.Reject;

            if (authResult == AuthResults.Ok)
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

                                AddonHandler.LoadAddonInfoData(session, compressedAddonData, compressedAddonInfoSize, uncompressedAddonInfoSize);
                            }
                            else
                                authResult = AuthResults.Failed;
                        }
                        else
                            authResult = AuthResults.UnknownAccount;
                    }
                    else
                        authResult = AuthResults.UnknownAccount;
                }
                else
                    authResult = AuthResults.UnknownAccount;
            }

            HandleAuthResponse(authResult, session);

            //TODO [partially done] Implement security checks & field handling.
            //TODO [partially done] Implement AuthResponse.
        }

        public static void HandleAuthResponse(AuthResults result, RealmSession session)
        {
            var gameAccount = session.GameAccount;
            var realm = session.Realm;

            var authResponse = new Packet(ServerMessages.AuthResponse);

            var hasSuccessInfo = result == AuthResults.Ok;
            var hasWaitInfo    = result == AuthResults.WaitQueue;

            authResponse.Write(result);

            authResponse.PutBit(hasSuccessInfo);
            authResponse.PutBit(hasWaitInfo);
            authResponse.Flush();

            if (hasSuccessInfo)
            {
                var allowedRaces   = Manager.GameAccountMgr.GetAvailableRaces(gameAccount, realm);
                var allowedClasses = Manager.GameAccountMgr.GetAvailableClasses(gameAccount, realm);
                var charTemplates  = Manager.GameAccountMgr.GetAvailableCharacterTemplates(gameAccount, realm);

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
                        authResponse.Write((byte)c.ClassId);
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
