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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Logging.IO;

namespace Framework.Network
{
    public class ServerBase : IDisposable
    {
        TcpListener listener;
        bool isRunning;

        public ServerBase(string ip, int port)
        {
            var bindIP = IPAddress.None;

            if (!IPAddress.TryParse(ip, out bindIP))
            {
                Log.Normal($"Server can't be started: Invalid IP-Address ({ip})");
                Console.ReadKey(true);

                Environment.Exit(0);
            }

            try
            {
                listener = new TcpListener(bindIP, port);
                listener.Start();

                if (isRunning = listener.Server.IsBound)
                    new Thread(AcceptConnection).Start(200);
            }
            catch (Exception ex)
            {
                ExceptionLog.Write(ex);

                Log.Error(ex.Message);
            }
        }

        async void AcceptConnection(object delay)
        {
            while (isRunning)
            {
                Thread.Sleep((int)delay);

                if (listener.Pending())
                {
                    var clientSocket = await listener.AcceptSocketAsync();

                    if (clientSocket != null)
                        await DoWork(clientSocket);
                }
            }
        }

        public virtual Task DoWork(Socket client)
        {
            return null;
        }

        public void Dispose()
        {
            listener = null;
            isRunning = false;
        }
    }
}
