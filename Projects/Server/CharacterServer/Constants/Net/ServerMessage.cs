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
    // Value '0x2000' means not updated/implemented
    public enum ServerMessage : ushort
    {
        #region CharacterService
        CharacterUpgradeStarted                 = 0x2000,
        CharacterUpgradeComplete                = 0x2000,
        CharacterUpgradeAborted                 = 0x2000,
        #endregion

        #region GlueMgr (Character)
        GenerateRandomCharacterNameResult       = 0x2000,
        FeatureSystemStatusGlueScreen           = 0x2000,
        MoveCharacterCheatSuccess               = 0x2000,
        MoveCharacterCheatFailure               = 0x2000,
        DisplayPromotion                        = 0x2000,
        SetPromotionResponse                    = 0x2000,
        RealmSplit                              = 0x2000,
        SetPlayerDeclinedNamesResult            = 0x2000,
        CharCustomize                           = 0x2000,
        CheatPlayerLookup                       = 0x2000,
        LiveRegionGetAccountCharacterListResult = 0x2000,
        CharacterRenameResult                   = 0x2000,
        LiveRegionCharacterCopyResult           = 0x2000,
        LiveRegionAccountRestoreResult          = 0x2000,
        CharFactionChangeResult                 = 0x2000,
        UpdateCharacterFlags                    = 0x2000,
        UndeleteCharacterResponse               = 0x2000,
        UndeleteCooldownStatusResponse          = 0x2000,
        #endregion

        #region RealmConnection
        AuthChallenge                           = 0x10AA,
        ConnectTo                               = 0x1082,
        SetVeteranTrial                         = 0x2000,
        AuthResponse                            = 0x0564,
        WaitQueueUpdate                         = 0x2000,
        WaitQueueFinish                         = 0x2000,
        EnumCharactersResult                    = 0x1154,
        UpdateExpansionLevel                    = 0x2000,
        AddonInfo                               = 0x2000,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x2000,
        DeleteChar                              = 0x2000,
        CacheVersion                            = 0x2000,
        CharacterLoginFailed                    = 0x2000,
        GetAccountCharacterListResult           = 0x2000,
        #endregion
    }
}
