/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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
using CharacterServer.Configuration;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.Network.Packets;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Logging;

namespace CharacterServer
{
    class CharacterServer
    {
        static void Main(string[] args)
        {
            ReadArguments(args);

            var authConnection = DB.Auth.CreateConnection(CharacterConfig.AuthDBHost, CharacterConfig.AuthDBUser, CharacterConfig.AuthDBPassword,
                                                          CharacterConfig.AuthDBDataBase, CharacterConfig.AuthDBPort, CharacterConfig.MySqlPooling,
                                                          CharacterConfig.MySqlMinPoolSize, CharacterConfig.MySqlMaxPoolSize);

            var charConnection = DB.Character.CreateConnection(CharacterConfig.CharacterDBHost, CharacterConfig.CharacterDBUser, CharacterConfig.CharacterDBPassword,
                                                               CharacterConfig.CharacterDBDataBase, CharacterConfig.CharacterDBPort, CharacterConfig.MySqlPooling,
                                                               CharacterConfig.MySqlMinPoolSize, CharacterConfig.MySqlMaxPoolSize);

            var dataConnection = DB.Data.CreateConnection(CharacterConfig.DataDBHost, CharacterConfig.DataDBUser, CharacterConfig.DataDBPassword,
                                                          CharacterConfig.DataDBDataBase, CharacterConfig.DataDBPort, CharacterConfig.MySqlPooling,
                                                          CharacterConfig.MySqlMinPoolSize, CharacterConfig.MySqlMaxPoolSize);

            Log.Message(LogType.Init, "_____________World of Warcraft_____________");
            Log.Message(LogType.Init, "    __                                     ");
            Log.Message(LogType.Init, "    / |                     ,              ");
            Log.Message(LogType.Init, "---/__|---)__----__--_/_--------------_--_-");
            Log.Message(LogType.Init, "  /   |  /   ) /   ' /    /   /   /  / /  )");
            Log.Message(LogType.Init, "_/____|_/_____(___ _(_ __/___(___(__/_/__/_");
            Log.Message(LogType.Init, "______________CharacterServer______________");
            Log.Message();

            Log.Message(LogType.Normal, "Starting Arctium WoW CharacterServer...");

            using (var server = new Server(CharacterConfig.BindIP, CharacterConfig.BindPort))
            {
                PacketManager.DefineMessageHandler();

                Manager.Initialize();

                Log.Message(LogType.Normal, "CharacterServer successfully started");
                Log.Message(LogType.Normal, "Total Memory: {0} Kilobytes", GC.GetTotalMemory(false) / 1024);

                // No need of console commands.
                while (true)
                    Thread.Sleep(1);
            }
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
                        Log.Message(LogType.Error, "'{0}' isn't a valid argument.", args[i - 1]);
                        break;
                }
            }

            if (!CharacterConfig.IsInitialized)
                CharacterConfig.Initialize("./Configs/CharacterServer.conf");
        }
    }
}
