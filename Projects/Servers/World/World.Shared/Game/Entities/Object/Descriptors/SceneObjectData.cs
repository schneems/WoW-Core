// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class SceneObjectData : DescriptorBase
    {
        public SceneObjectData() : base(ObjectData.End) { }

        public DescriptorField ScriptPackageID => base[0x0, 0x1, MirrorFlags.All];
        public DescriptorField RndSeedVal      => base[0x1, 0x1, MirrorFlags.All];
        public DescriptorField CreatedBy       => base[0x2, 0x4, MirrorFlags.All];
        public DescriptorField SceneType       => base[0x6, 0x1, MirrorFlags.All];

        public static new int End => ObjectData.End + 0x7;
    }
}
