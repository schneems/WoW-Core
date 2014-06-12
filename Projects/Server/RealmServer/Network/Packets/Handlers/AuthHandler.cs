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
using Framework.Database;
using Framework.Misc;
using Framework.Network.Packets;
using RealmServer.Attributes;
using RealmServer.Constants.Net;

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
                authChallenge.Write<uint>(0); // DosChallenge

            authChallenge.Write<byte>(1);     // DosZeroBits

            session.Send(authChallenge);
        }

        [Message(ClientMessages.AuthSession)]
        public static void OnAuthSession(Packet packet, RealmSession session)
        {
            // Part of the header
            packet.Read<ushort>();

            var loginServerId   = packet.Read<uint>();
            var build           = packet.Read<short>();
            var localChallenge  = packet.Read<uint>();
            var siteId          = packet.Read<uint>();
            var realmId         = packet.Read<uint>();
            var loginServerType = packet.Read<sbyte>();
            var buildType       = packet.Read<sbyte>();
            var regionId        = packet.Read<uint>();
            var dosResponse     = packet.Read<ulong>();
            var digest          = packet.ReadBytes(20);

            var accountName = packet.ReadString(11);
            var useIPv6 = packet.GetBit();

            // AddonInfo stuff
            var compressedAddonInfoSize   = packet.Read<int>();
            var uncompressedAddonInfoSize = packet.Read<int>();
            var compressedAddonData       = packet.ReadBytes(compressedAddonInfoSize - 4);

            var accountParts = accountName.Split(new[] { '#' });

            if (accountParts.Length == 2)
            {
                var accountId = int.Parse(accountParts[0]);
                var gameIndex = byte.Parse(accountParts[1]);

                var gameAccount = DB.Auth.GameAccounts.SingleOrDefault(ga => ga.AccountId == accountId && ga.Index == gameIndex);

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
                        AddonHandler.LoadAddonInfoData(session, compressedAddonData, compressedAddonInfoSize, uncompressedAddonInfoSize);
                    }
                }
            }

            //TODO Implement security checks & field handling.
            //TODO Implement AuthResponse.
        }

        public static void HandleAuthResponse(RealmSession session)
        {
        }
    }
}