// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class ContainerData : DescriptorBase
    {
        public ContainerData() : base(ItemData.End) { }

        public DescriptorField Slots    => base[0x0, 0x90, MirrorFlags.All];
        public DescriptorField NumSlots => base[0x90, 0x1, MirrorFlags.All];

        public static new int End => ItemData.End + 0x91;
    }
}
