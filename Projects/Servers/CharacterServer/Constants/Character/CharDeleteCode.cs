// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Character
{
    public enum CharDeleteCode : byte
    {
        InProgress              = 0x48,
        Success                 = 0x49,
        Failed                  = 0x4A,
        FailedLockedForTransfer = 0x4B,
        FailedGuildLeader       = 0x4C,
        FailedArenaCaptain      = 0x4D,
        FailedHasHeirloomOrMail = 0x4E,
    }
}
