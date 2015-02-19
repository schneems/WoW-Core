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
using AuthServer.Network;
using AuthServer.Network.Packets;
using Framework.Constants.Misc;
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
                AuthConfig.Initialize($"./Configs/{serverName}.conf");
        }
    }
}
