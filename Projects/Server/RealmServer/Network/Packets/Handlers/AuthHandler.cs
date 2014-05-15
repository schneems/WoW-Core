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

            authChallenge.Write<ushort>(0);
            authChallenge.Write<byte>(1);     // DosZeroBits

            for (int i = 0; i < 8; i++)
                authChallenge.Write<uint>(0); // DosChallenge

            authChallenge.Write<uint>(0);     // Challenge

            session.Send(authChallenge);
        }

        [Message(ClientMessages.AuthSession)]
        public static void OnAuthSession(Packet packet, RealmSession session)
        {
            var digest = new byte[20];

            packet.Push(out uint realmId);
            packet.Push(out digest[14]);
            packet.Push(out uint localChallenge);
            packet.Push(out digest[0]);
            packet.Push(out digest[6]);
            packet.Push(out digest[2]);
            packet.Push(out digest[15]);
            packet.Push(out digest[9]);
            packet.Push(out digest[8]);
            packet.Push(out digest[19]);
            packet.Push(out digest[17]);
            packet.Push(out sbyte loginServerType);
            packet.Push(out digest[1]);
            packet.Push(out digest[3]);
            packet.Push(out digest[12]);
            packet.Push(out digest[10]);
            packet.Push(out digest[4]);
            packet.Push(out digest[7]);
            packet.Push(out short build);
            packet.Push(out ulong dosResponse);
            packet.Push(out digest[11]);
            packet.Push(out digest[13]);
            packet.Push(out sbyte buildType);
            packet.Push(out digest[18]);
            packet.Push(out uint loginServerId);
            packet.Push(out uint siteId);
            packet.Push(out uint regionId);
            packet.Push(out digest[16]);
            packet.Push(out digest[15]);

            // AddonInfo stuff
            packet.Push(out int packedAddonSize);
            packet.Push(out int unpackedAddonSize);
            packet.Push(out ushort unknown);

            packet.PushBytes(out var packedAddonData, packedAddonSize - 4);
            packet.PushDynamicString(out var account, 11);

            //TODO Implement security checks & field handling.
            //TODO Implement AuthResponse.

            AddonHandler.LoadAddonInfoData(session, packedAddonData, packedAddonSize, unpackedAddonSize);
        }

        public static void HandleAuthResponse(RealmSession session)
        {

        }
    }
}
