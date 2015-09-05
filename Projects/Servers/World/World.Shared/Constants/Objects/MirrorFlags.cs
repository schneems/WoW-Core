// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace World.Shared.Constants.Objects
{
    [Flags]
    public enum MirrorFlags : uint
    {
        None           = 0x0,
        All            = 0x1,
        Self           = 0x2,
        Owner          = 0x4,
        Empath         = 0x10,
        Party          = 0x20,
        UnitAll        = 0x40,
        ViewerDependet = 0x80,
        Urgent         = 0x200,
        UrgentSelfOnly = 0x400
    }
}
