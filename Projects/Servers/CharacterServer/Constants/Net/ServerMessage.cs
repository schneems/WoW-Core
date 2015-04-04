// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum ServerMessage : ushort
    {
        #region CharacterService
        #endregion

        #region GlueMgr (Character)
        GenerateRandomCharacterNameResult       = 0x0216,
        #endregion

        #region RealmConnection
        AuthResponse                            = 0x18F6,
        EnumCharactersResult                    = 0x18F1,
        AddonInfo                               = 0x1715,
        TutorialFlags                           = 0x0E82,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x16BA,
        DeleteChar                              = 0x06B8,
        #endregion
    }
}
