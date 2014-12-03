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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Logging.IO;
using WorldServer.Managers;

namespace WorldServer.Network
{
    class Server : IDisposable
    {
        TcpListener listener;
        bool isRunning;

        public Server(string ip, int port)
        {
            var bindIP = IPAddress.None;

            if (!IPAddress.TryParse(ip, out bindIP))
            {
                Log.Message(LogType.Normal, "WorldServer can't be started: Invalid IP-Address ({0})", ip);
                Console.ReadKey(true);

                Environment.Exit(0);
            }

            try
            {
                listener = new TcpListener(bindIP, port);
                listener.Start();

                if (isRunning = listener.Server.IsBound)
                    Task.Factory.StartNew(AcceptConnection);
            }
            catch (Exception ex)
            {
                ExceptionLog.Write(ex);

                Log.Message(LogType.Error, "{0}", ex.Message);
            }
        }

        async void AcceptConnection()
        {
            while (isRunning)
            {
                // Accept a new connection every 200ms.
                Thread.Sleep(200);

                if (listener.Pending())
                {
                    var clientSocket = await listener.AcceptSocketAsync();

                    if (clientSocket != null)
                    {
                        var worker = new WorldSession(clientSocket);

                        worker.Id = ++Manager.Session.LastSessionId;

                        if (Manager.Session.Add(worker.Id, worker))
                            await Task.Factory.StartNew(Manager.Session.Sessions[worker.Id].Accept);
                    }
                }
            }
        }

        public void Dispose()
        {
            listener = null;
            isRunning = false;
        }
    }
}
