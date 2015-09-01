// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Constants.Character
{
    public enum CharCreateCode : byte
    {
        InProgress           = 0x2E,
        Success              = 0x2F,
        Error                = 0x30,
        Failed               = 0x31,
        NameInUse            = 0x32,
        Disabled             = 0x33,
        PvpTeamsViolation    = 0x34,
        ServerLimit          = 0x35,
        AccountLimit         = 0x36,
        ServerQueue          = 0x37,
        OnlyExisting         = 0x38,
        Expansion            = 0x39,
        ExpansionClass       = 0x3A,
        LevelRequirement     = 0x3B,
        UniqueClassLimit     = 0x3C,
        CharacterInGuild     = 0x3D,
        RestrictedRaceclass  = 0x3E,
        CharacterChooseRace  = 0x3F,
        CharacterArenaLeader = 0x40,
        CharacterDeleteMail  = 0x41,
        CharacterSwapFaction = 0x42,
        CharacterRaceOnly    = 0x43,
        CharacterGoldLimit   = 0x44,
        ForceLogin           = 0x45,
        Trial                = 0x46,
    }
}
