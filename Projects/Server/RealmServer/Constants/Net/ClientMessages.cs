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

namespace RealmServer.Constants.Net
{
    public enum ClientMessages : ushort
    {
        #region UserClient
        GenerateRandomCharacterName        = 0x2000,
        EnumCharacters                     = 0x2000,
        ReorderCharacters                  = 0x2000,
        CreateCharacter                    = 0x2000,
        CharCustomize                      = 0x2000,
        CharRaceOrFactionChange            = 0x2000,
        ReadyForAccountDataTimes           = 0x2000,
        CharDelete                         = 0x2000,
        CharForceDelete                    = 0x2000,
        GetAccountCharacterList            = 0x2000,
        LiveRegionGetAccountCharacterList  = 0x2000,
        LiveRegionCharacterCopy            = 0x2000,
        LiveRegionAccountRestore           = 0x2000,
        CharacterRenameRequest             = 0x2000,
        SwitchCharacter                    = 0x2000,
        EnumCharactersDeletedByClient      = 0x2000,
        UndeleteCharacter                  = 0x2000,
        GetUndeleteCharacterCooldownStatus = 0x2000,
        #endregion

        #region UserRouterClient
        AuthSession                        = 0x0602,
        Ping                               = 0x2000,
        LogDisconnect                      = 0x2000
        #endregion
    }
}
