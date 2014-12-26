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
    class BannedAddonInfo : IServerStruct
    {
        public int Id            { get; set; }
        public uint LastModified { get; set; }
        public int Flags         { get; set; }
        public uint[] NameMD5    { get; set; } = new uint[4];
        public uint[] VersionMD5 { get; set; } = new uint[4];

        public void Write(Packet packet)
        {
            packet.Write(Id);

            for (var i = 0; i < 4; i++)
            {
                packet.Write(NameMD5[i]);
                packet.Write(VersionMD5[i]);
            }

            packet.Write(LastModified);
            packet.Write(Flags);
        }
    }
}
