// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Framework.Constants.Object;
using Framework.Network.Packets;
using Framework.Objects;

namespace CharacterServer.Packets.Structures.Character
{
    class CharacterListEntry : IServerStruct
    {
        public SmartGuid Guid                     { get; set; }
        public string Name                        { get; set; }
        public byte ListPosition                  { get; set; }
        public Race RaceID                        { get; set; }
        public Class ClassID                      { get; set; }
        public byte SexID                         { get; set; }
        public byte SkinID                        { get; set; }
        public byte FaceID                        { get; set; }
        public byte HairStyle                     { get; set; }
        public byte HairColor                     { get; set; }
        public byte FacialHairStyle               { get; set; }
        public byte ExperienceLevel               { get; set; }
        public int ZoneID                         { get; set; }
        public int MapID                          { get; set; }
        public Vector3 PreloadPos                 { get; set; }
        public SmartGuid GuildGUID                { get; set; }
        public CharacterFlags Flags               { get; set; }
        public CharacterCustomizeFlags Flags2     { get; set; }
        public uint Flags3                        { get; set; }
        public bool FirstLogin                    { get; set; }
        public uint PetCreatureDisplayID          { get; set; }
        public uint PetExperienceLevel            { get; set; }
        public uint PetCreatureFamilyID           { get; set; }
        public bool BoostInProgress               { get; set; }
        public int[] ProfessionIDs                { get; set; } = new int[2];
        public CharacterListItem[] InventoryItems { get; set; } = new CharacterListItem[23];

        public void Write(Packet packet)
        {
            InventoryItems.Initialize();

            packet.Write(Guid);
            packet.Write(ListPosition);
            packet.Write(RaceID);
            packet.Write(ClassID);
            packet.Write(SexID);
            packet.Write(SkinID);
            packet.Write(FaceID);
            packet.Write(HairStyle);
            packet.Write(HairColor);
            packet.Write(FacialHairStyle);
            packet.Write(ExperienceLevel);
            packet.Write(ZoneID);
            packet.Write(MapID);
            packet.Write(PreloadPos);
            packet.Write(GuildGUID);
            packet.Write(Flags);
            packet.Write(Flags2);
            packet.Write(Flags3);
            packet.Write(PetCreatureDisplayID);
            packet.Write(PetExperienceLevel);
            packet.Write(PetCreatureFamilyID);

            foreach (var professionID in ProfessionIDs)
                packet.Write(professionID);

            foreach (var inventoryItem in InventoryItems)
                inventoryItem.Write(packet);

            packet.PutBits(Name.Length, 6);
            packet.PutBit(FirstLogin);
            packet.PutBit(BoostInProgress);
            packet.PutBits(0, 5);
            packet.FlushBits();

            packet.WriteString(Name);
        }
    }
}
