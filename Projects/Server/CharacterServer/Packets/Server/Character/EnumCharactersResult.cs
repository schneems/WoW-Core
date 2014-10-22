/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using CharacterServer.Constants.Net;
using CharacterServer.Packets.Structures.Character;
using CharacterServer.Packets.Structures.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Character
{
    class EnumCharactersResult() : IServerPacket(ServerMessage.EnumCharactersResult)
    {
        public bool Success             { get; set; } = true;
        public bool IsDeletedCharacters { get; set; } = false;
        public List<CharacterListEntry> Characters { get; set; } = new List<CharacterListEntry>();
        public List<RestrictedFactionChangeRule> FactionChangeRestrictions { get; set; } = new List<RestrictedFactionChangeRule>();

        public override void Write()
        {
            Packet.Write(Success);
            Packet.Write(IsDeletedCharacters);
            Packet.Flush();

            Characters.ForEach(c => c.Write(Packet));
            FactionChangeRestrictions.ForEach(fcr => fcr.Write(Packet));
        }
    }
}
