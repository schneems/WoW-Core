// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Net;
using Framework.Network.Packets;

namespace Framework.Packets.Server.Net
{
    public class Compression : ServerPacket
    {
        public int UncompressedSize   { get; set; }
        public uint UncompressedAdler { get; set; }
        public uint CompressedAdler   { get; set; }
        public byte[] CompressedData  { get; set; }

        public Compression() : base(GlobalServerMessage.Compression) { }

        public override void Write()
        {
            Packet.Write(UncompressedSize);
            Packet.Write(UncompressedAdler);
            Packet.Write(CompressedAdler);
            Packet.Write(CompressedData);
        }
    }
}
