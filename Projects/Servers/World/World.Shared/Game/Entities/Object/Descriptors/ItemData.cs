// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class ItemData : DescriptorBase
    {
        public ItemData() : base(ObjectData.End) { }

        public DescriptorField Owner              => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField ContainedIn        => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField Creator            => base[0x8, 0x4, MirrorFlags.All];
        public DescriptorField GiftCreator        => base[0xC, 0x4, MirrorFlags.All];
        public DescriptorField StackCount         => base[0x10, 0x1, MirrorFlags.Owner];
        public DescriptorField Expiration         => base[0x11, 0x1, MirrorFlags.Owner];
        public DescriptorField SpellCharges       => base[0x12, 0x5, MirrorFlags.Owner];
        public DescriptorField DynamicFlags       => base[0x17, 0x1, MirrorFlags.All];
        public DescriptorField Enchantment        => base[0x18, 0x27, MirrorFlags.All];
        public DescriptorField PropertySeed       => base[0x3F, 0x1, MirrorFlags.All];
        public DescriptorField RandomPropertiesID => base[0x40, 0x1, MirrorFlags.All];
        public DescriptorField Durability         => base[0x41, 0x1, MirrorFlags.Owner];
        public DescriptorField MaxDurability      => base[0x42, 0x1, MirrorFlags.Owner];
        public DescriptorField CreatePlayedTime   => base[0x43, 0x1, MirrorFlags.All];
        public DescriptorField ModifiersMask      => base[0x44, 0x1, MirrorFlags.Owner];
        public DescriptorField Context            => base[0x45, 0x1, MirrorFlags.All];

        public static new int End => ObjectData.End + 0x46;
    }
}
