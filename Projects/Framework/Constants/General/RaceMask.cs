/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace Framework.Constants.General
{
    [Flags]
    public enum RaceMask : int
    {
        All               = 0,
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
