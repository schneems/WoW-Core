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

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class ItemModifiedAppearance : Entity
    {
        public uint Id          { get; set; }
        public int ItemId       { get; set; }
        public int Mode         { get; set; }
        public int AppearanceId { get; set; }
        public int FileDataId   { get; set; } // Icon
        public int Version      { get; set; }
    }
}
