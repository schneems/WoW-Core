// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Server.Authentication
{
    public class TransferInitiate : ServerPacket
    {
        public override void Write()
        {
            var serverToClient = "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT";

            Packet.Write((ushort)(serverToClient.Length + 1));
            Packet.WriteString(serverToClient, true);
        }
    }
}
