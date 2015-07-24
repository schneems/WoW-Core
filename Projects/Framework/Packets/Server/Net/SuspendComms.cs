// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Net;
using Framework.Network.Packets;

namespace Framework.Packets.Server.Net
{
    public class SuspendComms : ServerPacket
    {
        public uint Serial { get; set; }

        public SuspendComms() : base(GlobalServerMessage.SuspendComms) { }

        public override void Write()
        {
            Packet.Write(Serial);
        }
    }
}
