// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class DescriptorBase
    {
        public int BaseEnd { get; set; }

        public DescriptorBase() { }

        protected DescriptorBase(int baseEnd)
        {
            BaseEnd = baseEnd;
        }

        public DescriptorField this[int index, uint size, MirrorFlags flags = MirrorFlags.All] => new DescriptorField { Index = index + BaseEnd, Size = size, Flags = flags };
        public DescriptorField this[int index] => new DescriptorField { Index = index + BaseEnd };

        public static int End { get; set; }
    }
}
