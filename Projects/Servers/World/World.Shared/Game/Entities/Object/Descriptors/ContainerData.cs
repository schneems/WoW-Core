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

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class ContainerData : DescriptorBase
    {
        public new int BaseEnd => ItemData.End;

        public DescriptorField Slots    => base[0x0, 0x90];
        public DescriptorField NumSlots => base[0x90, 0x1];

        public static new int End => ItemData.End + 0x91;
    }
}
