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

using AuthServer.Commands;
using AuthServer.Configuration;
using AuthServer.Managers;
using AuthServer.Network.Packets;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Logging;
using Framework.Misc;
using Framework.Network;

namespace AuthServer
{
    class AuthServer
    {
        static void Main(string[] args)
        {
            ReadArguments(args);

            var connString = DB.CreateConnectionString(AuthConfig.AuthDBHost, AuthConfig.AuthDBUser, AuthConfig.AuthDBPassword,
                                                       AuthConfig.AuthDBDataBase, AuthConfig.AuthDBPort, AuthConfig.AuthDBPooling,
                                                       AuthConfig.AuthDBMinPoolSize, AuthConfig.AuthDBMaxPoolSize, AuthConfig.AuthDBType);

            if (DB.Auth.CreateConnection(connString, AuthConfig.AuthDBType))
            {
                Log.Message(LogType.Init, "_____________World of Warcraft_____________");
                Log.Message(LogType.Init, "    __                                     ");
                Log.Message(LogType.Init, "    / |                     ,              ");
                Log.Message(LogType.Init, "---/__|---)__----__--_/_--------------_--_-");
                Log.Message(LogType.Init, "  /   |  /   ) /   ' /    /   /   /  / /  )");
                Log.Message(LogType.Init, "_/____|_/_____(___ _(_ __/___(___(__/_/__/_");
                Log.Message(LogType.Init, "________________AuthServer_________________");
                Log.Message();

                Helpers.PrintORMInfo();

                Log.Message(LogType.Normal, "Starting Arctium WoW AuthServer...");

                using (var server = new ServerBase(AuthConfig.BindIP, AuthConfig.BindPort))
                {
                    PacketManager.DefineMessageHandler();

                    // Set all game accounts offline
                    /*foreach (var ga in DB.Auth.GameAccounts)
                        ga.IsOnline = false;

                    DB.Auth.Update();*/

                    Manager.Initialize();

                    ConsoleCommandManager.InitCommands();
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
                        AuthConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Message(LogType.Error, $"'{args[i - 1]}' isn't a valid argument.");
                        break;
                }
            }

            if (!AuthConfig.IsInitialized)
                AuthConfig.Initialize("./Configs/AuthServer.conf");
        }
    }
}
