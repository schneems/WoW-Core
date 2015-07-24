// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class ConversationData : DescriptorBase
    {
        public ConversationData() : base(ObjectData.End) { }

        public DescriptorField Dummy => base[0x0, 0x1, MirrorFlags.Self];

        public static new int End => ObjectData.End + 0x1;
    }
}
