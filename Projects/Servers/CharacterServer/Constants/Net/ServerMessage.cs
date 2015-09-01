// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum ServerMessage : ushort
    {
        #region CharacterService
        #endregion

        #region GlueMgr (Character)
        GenerateRandomCharacterNameResult       = 0x070E,
        #endregion

        #region RealmConnection
        AuthResponse                            = 0x09B3,
        EnumCharactersResult                    = 0x096B,
        AddonInfo                               = 0x08B1,
        TutorialFlags                           = 0x03A1,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x1C71,
        DeleteChar                              = 0x0272,
        #endregion
    }
}
