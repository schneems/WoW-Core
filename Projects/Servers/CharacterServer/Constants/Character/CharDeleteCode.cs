// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Character
{
    public enum CharDeleteCode : byte
    {
        InProgress                     = 0x47,
        Success                        = 0x48,
        Failed                         = 0x49,
        FailedLockedForTransfer        = 0x4A,
        FailedGuildLeader              = 0x4B,
        FailedArenaCaptain             = 0x4C,
        FailedHasHeirloomOrMail        = 0x4D,
        FailedUpgradeInProgress        = 0x4E,
        FailedHasWowToken              = 0x4F,
        FailedVasTransactionInProgress = 0x50,
    }
}
