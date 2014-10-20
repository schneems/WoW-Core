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
        #endregion

        #region GlueMgr (Character)
        GenerateRandomCharacterNameResult       = 0x2000,
        #endregion

        #region RealmConnection
        AuthChallenge                           = 0x10AA,
        ConnectTo                               = 0x1082,
        AuthResponse                            = 0x0564,
        EnumCharactersResult                    = 0x1154,
        AddonInfo                               = 0x1400,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x0637,
        DeleteChar                              = 0x12A4,
        CharacterLoginFailed                    = 0x2000,
        #endregion
    }
}
