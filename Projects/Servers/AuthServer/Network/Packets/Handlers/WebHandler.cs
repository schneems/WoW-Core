/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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

using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using AuthServer.Network.Sessions;
using Framework.Network.Packets;

namespace AuthServer.Network.Packets.Handlers
{
    class WebHandler
    {
        [AuthMessage(AuthClientMessage.Receive, (AuthChannel)5)]
        public static void OnHTTPReceive(AuthPacket packet, Client client)
        {
            packet.ReadString();

            var path = packet.ReadString();

            Manager.PatchMgr.Send(path, client);
        }
    }
}
