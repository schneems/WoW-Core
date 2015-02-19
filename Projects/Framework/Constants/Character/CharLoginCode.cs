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
        InProgress                      = 0x4F,
        Success                         = 0x50,
        NoWorld                         = 0x51,
        DuplicateCharacter              = 0x52,
        NoInstances                     = 0x53,
        Failed                          = 0x54,
        Disabled                        = 0x55,
        NoCharacter                     = 0x56,
        LockedForTransfer               = 0x57,
        LockedByBilling                 = 0x58,
        LockedByMobileAh                = 0x59,
        TemporaryGmLock                 = 0x5A,
        LockedByCharacterUpgrade        = 0x5B,
        LockedByRevokedCharacterUpgrade = 0x5C,
    }
}
