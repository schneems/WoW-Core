// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
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
            var nameLength     = Packet.GetBits<byte>(6);
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

            Name = Packet.ReadString(nameLength);

            if (useTemplateSet)
                TemplateSetID = Packet.Read<int>();
        }
    }
}
