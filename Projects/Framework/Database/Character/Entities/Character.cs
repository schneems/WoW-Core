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
using Framework.Attributes;
using Framework.Constants.General;
using Framework.Constants.Object;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class Character : Entity
    {
        [Field(PrimaryKey = true)]
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
        public byte Level                             { get; set; }
        public int Zone                               { get; set; }
        public int Map                                { get; set; }
        public float X                                { get; set; }
        public float Y                                { get; set; }
        public float Z                                { get; set; }
        public float O                                { get; set; }
        public ulong GuildGuid                        { get; set; }
        public CharacterFlags CharacterFlags          { get; set; }
        public CharacterCustomizeFlags CustomizeFlags { get; set; }
        public uint Flags3                            { get; set; }
        public bool FirstLogin                        { get; set; }
        public uint PetCreatureDisplayId              { get; set; }
        public uint PetLevel                          { get; set; }
        public uint PetCreatureFamily                 { get; set; }

        [Field(ForeignKey = "CharacterGuid")]
        public virtual IList<CharacterAction> CharacterActions { get; set; }
        [Field(ForeignKey = "CharacterGuid")]
        public virtual IList<CharacterItem> CharacterItems     { get; set; }
        [Field(ForeignKey = "CharacterGuid")]
        public virtual IList<CharacterSkill> CharacterSkills   { get; set; }
        [Field(ForeignKey = "CharacterGuid")]
        public virtual IList<CharacterSpell> CharacterSpells   { get; set; }

        public RaceMask GetRaceMask()
        {
            return (RaceMask)(1 << ((byte)Race - 1));
        }

        public ClassMask GetClassMask()
        {
            return (ClassMask)(1 << ((byte)Class - 1));
        }
    }
}
