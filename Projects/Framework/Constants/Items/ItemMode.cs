// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        Stage3     = 8,
        Stage4     = 9
    }
}
