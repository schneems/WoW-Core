/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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

namespace CharacterServer.Constants.Net
{
    public enum ServerMessages : ushort
    {
        SetVeteranTrial                         = 0x2000,
        AuthResponse                            = 0x2000,
        WaitQueueUpdate                         = 0x2000,
        WaitQueueFinish                         = 0x2000,
        EnumCharactersResult                    = 0x2000,
        GenerateRandomCharacterNameResult       = 0x2000,
        UpdateExpansionLevel                    = 0x2000,
        CharCustomize                           = 0x2000,
        CreateChar                              = 0x2000,
        DeleteChar                              = 0x2000,
        CacheVersion                            = 0x2000,
        CharacterLoginFailed                    = 0x2000,
        AccountDataTimes                        = 0x2000,
        AddonInfo                               = 0x2000,
        GetAccountCharacterListResult           = 0x2000,
        LiveRegionGetAccountCharacterListResult = 0x2000,
        CharacterRenameResult                   = 0x2000,
        LiveRegionCharacterCopyResult           = 0x2000,
        LiveRegionAccountRestoreResult          = 0x2000,
        CharacterObjectTestResponse             = 0x2000,
        CharFactionChangeResult                 = 0x2000,
        CharacterUpgradeStarted                 = 0x2000,
        CharacterUpgradeComplete                = 0x2000,
        CharacterUpgradeAborted                 = 0x2000,
        UpdateCharacterFlags                    = 0x2000,
        UndeleteCharacterResponse               = 0x2000,
        UndeleteCooldownStatusResponse          = 0x2000,

        #region Connection
        AuthChallenge                           = 0x2000,
        Pong                                    = 0x2000,
        #endregion
    }
}
