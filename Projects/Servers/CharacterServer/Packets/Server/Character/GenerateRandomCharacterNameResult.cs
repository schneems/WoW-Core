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

using CharacterServer.Constants.Net;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Character
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
            Packet.Flush();

            Packet.Write(Name);
        }
    }
}
