// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Sockets;
using System.Threading.Tasks;
using Framework.Network;
using Framework.Remoting;
using Framework.Remoting.Objects;
using WorldServer.Configuration;
using WorldServer.Managers;

namespace WorldServer.Network
{
    class Server : ServerBase
    {
        public static IPCClient WorldService;
        public static IPCClient NodeService;
        public static WorldServerInfo ServerInfo;

        public Server(string ip, int port) : base(ip, port)
        {
            WorldService = new IPCClient(WorldConfig.WorldServiceHost, WorldConfig.WorldServiceName);
            NodeService = new IPCClient(WorldConfig.NodeServiceHost, WorldConfig.NodeServiceName);
        }

        public override async Task DoWork(Socket client)
        {
            var worker = new WorldSession(client);

            worker.Id = ++Manager.Session.LastSessionId;

            if (Manager.Session.Add(worker.Id, worker))
                await Task.Factory.StartNew(Manager.Session.Sessions[worker.Id].Accept);
        }
    }
}
