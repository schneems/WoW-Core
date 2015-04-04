// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class DescriptorField
    {
        public int Index { get; set; }
        public uint Size { get; set; }
        public MirrorFlags Flags { get; set; }
        public object Value { get; set; }

        public static DescriptorField operator +(DescriptorField f, int add)
        {
            return new DescriptorField
            {
                Index = f.Index + add,
                Size  = (uint)(f.Size - add),
                Flags = f.Flags
            };
        }
    }
}
