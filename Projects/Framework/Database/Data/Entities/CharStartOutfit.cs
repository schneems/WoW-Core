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
