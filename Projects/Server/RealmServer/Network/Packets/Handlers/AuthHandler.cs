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

using Framework.Network.Packets;
using RealmServer.Attributes;
using RealmServer.Constants.Net;

namespace RealmServer.Network.Packets.Handlers
{
    class AuthHandler
    {
        //TODO Implement field handling.
        public static void HandleAuthChallenge(RealmSession session)
        {
            var authChallenge = new Packet(ServerMessages.AuthChallenge);

            authChallenge.Write<byte>(0);     // DosZeroBits

            for (int i = 0; i < 8; i++)
                authChallenge.Write<uint>(0); // DosChallenge

            authChallenge.Write<uint>(0);     // Challenge

            session.Send(authChallenge);
        }

        [Message(ClientMessages.AuthSession)]
        public static void OnAuthSession(Packet packet, RealmSession session)
        {
            var digest = new byte[20];

            var realmId = packet.Read<uint>();

            packet.Read(digest, 14);

            var localChallenge = packet.Read<uint>();

            packet.Read(data: digest, index: 0);
            packet.Read(data: digest, index: 6);
            packet.Read(data: digest, index: 2);
            packet.Read(data: digest, index: 15);
            packet.Read(data: digest, index: 9);
            packet.Read(data: digest, index: 8);
            packet.Read(data: digest, index: 19);
            packet.Read(data: digest, index: 17);

            var loginServerType = packet.Read<sbyte>();

            packet.Read(data: digest, index: 1);
            packet.Read(data: digest, index: 3);
            packet.Read(data: digest, index: 12);
            packet.Read(data: digest, index: 10);
            packet.Read(data: digest, index: 4);
            packet.Read(data: digest, index: 7);

            var build = packet.Read<short>();
            var dosResponse = packet.Read<ulong>();

            packet.Read(data: digest, index: 11);
            packet.Read(data: digest, index: 13);

            var buildType = packet.Read<sbyte>();

            packet.Read(data: digest, index: 18);

            var loginServerId = packet.Read<uint>();
            var siteId        = packet.Read<uint>();
            var regionId      = packet.Read<uint>();

            packet.Read(data: digest, index: 16);
            packet.Read(data: digest, index: 5);

            // AddonInfo stuff
            var packedAddonSize   = packet.Read<int>();
            var unpackedAddonSize = packet.Read<int>();
            var unknown           = packet.Read<ushort>();
            var packedAddonData   = packet.ReadBytes(packedAddonSize - 4);

            var account = packet.ReadString(11);

            //TODO Implement security checks & field handling.
            //TODO Implement AuthResponse.

            AddonHandler.LoadAddonInfoData(session, packedAddonData, packedAddonSize, unpackedAddonSize);
        }

        public static void HandleAuthResponse(RealmSession session)
        {

        }
    }
}
