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
        public uint ItemId0      { get; set; }
        public uint ItemId1      { get; set; }
        public uint ItemId2      { get; set; }
        public uint ItemId3      { get; set; }
        public uint ItemId4      { get; set; }
        public uint ItemId5      { get; set; }
        public uint ItemId6      { get; set; }
        public uint ItemId7      { get; set; }
        public uint ItemId8      { get; set; }
        public uint ItemId9      { get; set; }
        public uint ItemId10     { get; set; }
        public uint ItemId11     { get; set; }
        public uint ItemId12     { get; set; }
        public uint ItemId13     { get; set; }
        public uint ItemId14     { get; set; }
        public uint ItemId15     { get; set; }
        public uint ItemId16     { get; set; }
        public uint ItemId17     { get; set; }
        public uint ItemId18     { get; set; }
        public uint ItemId19     { get; set; }
        public uint ItemId20     { get; set; }
        public uint ItemId21     { get; set; }
        public uint ItemId22     { get; set; }
        public uint ItemId23     { get; set; }
        public uint PetDisplayId { get; set; }
        public uint PetFamilyId  { get; set; }
    }
}
