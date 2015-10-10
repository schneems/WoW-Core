// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    enum ServerMessage : ushort
    {
        AuthResponse                      = 0x09B3,
        EnumCharactersResult              = 0x096B,
        AddonInfo                         = 0x08B1,
        TutorialFlags                     = 0x03A1,
        SetTimeZoneInformation            = 0x2000,
        CreateChar                        = 0x1C71,
        DeleteChar                        = 0x0272,
        GenerateRandomCharacterNameResult = 0x070E,
        AccountDataTimes                  = 0x09FB,
        InitialKnownSpells                = 0x1E0A,
        ObjectUpdate                      = 0x0C59,
    }
}
