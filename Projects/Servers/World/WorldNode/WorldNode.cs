/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Threading;
using Framework.Constants.Misc;
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
                                                           NodeConfig.AuthDBDataBase, NodeConfig.AuthDBPort, NodeConfig.AuthDBPooling,
                                                           NodeConfig.AuthDBMinPoolSize, NodeConfig.AuthDBMaxPoolSize, NodeConfig.AuthDBType);
            var charConnString = DB.CreateConnectionString(NodeConfig.CharacterDBHost, NodeConfig.CharacterDBUser, NodeConfig.CharacterDBPassword,
                                                           NodeConfig.CharacterDBDataBase, NodeConfig.CharacterDBPort, NodeConfig.CharacterDBPooling,
                                                           NodeConfig.CharacterDBMinPoolSize, NodeConfig.CharacterDBMaxPoolSize, NodeConfig.CharacterDBType);
            var dataConnString = DB.CreateConnectionString(NodeConfig.DataDBHost, NodeConfig.DataDBUser, NodeConfig.DataDBPassword,
                                                           NodeConfig.DataDBDataBase, NodeConfig.DataDBPort, NodeConfig.DataDBPooling,
                                                           NodeConfig.DataDBMinPoolSize, NodeConfig.DataDBMaxPoolSize, NodeConfig.DataDBType);

            if (DB.Auth.Initialize(authConnString, NodeConfig.AuthDBType) &&
                DB.Character.Initialize(charConnString, NodeConfig.CharacterDBType) &&
                DB.Data.Initialize(dataConnString, NodeConfig.DataDBType))
            {
                Helper.PrintHeader(serverName);

                using (var server = new Server(NodeConfig.BindIP, NodeConfig.BindPort))
                {
                    PacketManager.DefineMessageHandler();

                    Manager.Initialize();

                    Log.Message(LogType.Normal, $"{serverName} successfully started");
                    Log.Message(LogType.Normal, "Total Memory: {0} Kilobytes", GC.GetTotalMemory(false) / 1024);

                    // No need of console commands.
                    while (true)
                        Thread.Sleep(1);
                }
            }
            else
                Log.Message(LogType.Error, "Not all database connections successfully opened.");
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
                        Log.Message(LogType.Error, $"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!NodeConfig.IsInitialized)
                NodeConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
