// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants.General
{
    [Flags]
    public enum ClassMask : int
    {
        All         =-1,
        Warrior     = 1 << 0,
        Paladin     = 1 << 1,
        Hunter      = 1 << 2,
        Rogue       = 1 << 3,
        Priest      = 1 << 4,
        Deathknight = 1 << 5,
        Shaman      = 1 << 6,
        Mage        = 1 << 7,
        Warlock     = 1 << 8,
        Monk        = 1 << 9,
        Druid       = 1 << 10,
    }
}
