// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class AreaTriggerData : DescriptorBase
    {
        public AreaTriggerData() : base(ObjectData.End) { }

        public DescriptorField OverrideScaleCurve => base[0x0, 0x7, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField Caster             => base[0x7, 0x4, MirrorFlags.All];
        public DescriptorField Duration           => base[0xB, 0x1, MirrorFlags.All];
        public DescriptorField TimeToTargetScale  => base[0xC, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField SpellID            => base[0xD, 0x1, MirrorFlags.All];
        public DescriptorField SpellVisualID      => base[0xE, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField ExplicitScale      => base[0xF, 0x1, MirrorFlags.All | MirrorFlags.Urgent];

        public static new int End => ObjectData.End + 0x10;
    }
}
