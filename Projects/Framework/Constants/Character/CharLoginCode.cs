// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
