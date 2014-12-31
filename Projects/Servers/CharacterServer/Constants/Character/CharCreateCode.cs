/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
