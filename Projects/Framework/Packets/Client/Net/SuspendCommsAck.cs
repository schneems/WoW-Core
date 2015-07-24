// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Client.Net
{
    public class SuspendCommsAck : ClientPacket
    {
        public uint Serial    { get; set; }
        public uint Timestamp { get; set; }

        public override void Read()
        {
            Serial    = Packet.Read<uint>();
            Timestamp = Packet.Read<uint>();
        }
    }
}
