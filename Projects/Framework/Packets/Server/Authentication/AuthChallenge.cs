// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Net;
using Framework.Network.Packets;

namespace Framework.Packets.Server.Authentication
{
    public class AuthChallenge : ServerPacket
    {
        public uint Challenge      { get; set; }
        public byte[] DosChallenge { get; set; } = new byte[32];
        public byte DosZeroBits    { get; set; } = 1;

        public AuthChallenge() : base(GlobalServerMessage.AuthChallenge, true) { }

        public override void Write()
        {
            Packet.Write(Challenge);
            Packet.WriteBytes(DosChallenge);
            Packet.Write(DosZeroBits);
        }
    }
}
