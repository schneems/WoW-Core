// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Account;
using WorldNode.Attributes;
using WorldNode.Constants.Net;
using WorldNode.Network;
using WorldNode.Packets.Client;
using WorldNode.Packets.Server;

namespace WorldNode.Packets.Handlers
{
    class LogoutHandler
    {
        [Message(ClientMessage.LogoutRequest, SessionState.InWorld)]
        public static async void HandleLogoutRequest(LogoutRequest logoutRequest, NodeSession session)
        {
            await session.Send(new LogoutComplete());
        }
    }
}
