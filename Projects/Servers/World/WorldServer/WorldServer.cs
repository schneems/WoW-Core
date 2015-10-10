// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;
using Framework.Remoting.Objects;
using WorldServer.Configuration;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets;

namespace WorldServer
{
    class WorldServer
    {
        public static ServerInfo Info { get; private set; }
        static string serverName = nameof(WorldServer);

        static void Main(string[] args)
        {
            ReadArguments(args);

            Info = Helper.GetServerDefinition(WorldConfig.ServerId, "wId");

            if (Info == null)
            {
                Log.Error($"Can't find any server definition for wId '{WorldConfig.ServerId}'.");
                Log.Wait();

                return;
            }

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

                Log.Normal($"Loading server with wId '{WorldConfig.ServerId}'.");
                
                using (var server = new Server(WorldConfig.BindIP, Info.Port))
                {
                    Server.NodeService.Register(null);
                    Server.WorldService.Register(null);

                    Server.ServerInfo = new WorldServerInfo
                    {
                        RealmId   = Info.Realm,
                        Maps      = Info.Maps,
                        IPAddress = Info.Address,
                        Port      = Info.Port,
                    };

                    Server.WorldService.Register(Server.ServerInfo);

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
            if (!WorldConfig.IsInitialized)
                WorldConfig.Initialize($"./Configs/{serverName}.conf");

            for (int i = 1; i < args.Length; i += 2)
            {
                switch (args[i - 1])
                {
                    case "-config":
                        WorldConfig.Initialize(args[i]);
                        break;
                    case "-id":
                        WorldConfig.ServerId = uint.Parse(args[i]);
                        break;
                    default:
                        Log.Error($"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }
        }
    }
}
