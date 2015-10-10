// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WorldServer.Attributes;
using WorldServer.Constants.Net;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets.Client.Misc;
using Framework.Constants.Account;
using Framework.Logging;
using Framework.Packets.Server.Character;
using Framework.Packets.Server.Net;
using Framework.Constants.Character;
using Framework.Packets.Handlers;
using World.Shared.Game.Entities.Object.Guid;
using System.Linq;

namespace WorldServer.Packets.Handlers
{
    class MiscHandler
    {
        [Message(ClientMessage.LoadingScreenNotify, SessionState.Authenticated)]
        public static async void HandleLoadingScreenNotify(LoadingScreenNotify loadingScreenNotify, WorldSession session)
        {
            Log.Debug("Loading screen for map '{0}' {1}.", loadingScreenNotify.MapID, loadingScreenNotify.Showing ? "enabled" : "disabled");

            if (loadingScreenNotify.Showing)
            {
                if (WorldServer.Info.Maps.Contains(loadingScreenNotify.MapID) || WorldServer.Info.Maps.Contains(-1))
                    return;

                var worldServer = Manager.Redirect.GetWorldServer(loadingScreenNotify.MapID);

                if (worldServer != null)
                {
                    var key = Manager.Redirect.CreateRedirectKey(session.GameAccount.Id, session.Player != null ? (session.Player.Guid as PlayerGuid).CreationBits : 0);

                    await NetHandler.SendConnectTo(session, Manager.Redirect.Crypt, key, worldServer.IPAddress, worldServer.Port);

                    // Suspend the current connection
                    await session.Send(new SuspendComms { Serial = 0x14 });
                }
                else
                    await session.Send(new CharacterLoginFailed { Code = CharLoginCode.NoWorld });
            }
        }
    }
}
