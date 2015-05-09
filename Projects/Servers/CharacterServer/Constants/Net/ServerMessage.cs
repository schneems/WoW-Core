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
        GenerateRandomCharacterNameResult       = 0x0693,
        #endregion

        #region RealmConnection
        AuthResponse                            = 0x0403,
        EnumCharactersResult                    = 0x0290,
        AddonInfo                               = 0x0AB6,
        TutorialFlags                           = 0x005C,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x0488,
        DeleteChar                              = 0x0856,
        #endregion
    }
}
