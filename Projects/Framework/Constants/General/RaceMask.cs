// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants.General
{
    [Flags]
    public enum RaceMask : int
    {
        All               = -1,
        Human             = 1 << 0,
        Orc               = 1 << 1,
        Dwarf             = 1 << 2,
        NightElf          = 1 << 3,
        Scourge           = 1 << 4,
        Tauren            = 1 << 5,
        Gnome             = 1 << 6,
        Troll             = 1 << 7,
        Goblin            = 1 << 8,
        BloodElf          = 1 << 9,
        Draenei           = 1 << 10,
        FelOrc            = 1 << 11,
        Naga              = 1 << 12,
        Broken            = 1 << 13,
        Skeleton          = 1 << 14,
        Vrykul            = 1 << 15,
        Tuskarr           = 1 << 16,
        ForestTroll       = 1 << 17,
        Taunka            = 1 << 18,
        NorthrendSkeleton = 1 << 19,
        IceTroll          = 1 << 20,
        Worgen            = 1 << 21,
        Gilnean           = 1 << 22,
        PandarenNeutral   = 1 << 23,
        PandarenAlliance  = 1 << 24,
        PandarenHorde     = 1 << 25
    }
}
