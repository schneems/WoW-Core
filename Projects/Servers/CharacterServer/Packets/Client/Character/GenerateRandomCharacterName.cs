// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Character
{
    class GenerateRandomCharacterName : ClientPacket
    {
        public Race Race { get; set; }
        public byte Sex  { get; set; }

        public override void Read()
        {
            Race = Packet.Read<Race>();
            Sex  = Packet.Read<byte>();
        }
    }
}
