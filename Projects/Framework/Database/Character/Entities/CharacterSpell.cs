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
using Lappa_ORM.Attributes;

namespace Framework.Database.Character.Entities
{
    public class CharacterSpell : Entity
    {
        [Field(PrimaryKey = true)]
        public ulong CharacterGuid { get; set; }
        public uint SpellId        { get; set; }

        [Field(ForeignKey = "Guid")]
        public virtual Character Character { get; set; }
    }
}
