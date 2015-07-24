// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Client.Net
{
    public class LogDisconnect : ClientPacket
    {
        public uint Reason { get; set; }

        public override void Read()
        {
            Reason = Packet.Read<uint>();
        }
    }
}
