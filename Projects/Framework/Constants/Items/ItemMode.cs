/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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

namespace Framework.Constants.Items
{
    public enum ItemMode : byte
    {
        // Single modes
        Normal     = 0,
        Heroic     = 1,
        Unknown    = 2,
        Mythic     = 3,
        RaidFinder = 4,
        Dungeon    = 5,

        // Staged modes (Epic Stages...)
        Stage1     = 6,
        Stage2     = 7,
        Stage3     = 8
    }
}
