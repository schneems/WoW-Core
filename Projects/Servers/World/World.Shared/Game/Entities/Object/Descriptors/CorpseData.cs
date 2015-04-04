// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class CorpseData : DescriptorBase
    {
        public CorpseData() : base(ObjectData.End) { }

        public DescriptorField Owner             => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField PartyGUID         => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField DisplayID         => base[0x8, 0x1, MirrorFlags.All];
        public DescriptorField Items             => base[0x9, 0x13, MirrorFlags.All];
        public DescriptorField SkinID            => base[0x1C, 0x1, MirrorFlags.All];
        public DescriptorField FacialHairStyleID => base[0x1D, 0x1, MirrorFlags.All];
        public DescriptorField Flags             => base[0x1E, 0x1, MirrorFlags.All];
        public DescriptorField DynamicFlags      => base[0x1F, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField FactionTemplate   => base[0x20, 0x1, MirrorFlags.All];

        public static new int End                => ObjectData.End + 0x21;
    }
}
