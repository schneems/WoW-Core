// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum ClientMessage : ushort
    {
        #region UserClient
        ConnectToFailed                      = 0x2000,
        GenerateRandomCharacterName          = 0x02F4,
        EnumCharacters                       = 0x09FB,
        ReorderCharacters                    = 0x2000,
        LoadingScreenNotify                  = 0x00FC,
        CreateCharacter                      = 0x00B2,
        CharCustomize                        = 0x2000,
        CharRaceOrFactionChange              = 0x2000,
        CharDelete                           = 0x093C,
        LiveRegionGetAccountCharacterList    = 0x2000,
        LiveRegionCharacterCopy              = 0x2000,
        LiveRegionAccountRestore             = 0x2000,
        CharacterRenameRequest               = 0x2000,
        Tutorial                             = 0x2000,
        EnumCharactersDeletedByClient        = 0x2000,
        UndeleteCharacter                    = 0x2000,
        GetUndeleteCharacterCooldownStatus   = 0x2000,
        #endregion
    }
}
