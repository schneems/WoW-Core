// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BnetServer.Console;
using BnetServer.Misc;
using BnetServer.Network;
using BnetServer.Pipes;
using Framework.Logging;
using Framework.Misc;
using Framework.Pipes.Packets;

namespace BnetServer
{
    public class BnetServer
    {
        public static string Alias { get; private set; }
        public static ConsolePipeClient ConsoleClient { get; private set; }

        static readonly string serverName = nameof(BnetServer);

        public static void Main(string[] args)
        {
            var startParams = Helper.ParseArgs(args);

            // We need an alias here.
            if (!startParams.ContainsKey("alias") || startParams["alias"] == null)
                return;

            Alias = startParams["alias"].ToString();

            BnetConfig.Initialize("Configs/BnetServer.conf");

            Helper.PrintHeader(serverName);

            using (ConsoleClient = new ConsolePipeClient(BnetConfig.ConsoleServiceServer, BnetConfig.ConsoleServiceName))
            {
                IPCPacketManager.DefineMessageHandler();

                // Register console to ServerManager and start listening for incoming ipc packets.
                ConsoleClient.Send(new RegisterConsole { Alias = Alias }).GetAwaiter().GetResult();
                ConsoleClient.Process();

                using (var bnetServer = new BnetSocketServer(BnetConfig.BnetBindHost, BnetConfig.BnetBindPort, BnetConfig.BnetMaxConnections, 0x4000))
                using (var bnetChallengeServer = new BnetChallengeSocketServer(BnetConfig.BnetChallengeBindHost, BnetConfig.BnetChallengeBindPort, BnetConfig.BnetChallengeMaxConnections, 0x4000))
                {
                    if (bnetServer.Start())
                        Log.Message(LogTypes.Info, $"Bnet connection listening on '{BnetConfig.BnetBindHost}:{BnetConfig.BnetBindPort}'.");

                    if (bnetChallengeServer.Start())
                        Log.Message(LogTypes.Info, $"Bnet challenge connection listening on '{BnetConfig.BnetChallengeBindHost}:{BnetConfig.BnetChallengeBindPort}'.");

                    if (bnetServer.IsListening && bnetChallengeServer.IsListening)
                    {
                        Manager.Packet.Initialize();

                        CommandManager.InitializeCommands();

                        Log.Message(LogTypes.Success, $"{serverName} successfully started.");

                        CommandManager.StartCommandHandler();
                    }
                }
            }
        }

        public static void Shutdown()
        {
            // TODO: Implement save shutdown.
            Environment.Exit(0);
        }
    }
}
