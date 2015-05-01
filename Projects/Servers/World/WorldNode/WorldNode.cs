// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;
using WorldNode.Configuration;
using WorldNode.Managers;
using WorldNode.Network;
using WorldNode.Packets;

namespace WorldNode
{
    class WorldNode
    {
        static string serverName = nameof(WorldNode);

        static void Main(string[] args)
        {
            ReadArguments(args);

            var authConnString = DB.CreateConnectionString(NodeConfig.AuthDBHost, NodeConfig.AuthDBUser, NodeConfig.AuthDBPassword,
                                                           NodeConfig.AuthDBDataBase, NodeConfig.AuthDBPort, NodeConfig.AuthDBMinPoolSize, 
                                                           NodeConfig.AuthDBMaxPoolSize, NodeConfig.AuthDBType);
            var charConnString = DB.CreateConnectionString(NodeConfig.CharacterDBHost, NodeConfig.CharacterDBUser, NodeConfig.CharacterDBPassword,
                                                           NodeConfig.CharacterDBDataBase, NodeConfig.CharacterDBPort, NodeConfig.CharacterDBMinPoolSize, 
                                                           NodeConfig.CharacterDBMaxPoolSize, NodeConfig.CharacterDBType);
            var dataConnString = DB.CreateConnectionString(NodeConfig.DataDBHost, NodeConfig.DataDBUser, NodeConfig.DataDBPassword,
                                                           NodeConfig.DataDBDataBase, NodeConfig.DataDBPort, NodeConfig.DataDBMinPoolSize, 
                                                           NodeConfig.DataDBMaxPoolSize, NodeConfig.DataDBType);

            if (DB.Auth.Initialize(authConnString, NodeConfig.AuthDBType) &&
                DB.Character.Initialize(charConnString, NodeConfig.CharacterDBType) &&
                DB.Data.Initialize(dataConnString, NodeConfig.DataDBType))
            {
                Helper.PrintHeader(serverName);

                using (var server = new Server(NodeConfig.BindIP, NodeConfig.BindPort))
                {
                    PacketManager.DefineMessageHandler();

                    Manager.Initialize();

                    Log.Normal($"{serverName} successfully started");
                    Log.Normal("Total Memory: {0} Kilobytes", GC.GetTotalMemory(false) / 1024);

                    // No need of console commands.
                    while (true)
                        Thread.Sleep(1);
                }
            }
            else
                Log.Error("Not all database connections successfully opened.");
        }

        static void ReadArguments(string[] args)
        {
            for (int i = 1; i < args.Length; i += 2)
            {
                switch (args[i - 1])
                {
                    case "-config":
                        NodeConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Error($"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!NodeConfig.IsInitialized)
                NodeConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
