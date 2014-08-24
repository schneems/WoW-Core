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
    public enum ClientMessage : ushort
    {
        #region UserClient
        ConnectToFailed                      = 0x2000,
        GenerateRandomCharacterName          = 0x2000,
        EnumCharacters                       = 0x01EC,
        ReorderCharacters                    = 0x2000,
        LoadingScreenNotify                  = 0x2000,
        CreateCharacter                      = 0x2000,
        CharCustomize                        = 0x2000,
        CharRaceOrFactionChange              = 0x2000,
        CharDelete                           = 0x2000,
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
