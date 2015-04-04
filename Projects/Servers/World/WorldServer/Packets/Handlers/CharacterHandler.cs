// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Logging;
using Framework.Packets.Server.Net;
using World.Shared.Game.Entities;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets.Client.Character;

namespace WorldServer.Packets.Handlers
{
    class CharacterHandler
    {
        [GlobalMessage(GlobalClientMessage.PlayerLogin, SessionState.Authenticated)]
        public static void HandlePlayerLogin(PlayerLogin playerLogin, WorldSession session)
        {
            Log.Debug($"Character with GUID '{playerLogin.PlayerGUID.CreationBits}' tried to login...");

            var character = DB.Character.Single<Character>(c => c.Guid == playerLogin.PlayerGUID.CreationBits);

            if (character != null)
            {
                var worldNode = Manager.Redirect.GetWorldNode((int)character.Map);

                if (worldNode != null)
                {
                    // Create new player.
                    session.Player = new Player(character);

                    NetHandler.SendConnectTo(session, worldNode.Address, worldNode.Port, 1);

                    // Suspend the current connection
                    session.Send(new SuspendComms { Serial = 0x14 });

                    Manager.Player.EnterWorld(session);

                    // Enter world.
                    ObjectHandler.ObjectUpdateHandler(session);
                }
            }
            else
                session.Dispose();
        }
    }
}
