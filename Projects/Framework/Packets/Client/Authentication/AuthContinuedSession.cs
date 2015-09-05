// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Client.Authentication
{
    public class AuthContinuedSession : ClientPacket
    {
        public ulong DosResponse { get; set; }
        public ulong Key         { get; set; }
        public byte[] Digest     { get; set; }

        public override void Read()
        {
            DosResponse = Packet.Read<ulong>();
            Key         = Packet.Read<ulong>();
            Digest      = Packet.ReadBytes(20);
        }
    }
}
