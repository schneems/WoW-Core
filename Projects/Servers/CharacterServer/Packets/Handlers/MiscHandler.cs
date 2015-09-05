// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Attributes;
using CharacterServer.Constants.Net;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.Packets.Client.Misc;
using Framework.Constants.Account;
using Framework.Logging;
using Framework.Packets.Server.Net;

namespace CharacterServer.Packets.Handlers
{
    class MiscHandler
    {
        [Message(ClientMessage.LoadingScreenNotify, SessionState.Authenticated)]
        public static async void HandleLoadingScreenNotify(LoadingScreenNotify loadingScreenNotify, CharacterSession session)
        {
            Log.Debug("Loading screen for map '{0}' {1}.", loadingScreenNotify.MapID, loadingScreenNotify.Showing ? "enabled" : "disabled");

            if (loadingScreenNotify.Showing)
            {
                var worldServer = Manager.Redirect.GetWorldServer(loadingScreenNotify.MapID);

                if (worldServer != null)
                {
                    NetHandler.SendConnectTo(session, worldServer.Address, worldServer.Port);

                    // Suspend the current connection
                    await session.Send(new SuspendComms { Serial = 0x14 });
                }
            }
        }
    }
}
