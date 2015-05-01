// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Commands;
using AuthServer.Configuration;
using AuthServer.Managers;
using AuthServer.Network;
using AuthServer.Network.Packets;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;

namespace AuthServer
{
    class AuthServer
    {
        static string serverName = nameof(AuthServer);

        static void Main(string[] args)
        {
            ReadArguments(args);

            var connString = DB.CreateConnectionString(AuthConfig.AuthDBHost, AuthConfig.AuthDBUser, AuthConfig.AuthDBPassword,
                                                       AuthConfig.AuthDBDataBase, AuthConfig.AuthDBPort, AuthConfig.AuthDBMinPoolSize, 
                                                       AuthConfig.AuthDBMaxPoolSize, AuthConfig.AuthDBType);

            if (DB.Auth.Initialize(connString, AuthConfig.AuthDBType))
            {
                Helper.PrintHeader(serverName);

                using (var server = new Server(AuthConfig.BindIP, AuthConfig.BindPort))
                {
                    PacketManager.DefineMessageHandler();

                    Manager.Initialize();

                    ConsoleCommandManager.InitCommands();
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
                        AuthConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Error($"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!AuthConfig.IsInitialized)
                AuthConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
