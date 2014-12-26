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

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Misc
{
    public struct VirtualRealmNameInfo : IServerStruct
    {
        public bool IsLocal               { get; set; }
        public string RealmNameActual     { get; set; }
        public string RealmNameNormalized { get; set; }

        public void Write(Packet packet)
        {
            packet.PutBit(IsLocal);
            packet.PutBits(RealmNameActual.Length, 8);
            packet.PutBits(RealmNameNormalized.Length, 8);
            packet.Flush();

            packet.Write(RealmNameActual);
            packet.Write(RealmNameNormalized);
        }
    }
}
