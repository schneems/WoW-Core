// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets
{
    public abstract class ClientPacket
    {
        public AuthPacket Packet { protected get; set; }
        public bool IsReadComplete { get { return Packet.IsReadComplete; } }

        public abstract void Read();
    }
}
