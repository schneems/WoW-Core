// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Logging;
using Framework.Misc;
using Framework.Pipes;
using ServerManager.Commands;
using ServerManager.Misc;
using ServerManager.Pipes;

namespace ServerManager
{
    public class ServerManager
    {
        static string serverName = nameof(ServerManager);

        public static void Main(string[] args)
        {
            ManagerConfig.Initialize("Configs/ServerManager.conf");

            Helper.PrintHeader(serverName);

            using (var consolePipeServer = new IPCServer<IPCSession>(ManagerConfig.ConsoleServiceName))
            {
                consolePipeServer.Start();

                IPCPacketManager.DefineMessageHandler();

                CommandManager.InitializeCommands();

                Log.Message(LogTypes.Success, $"{serverName} successfully started.");

                CommandManager.StartCommandHandler();
            }
        }
    }
}
