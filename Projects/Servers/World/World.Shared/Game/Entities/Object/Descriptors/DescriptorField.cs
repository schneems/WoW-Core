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

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class DescriptorField
    {
        public int Index { get; set; }
        public uint Size { get; set; }
        public MirrorFlags Flags { get; set; }
        public object Value { get; set; }

        public static DescriptorField operator +(DescriptorField f, int add)
        {
            return new DescriptorField
            {
                Index = f.Index + add,
                Size  = (uint)(f.Size - add),
                Flags = f.Flags
            };
        }
    }
}
