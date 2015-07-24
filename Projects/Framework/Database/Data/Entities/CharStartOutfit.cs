// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class CharStartOutfit : Entity
    {
        public uint Id           { get; set; }
        public Race RaceId       { get; set; }
        public Class ClassId     { get; set; }
        public byte SexId        { get; set; }
        public byte OutfitId     { get; set; }
        public uint[] ItemId     { get; set; } = new uint[24];
        public uint PetDisplayId { get; set; }
        public uint PetFamilyId  { get; set; }
    }
}
