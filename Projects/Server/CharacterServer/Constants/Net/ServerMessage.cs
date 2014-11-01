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
        GenerateRandomCharacterNameResult       = 0x0D8F,
        #endregion

        #region RealmConnection
        ConnectTo                               = 0x175A,
        AuthResponse                            = 0x0DA9,
        EnumCharactersResult                    = 0x05AF,
        AddonInfo                               = 0x1D9F,
        SetTimeZoneInformation                  = 0x2000,
        CreateChar                              = 0x0107,
        DeleteChar                              = 0x0BC4,
        CharacterLoginFailed                    = 0x2000,
        #endregion
    }
}
