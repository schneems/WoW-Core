// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Constants.Authentication;
using CharacterServer.Constants.Net;
using CharacterServer.Packets.Structures.Authentication;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Authentication
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
