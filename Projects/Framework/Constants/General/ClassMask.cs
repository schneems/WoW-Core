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
    public enum ClassMask : int
    {
        All         = 0,
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
