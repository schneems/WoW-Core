// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Sockets;
using System.Threading.Tasks;
using Framework.Network;
using WorldNode.Managers;

namespace WorldNode.Network
{
    class Server : ServerBase
    {
        public Server(string ip, int port) : base(ip, port) { }

        public override async Task DoWork(Socket client)
        {
            var worker = new NodeSession(client);

            worker.Id = ++Manager.Session.LastSessionId;

            if (Manager.Session.Add(worker.Id, worker))
                await Task.Factory.StartNew(Manager.Session.Sessions[worker.Id].Accept);
        }
    }
}
