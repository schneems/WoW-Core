// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class GameObjectData : DescriptorBase
    {
        public GameObjectData() : base(ObjectData.End) { }

        public DescriptorField CreatedBy          => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField DisplayID          => base[0x4, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField Flags              => base[0x5, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField ParentRotation     => base[0x6, 0x4, MirrorFlags.All];
        public DescriptorField FactionTemplate    => base[0xA, 0x1, MirrorFlags.All];
        public DescriptorField Level              => base[0xB, 0x1, MirrorFlags.All];
        public DescriptorField PercentHealth      => base[0xC, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField SpellVisualID      => base[0xD, 0x1, MirrorFlags.All | MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateSpellVisualID => base[0xE, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateAnimID        => base[0xF, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateAnimKitID     => base[0x10, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateWorldEffectID => base[0x11, 0x4, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];


        public static new int End => ObjectData.End + 0x15;
    }
}
