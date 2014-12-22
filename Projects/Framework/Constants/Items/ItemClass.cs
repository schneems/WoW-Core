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

namespace Framework.Constants.Items
{
    public enum ItemClass : byte
    {
        Consumable    = 0,
        Container     = 1,
        Weapon        = 2,
        Gem           = 3,
        Armor         = 4,
        Reagent       = 5,
        Projectile    = 6,
        TradeGoods    = 7,
        Recipe        = 9,
        Quiver        = 11,
        Quest         = 12,
        Key           = 13,
        Miscellaneous = 15,
        Glyph         = 16,
        BattlePets    = 17,

        // Obsolete members
        [Obsolete("", true)]
        Generic       = 8,
        [Obsolete("", true)]
        Money         = 10,
        [Obsolete("", true)]
        Permanent     = 14,
    }
}
