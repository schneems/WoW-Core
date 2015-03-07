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

using System.Net.Sockets;
using System.Text;
using Framework.Constants.Misc;
using Framework.Logging;

namespace Framework.Misc
{
    public class Helper
    {
        public static bool CheckConnection(string ip, int port)
        {
            var canConnect = false;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Connect(ip, port);

                    canConnect = true;
                }
                catch
                {
                }
            }

            return canConnect;
        }

        public static void PrintHeader(string serverName)
        {
            Log.Init("_____________World of Warcraft_____________");
            Log.Init("    __                                     ");
            Log.Init("    / |                     ,              ");
            Log.Init("---/__|---)__----__--_/_--------------_--_-");
            Log.Init("  /   |  /   ) /   ' /    /   /   /  / /  )");
            Log.Init("_/____|_/_____(___ _(_ __/___(___(__/_/__/_");

            var sb = new StringBuilder();

            sb.Append("___________________________________________");

            var nameStart = (43 - serverName.Length) / 2;

            sb.Insert(nameStart, serverName);
            sb.Remove(nameStart + serverName.Length, serverName.Length);

            Log.Init(sb.ToString());
            Log.Message();
            Log.Normal($"Starting Arctium WoW {serverName}...");
        }
    }
}
