// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
