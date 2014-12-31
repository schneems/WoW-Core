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

using System;
using Framework.Constants.General;
using Framework.Constants.Object;

namespace Framework.Objects
{
    [Serializable]
    public class CharacterData
    {
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
    }
}
