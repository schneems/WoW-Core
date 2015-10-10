// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WorldServer.Constants.Authentication;
using WorldServer.Constants.Net;
using WorldServer.Packets.Structures.Authentication;
using Framework.Network.Packets;

namespace WorldServer.Packets.Server.Authentication
{
    public class AuthResponse : ServerPacket
    {
        public AuthResult Result           { get; set; }
        public bool HasSuccessInfo         { get; set; }
        public bool HasWaitInfo            { get; set; }
        public AuthSuccessInfo SuccessInfo { get; set; } = new AuthSuccessInfo();
        public AuthWaitInfo WaitInfo       { get; set; } = new AuthWaitInfo();

        public AuthResponse() : base(ServerMessage.AuthResponse) { }

        public override void Write()
        {
            Packet.Write(Result);

            Packet.PutBit(HasSuccessInfo);
            Packet.PutBit(HasWaitInfo);
            Packet.FlushBits();

            if (HasSuccessInfo)
                SuccessInfo.Write(Packet);

            if (HasWaitInfo)
                WaitInfo.Write(Packet);
        }
    }
}
