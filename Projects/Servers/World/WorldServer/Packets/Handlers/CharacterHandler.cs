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
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Logging;
using Framework.Packets.Client.Character;
using Framework.Packets.Server.Net;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets.Server.Object;

namespace WorldServer.Packets.Handlers
{
    class CharacterHandler
    {
        [GlobalMessage(GlobalClientMessage.PlayerLogin, SessionState.Authenticated)]
        public static void HandlePlayerLogin(PlayerLogin playerLogin, WorldSession session)
        {
            Log.Message(LogType.Debug, "Character with GUID '\{playerLogin.PlayerGUID.CreationBits}' tried to login...");

            var creationBits = playerLogin.PlayerGUID.CreationBits;
            var character = DB.Character.Single<Character>(c => c.Guid == creationBits);

            if (character != null)
            {
                var worldNode = Manager.Redirect.GetWorldNode((int)character.Map);

                if (worldNode != null)
                {
                    NetHandler.SendConnectTo(session, worldNode.Address, worldNode.Port, 1);

                    // Send world login stuff...
                    //session.Send(new ObjectUpdate());

                    // Suspend the current connection
                    session.Send(new SuspendComms { Serial = 0x14 });
                }
            }
            else
                session.Dispose();
        }
    }
}
