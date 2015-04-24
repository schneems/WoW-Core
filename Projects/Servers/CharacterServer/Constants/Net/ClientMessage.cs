// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum ClientMessage : ushort
    {
        #region UserClient
        ConnectToFailed                      = 0x2000,
        GenerateRandomCharacterName          = 0x0336,
        EnumCharacters                       = 0x03C6,
        ReorderCharacters                    = 0x2000,
        LoadingScreenNotify                  = 0x0735,
        CreateCharacter                      = 0x0F35,
        CharCustomize                        = 0x2000,
        CharRaceOrFactionChange              = 0x2000,
        CharDelete                           = 0x0BC5,
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
