// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using CharacterServer.Constants.Net;
using CharacterServer.Packets.Structures.Character;
using CharacterServer.Packets.Structures.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Character
{
    class EnumCharactersResult : ServerPacket
    {
        public bool Success             { get; set; } = true;
        public bool IsDeletedCharacters { get; set; } = false;
        public List<CharacterListEntry> Characters { get; set; } = new List<CharacterListEntry>();
        public List<RestrictedFactionChangeRule> FactionChangeRestrictions { get; set; } = new List<RestrictedFactionChangeRule>();

        public EnumCharactersResult() : base(ServerMessage.EnumCharactersResult) { }

        public override void Write()
        {
            Packet.PutBit(Success);
            Packet.PutBit(IsDeletedCharacters);
            Packet.FlushBits();

            Packet.Write(Characters.Count);
            Packet.Write(FactionChangeRestrictions.Count);

            Characters.ForEach(c => c.Write(Packet));
            FactionChangeRestrictions.ForEach(fcr => fcr.Write(Packet));
        }
    }
}
