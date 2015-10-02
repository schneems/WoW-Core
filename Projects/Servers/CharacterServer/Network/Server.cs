// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Sockets;
using System.Threading.Tasks;
using CharacterServer.Configuration;
using Framework.Network;
using Framework.Remoting;
using Framework.Remoting.Objects;

namespace CharacterServer.Network
{
    class Server : ServerBase
    {
        public static IPCClient CharacterService;
        public static CharacterServerInfo ServerInfo;

        public Server(string ip, int port) : base(ip, port)
        {
            CharacterService = new IPCClient(CharacterConfig.CharacterServiceHost, CharacterConfig.CharacterServiceName);
        }

        public override async Task DoWork(Socket client)
        {
            var worker = new CharacterSession(client);

            await Task.Factory.StartNew(worker.Accept);
        }
    }
}
