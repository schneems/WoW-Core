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
    class AddonInfoData : IServerStruct
    {
        public byte Status       { get; set; } = 2;
        public bool InfoProvided { get; set; }
        public bool KeyProvided  { get; set; }
        public bool UrlProvided  { get; set; }
        public byte KeyVersion   { get; set; } = 1;
        public uint Revision     { get; set; }
        public string Url        { get; set; }
        public byte[] KeyData    { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(Status);
            packet.PutBit(InfoProvided);
            packet.PutBit(KeyProvided);
            packet.PutBit(UrlProvided);

            if (UrlProvided)
            {
                packet.PutBits(Url.Length, 8);
                packet.Flush();

                packet.Write(Url);
            }

            packet.Flush();

            if (InfoProvided)
            {
                packet.Write(KeyVersion);
                packet.Write(Revision);
            }

            if (KeyProvided)
                packet.Write(KeyData);
        }
    }
}
