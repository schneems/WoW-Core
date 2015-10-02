// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using Framework.Logging;
using Framework.Misc;
using Framework.Remoting;
using Framework.Remoting.Services;
using RemoteServer.Configuration;

namespace RemoteServer
{
    class RemoteServer
    {
        static string serverName = nameof(RemoteServer);

        static void Main(string[] args)
        {
            RemoteConfig.Initialize($"./Configs/{serverName}.conf");

            Helper.PrintHeader(serverName);

            new Thread(() => new IPCServer<CharacterService, IService>(RemoteConfig.CharacterServiceBindIP, RemoteConfig.CharacterServiceName)).Start();
            //new Thread(() => new IPCServer<WorldService, IService>(RemoteConfig.WorldServiceBindIP, RemoteConfig.WorldServiceName)).Start();
            //new Thread(() => new IPCServer<NodeService, IService>(RemoteConfig.NodeServiceBindIP, RemoteConfig.NodeServiceName)).Start();

            Log.Normal("Remote services successfully started.");

            while (true)
                Thread.Sleep(10);
        }
    }
}
