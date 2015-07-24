// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using CharacterServer.Configuration;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.Packets;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;

namespace CharacterServer
{
    class CharacterServer
    {
        static string serverName = nameof(CharacterServer);

        static void Main(string[] args)
        {
            ReadArguments(args);

            var authConnString = DB.CreateConnectionString(CharacterConfig.AuthDBHost, CharacterConfig.AuthDBUser, CharacterConfig.AuthDBPassword,
                                                           CharacterConfig.AuthDBDataBase, CharacterConfig.AuthDBPort, CharacterConfig.AuthDBMinPoolSize, 
                                                           CharacterConfig.AuthDBMaxPoolSize, CharacterConfig.AuthDBType);
            var charConnString = DB.CreateConnectionString(CharacterConfig.CharacterDBHost, CharacterConfig.CharacterDBUser, CharacterConfig.CharacterDBPassword,
                                                           CharacterConfig.CharacterDBDataBase, CharacterConfig.CharacterDBPort, CharacterConfig.CharacterDBMinPoolSize, 
                                                           CharacterConfig.CharacterDBMaxPoolSize, CharacterConfig.CharacterDBType);
            var dataConnString = DB.CreateConnectionString(CharacterConfig.DataDBHost, CharacterConfig.DataDBUser, CharacterConfig.DataDBPassword,
                                                           CharacterConfig.DataDBDataBase, CharacterConfig.DataDBPort, CharacterConfig.DataDBMinPoolSize, 
                                                           CharacterConfig.DataDBMaxPoolSize, CharacterConfig.DataDBType);

            if (DB.Auth.Initialize(authConnString, CharacterConfig.AuthDBType) &&
                DB.Character.Initialize(charConnString, CharacterConfig.CharacterDBType) &&
                DB.Data.Initialize(dataConnString, CharacterConfig.DataDBType))
            {
                Helper.PrintHeader(serverName);

                using (var server = new Server(CharacterConfig.BindIP, CharacterConfig.BindPort))
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
                        CharacterConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Error($"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!CharacterConfig.IsInitialized)
                CharacterConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
