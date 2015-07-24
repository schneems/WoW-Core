// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Constants.General;
using Framework.Constants.Object;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class Character : Entity
    {
        [PrimaryKey, AutoIncrement]
        public ulong Guid                             { get; set; }
        public uint GameAccountId                     { get; set; }
        public uint RealmId                           { get; set; }
        public string Name                            { get; set; }
        public byte ListPosition                      { get; set; }
        public Race Race                              { get; set; }
        public Class Class                            { get; set; }
        public byte Sex                               { get; set; }
        public byte Skin                              { get; set; }
        public byte Face                              { get; set; }
        public byte HairStyle                         { get; set; }
        public byte HairColor                         { get; set; }
        public byte FacialHairStyle                   { get; set; }
        public uint Experience                        { get; set; }
        public byte ExperienceLevel                   { get; set; }
        public uint Zone                              { get; set; }
        public uint Map                               { get; set; }
        public float X                                { get; set; }
        public float Y                                { get; set; }
        public float Z                                { get; set; }
        public float O                                { get; set; }
        public ulong GuildGuid                        { get; set; }
        public CharacterFlags CharacterFlags          { get; set; }
        public CharacterCustomizeFlags CustomizeFlags { get; set; }
        public uint Flags3                            { get; set; }
        public byte FirstLogin                        { get; set; }
        public uint PetCreatureDisplayId              { get; set; }
        public uint PetLevel                          { get; set; }
        public uint PetCreatureFamily                 { get; set; }

        public virtual IList<CharacterAction> CharacterActions { get; set; }
        public virtual IList<CharacterItem> CharacterItems     { get; set; }
        public virtual IList<CharacterSkill> CharacterSkills   { get; set; }
        public virtual IList<CharacterSpell> CharacterSpells   { get; set; }

        public RaceMask RaceMask => (RaceMask)(1 << ((byte)Race - 1));
        public ClassMask ClassMask => (ClassMask)(1 << ((byte)Class - 1));
    }
}
