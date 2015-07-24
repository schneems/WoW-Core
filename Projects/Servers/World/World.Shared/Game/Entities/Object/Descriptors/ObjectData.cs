// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class ObjectData : DescriptorBase
    {
        public DescriptorField Guid         => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField Data         => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField Type         => base[0x8, 0x1, MirrorFlags.All];
        public DescriptorField EntryID      => base[0x9, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField DynamicFlags => base[0xA, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField Scale        => base[0xB, 0x1, MirrorFlags.All];

        public static new int End => 0xC;
    }
}
