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
    class ItemData : DescriptorBase
    {
        public ItemData() : base(ObjectData.End) { }

        public DescriptorField Owner              => base[0x0, 0x4];
        public DescriptorField ContainedIn        => base[0x4, 0x4];
        public DescriptorField Creator            => base[0x8, 0x4];
        public DescriptorField GiftCreator        => base[0xC, 0x4];
        public DescriptorField StackCount         => base[0x10, 0x1, MirrorFlags.Owner];
        public DescriptorField Expiration         => base[0x11, 0x1, MirrorFlags.Owner];
        public DescriptorField SpellCharges       => base[0x12, 0x5, MirrorFlags.Owner];
        public DescriptorField DynamicFlags       => base[0x17, 0x1];
        public DescriptorField Enchantment        => base[0x18, 0x27];
        public DescriptorField PropertySeed       => base[0x3F, 0x1];
        public DescriptorField RandomPropertiesID => base[0x40, 0x1];
        public DescriptorField Durability         => base[0x41, 0x1, MirrorFlags.Owner];
        public DescriptorField MaxDurability      => base[0x42, 0x1, MirrorFlags.Owner];
        public DescriptorField CreatePlayedTime   => base[0x43, 0x1];
        public DescriptorField ModifiersMask      => base[0x44, 0x1, MirrorFlags.Owner];
        public DescriptorField Context            => base[0x45, 0x1];

        public static new int End => ObjectData.End + 0x46;
    }
}
