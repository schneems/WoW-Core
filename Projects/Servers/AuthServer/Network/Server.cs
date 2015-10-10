// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Sockets;
using System.Threading.Tasks;
using AuthServer.Configuration;
using AuthServer.Managers;
using AuthServer.Network.Sessions;
using Framework.Network;
using Framework.Remoting;

namespace AuthServer.Network
{
    class Server : ServerBase
    {
        public static IPCClient WorldService;

        public Server(string ip, int port) : base(ip, port)
        {
            WorldService = new IPCClient(AuthConfig.WorldServiceHost, AuthConfig.WorldServiceName);
        }

        public override async Task DoWork(Socket client)
        {
            var clientId = ++Manager.SessionMgr.LastSessionId;

            if (Manager.SessionMgr.Clients.TryAdd(clientId, new AuthSession(client)))
                await Task.Factory.StartNew(Manager.SessionMgr.Clients[clientId].Accept);
        }
    }
}
