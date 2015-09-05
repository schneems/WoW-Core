// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
