// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;
using WorldServer.Configuration;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets;

namespace WorldServer
{
    class WorldServer
    {
        static string serverName = nameof(WorldServer);

        static void Main(string[] args)
        {
            ReadArguments(args);

            var authConnString = DB.CreateConnectionString(WorldConfig.AuthDBHost, WorldConfig.AuthDBUser, WorldConfig.AuthDBPassword,
                                                           WorldConfig.AuthDBDataBase, WorldConfig.AuthDBPort, WorldConfig.AuthDBMinPoolSize, 
                                                           WorldConfig.AuthDBMaxPoolSize, WorldConfig.AuthDBType);
            var charConnString = DB.CreateConnectionString(WorldConfig.CharacterDBHost, WorldConfig.CharacterDBUser, WorldConfig.CharacterDBPassword,
                                                           WorldConfig.CharacterDBDataBase, WorldConfig.CharacterDBPort, WorldConfig.CharacterDBMinPoolSize, 
                                                           WorldConfig.CharacterDBMaxPoolSize, WorldConfig.CharacterDBType);
            var dataConnString = DB.CreateConnectionString(WorldConfig.DataDBHost, WorldConfig.DataDBUser, WorldConfig.DataDBPassword,
                                                           WorldConfig.DataDBDataBase, WorldConfig.DataDBPort, WorldConfig.DataDBMinPoolSize, 
                                                           WorldConfig.DataDBMaxPoolSize, WorldConfig.DataDBType);

            if (DB.Auth.Initialize(authConnString, WorldConfig.AuthDBType) &&
                DB.Character.Initialize(charConnString, WorldConfig.CharacterDBType) &&
                DB.Data.Initialize(dataConnString, WorldConfig.DataDBType))
            {
                Helper.PrintHeader(serverName);

                using (var server = new Server(WorldConfig.BindIP, WorldConfig.BindPort))
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
                        WorldConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Error($"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!WorldConfig.IsInitialized)
                WorldConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
