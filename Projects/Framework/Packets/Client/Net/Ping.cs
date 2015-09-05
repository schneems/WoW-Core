// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Client.Net
{
    public class Ping : ClientPacket
    {
        public uint Serial { get; set; }
        public uint Latency { get; set; }

        public override void Read()
        {
            Serial  = Packet.Read<uint>();
            Latency = Packet.Read<uint>();
        }
    }
}
