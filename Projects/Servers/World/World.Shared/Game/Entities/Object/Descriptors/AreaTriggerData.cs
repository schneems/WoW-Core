// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class AreaTriggerData : DescriptorBase
    {
        public AreaTriggerData() : base(ObjectData.End) { }

        public DescriptorField Caster        => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField Duration      => base[0x4, 0x1, MirrorFlags.All];
        public DescriptorField SpellID       => base[0x5, 0x1, MirrorFlags.All];
        public DescriptorField SpellVisualID => base[0x6, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField ExplicitScale => base[0x7, 0x1, MirrorFlags.All | MirrorFlags.Urgent];

        public static new int End => ObjectData.End + 0x8;
    }
}
