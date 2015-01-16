/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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

using Framework.Constants.General;
using Framework.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Character
{
    class CreateCharacter : ClientPacket
    {
        public Race RaceID            { get; set; }
        public Class ClassID          { get; set; }
        public byte SexID             { get; set; }
        public byte SkinID            { get; set; }
        public byte FaceID            { get; set; }
        public byte HairStyleID       { get; set; }
        public byte HairColorID       { get; set; }
        public byte FacialHairStyleID { get; set; }
        public byte OutfitID          { get; set; }
        public int TemplateSetID      { get; set; }
        public string Name            { get; set; }

        public override void Read()
        {
            var nameLength     = Packet.GetBits<int>(6);
            var useTemplateSet = Packet.GetBit();
                                    
            RaceID            = Packet.Read<Race>();
            ClassID           = Packet.Read<Class>();
            SexID             = Packet.Read<byte>();
            SkinID            = Packet.Read<byte>();
            FaceID            = Packet.Read<byte>();
            HairStyleID       = Packet.Read<byte>();
            HairColorID       = Packet.Read<byte>();
            FacialHairStyleID = Packet.Read<byte>();
            OutfitID          = Packet.Read<byte>();

            Name = Packet.ReadString(nameLength).ToLowerEnd();

            if (useTemplateSet)
                TemplateSetID = Packet.Read<int>();
        }
    }
}
