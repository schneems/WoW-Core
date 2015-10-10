// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WorldServer.Constants.Net;
using Framework.Network.Packets;

namespace WorldServer.Packets.Server.Character
{
    class GenerateRandomCharacterNameResult : ServerPacket
    {
        public bool Success { get; set; }
        public string Name  { get; set; }

        public GenerateRandomCharacterNameResult() : base(ServerMessage.GenerateRandomCharacterNameResult) { }

        public override void Write()
        {
            Packet.PutBit(Success);
            Packet.PutBits(Name.Length, 6);
            Packet.FlushBits();

            Packet.WriteString(Name);
        }
    }
}
