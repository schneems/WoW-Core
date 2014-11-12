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
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Cryptography.WoW;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Network.Packets;
using Framework.Objects.WorldEntities;
using Framework.Packets.Server.Authentication;

namespace Framework.Network
{
    public abstract class SessionBase : IDisposable
    {
        public Realm Realm { get; set; }
        public Account Account { get; set; }
        public GameAccount GameAccount { get; set; }
        public Player Player { get; set; }
        public WoWCrypt Crypt { get; set; }
        public SessionState State { get; set; }

        public Socket client;
        public Queue<Packet> packetQueue;
        public bool[] isTransferInitiated;
        public byte[] dataBuffer = new byte[0x2000];

        public SessionBase(Socket clientSocket)
        {
            client = clientSocket;
            packetQueue = new Queue<Packet>();
            isTransferInitiated = new bool[2]; // ServerInitiated, ClientInitiated
        }

        public void Accept()
        {
            var socketEventargs = new SocketAsyncEventArgs();

            socketEventargs.SetBuffer(dataBuffer, 0, dataBuffer.Length);

            socketEventargs.Completed += OnConnection;
            socketEventargs.UserToken = client;
            socketEventargs.SocketFlags = SocketFlags.None;

            client.ReceiveAsync(socketEventargs);

            if (!isTransferInitiated[0])
            {
                Send(new TransferInitiate());

                isTransferInitiated[0] = true;
            }
        }

        public abstract void OnConnection(object sender, SocketAsyncEventArgs e);

        public void Process(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var socket = e.UserToken as Socket;
                var recievedBytes = e.BytesTransferred;

                if (recievedBytes != 0)
                {
                    if (Crypt != null && Crypt.IsInitialized)
                    {
                        while (recievedBytes > 0)
                        {
                            Decrypt(dataBuffer);

                            var length = BitConverter.ToUInt16(dataBuffer, 0) + 4;
                            var packetData = new byte[length];

                            Buffer.BlockCopy(dataBuffer, 0, packetData, 0, length);

                            var packet = new Packet(dataBuffer, 4);

                            if (length > recievedBytes)
                                packetQueue.Enqueue(packet);

                            ProcessPacket(packet);

                            recievedBytes -= length;

                            Buffer.BlockCopy(dataBuffer, length, dataBuffer, 0, recievedBytes);
                        }
                    }
                    else
                    {
                        var packet = new Packet(dataBuffer);

                        ProcessPacket(packet);
                    }

                    client.ReceiveAsync(e);
                }
            }
            catch (Exception ex)
            {
                Dispose();

                ExceptionLog.Write(ex);

                Log.Message(LogType.Error, "{0}", ex.Message);
            }
        }

        public abstract Task ProcessPacket(Packet packet);
        public abstract void Send(ServerPacket packet);

        public void Encrypt(Packet packet)
        {
            var totalLength = (uint)packet.Header.Size - 2;
            totalLength <<= 13;
            totalLength |= ((uint)packet.Header.Message & 0x1FFF);

            var header = BitConverter.GetBytes(totalLength);

            Crypt.Encrypt(header, 4);

            packet.Data[0] = header[0];
            packet.Data[1] = header[1];
            packet.Data[2] = header[2];
            packet.Data[3] = header[3];
        }

        public void Decrypt(byte[] data)
        {
            Crypt.Decrypt(data, 4);

            var header = BitConverter.ToUInt32(data, 0);
            var size = (ushort)(header >> 13);
            var message = (ushort)(header & 0x1FFF);

            data[0] = (byte)(0xFF & size);
            data[1] = (byte)(0xFF & (size >> 8));
            data[2] = (byte)(0xFF & message);
            data[3] = (byte)(0xFF & (message >> 8));
        }

        public string GetClientInfo()
        {
            var ipEndPoint = client.RemoteEndPoint as IPEndPoint;

            return ipEndPoint != null ? ipEndPoint.Address + ":" + ipEndPoint.Port : "";
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    isTransferInitiated = new bool[2];

                    client.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
