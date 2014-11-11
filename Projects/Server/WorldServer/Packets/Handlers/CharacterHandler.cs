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

using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Packets.Client.Character;
using WorldServer.Network;

namespace WorldServer.Packets.Handlers
{
    class CharacterHandler
    {
        [GlobalMessage(GlobalClientMessage.PlayerLogin, SessionState.Authenticated)]
        public static void HandlePlayerLogin(PlayerLogin playerLogin, WorldSession session)
        {
            Log.Message(LogType.Debug, "Character with GUID '{0}' tried to login...", playerLogin.PlayerGUID.CreationBits);
        }
    }
}
