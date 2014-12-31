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

namespace Framework.Constants.Character
{
    public enum CharLoginCode : byte
    {
        InProgress                      = 0x4E,
        Success                         = 0x4F,
        NoWorld                         = 0x50,
        DuplicateCharacter              = 0x51,
        NoInstances                     = 0x52,
        Failed                          = 0x53,
        Disabled                        = 0x54,
        NoCharacter                     = 0x55,
        LockedForTransfer               = 0x56,
        LockedByBilling                 = 0x57,
        LockedByMobileAh                = 0x58,
        TemporaryGmLock                 = 0x59,
        LockedByCharacterUpgrade        = 0x5A,
        LockedByRevokedCharacterUpgrade = 0x5B,
    }
}
