// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace WorldServer.Packets.Client.Misc
{
    class LoadingScreenNotify : ClientPacket
    {
        public int MapID    { get; set; }
        public bool Showing { get; set; }

        public override void Read()
        {
            MapID   = Packet.Read<int>();
            Showing = Packet.GetBit();
        }
    }
}
