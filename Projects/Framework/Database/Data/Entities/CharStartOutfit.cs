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
        public int Id           { get; set; }
        public Race RaceId      { get; set; }
        public Class ClassId    { get; set; }
        public byte SexId       { get; set; }
        public byte OutfitId    { get; set; }
        public int ItemId0      { get; set; }
        public int ItemId1      { get; set; }
        public int ItemId2      { get; set; }
        public int ItemId3      { get; set; }
        public int ItemId4      { get; set; }
        public int ItemId5      { get; set; }
        public int ItemId6      { get; set; }
        public int ItemId7      { get; set; }
        public int ItemId8      { get; set; }
        public int ItemId9      { get; set; }
        public int ItemId10     { get; set; }
        public int ItemId11     { get; set; }
        public int ItemId12     { get; set; }
        public int ItemId13     { get; set; }
        public int ItemId14     { get; set; }
        public int ItemId15     { get; set; }
        public int ItemId16     { get; set; }
        public int ItemId17     { get; set; }
        public int ItemId18     { get; set; }
        public int ItemId19     { get; set; }
        public int ItemId20     { get; set; }
        public int ItemId21     { get; set; }
        public int ItemId22     { get; set; }
        public int ItemId23     { get; set; }
        public int PetDisplayId { get; set; }
        public int PetFamilyId  { get; set; }
    }
}
