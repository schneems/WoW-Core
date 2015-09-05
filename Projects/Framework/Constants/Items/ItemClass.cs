// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
