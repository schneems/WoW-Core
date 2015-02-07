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
using WorldNode.Configuration;
using WorldNode.Managers;
using WorldNode.Network;
using WorldNode.Packets;

namespace WorldNode
{
    class WorldNode
    {
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

            if (DB.Auth.CreateConnection(authConnString, NodeConfig.AuthDBType) &&
                DB.Character.CreateConnection(charConnString, NodeConfig.CharacterDBType) &&
                DB.Data.CreateConnection(dataConnString, NodeConfig.DataDBType))
            {
                Log.Message(LogType.Init, "_____________World of Warcraft_____________");
                Log.Message(LogType.Init, "    __                                     ");
                Log.Message(LogType.Init, "    / |                     ,              ");
                Log.Message(LogType.Init, "---/__|---)__----__--_/_--------------_--_-");
                Log.Message(LogType.Init, "  /   |  /   ) /   ' /    /   /   /  / /  )");
                Log.Message(LogType.Init, "_/____|_/_____(___ _(_ __/___(___(__/_/__/_");
                Log.Message(LogType.Init, "_________________WorldNode_________________");
                Log.Message();

                Log.Message(LogType.Normal, "Starting Arctium WoW WorldNode...");

                using (var server = new Server(NodeConfig.BindIP, NodeConfig.BindPort))
                {
                    PacketManager.DefineMessageHandler();

                    Manager.Initialize();

                    Log.Message(LogType.Normal, "WorldNode successfully started");
                    Log.Message(LogType.Normal, "Total Memory: {0} Kilobytes", GC.GetTotalMemory(false) / 1024);

                    // Not used for now.
                    /*var channel = new IpcChannel();

                    ChannelServices.RegisterChannel(channel, false);

                    Manager.Session.Remote = Activator.GetObject(typeof(RemoteObject), "ipc://127.0.0.1:9000/WorldObject.rem") as RemoteObject;
                    */
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
                NodeConfig.Initialize("./Configs/WorldNode.conf");
        }
    }
}
