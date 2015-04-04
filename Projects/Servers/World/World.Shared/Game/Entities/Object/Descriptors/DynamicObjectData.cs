// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class DynamicObjectData : DescriptorBase
    {
        public DynamicObjectData() : base(ObjectData.End) { }

        public DescriptorField Caster          => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField TypeAndVisualID => base[0x4, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField SpellID         => base[0x5, 0x1, MirrorFlags.All];
        public DescriptorField Radius          => base[0x6, 0x1, MirrorFlags.All];
        public DescriptorField CastTime        => base[0x7, 0x1, MirrorFlags.All];

        public static new int End => ObjectData.End + 0x8;
    }
}
