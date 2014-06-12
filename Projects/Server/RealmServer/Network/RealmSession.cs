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
using Framework.Constants.Misc;
using Framework.Cryptography.WoW;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;
using RealmServer.Constants.Net;
using RealmServer.Network.Packets;
using RealmServer.Network.Packets.Handlers;

namespace RealmServer.Network
{
    class RealmSession : IDisposable
    {
        public GameAccount GameAccount { get; set; }
        public WoWCrypt Crypt { get; private set; }
        public uint Challenge { get; private set; }

        Socket client;
        Queue<Packet> packetQueue;
        bool[] isTransferInitiated;
        byte[] dataBuffer = new byte[0x2000];

        public RealmSession(Socket socket)
        {
            client = socket;
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
                var serverToClient = "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT";
                var transferInitiate = new Packet();

                transferInitiate.Write((ushort)(serverToClient.Length + 1));
                transferInitiate.Write(serverToClient, true);

                Send(transferInitiate);

                isTransferInitiated[0] = true;
            }
        }

        void OnConnection(object sender, SocketAsyncEventArgs e)
        {
            if (!isTransferInitiated[1])
            {
                var clientToServer = "WORLD OF WARCRAFT CONNECTION - CLIENT TO SERVER";
                var data = new byte[0x32];

                Buffer.BlockCopy(dataBuffer, 0, data, 0, data.Length);

                var transferInitiate = new Packet(data, false);

                var length = transferInitiate.Read<ushort>();
                var msg    = transferInitiate.Read<string>(0, true);

                if (msg == clientToServer)
                {
                    isTransferInitiated[1] = true;

                    e.Completed -= OnConnection;
                    e.Completed += Process;

                    Log.Message(LogType.Debug, "Initial packet transfer for Client '{0}' successfully initialized.", GetClientIP());

                    client.ReceiveAsync(e);

                    // Assign server challenge for auth digest calculations
                    Challenge = BitConverter.ToUInt32(new byte[0].GenerateRandomKey(4), 0);

                    AuthHandler.HandleAuthChallenge(this);
                }
                else
                {
                    Log.Message(LogType.Debug, "Wrong initial packet transfer data for Client '{0}'.", GetClientIP());

                    Dispose();
                }
            }
            else
                Dispose();
        }

        void Process(object sender, SocketAsyncEventArgs e)
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
                            Crypt.Decrypt(dataBuffer, 4);

                            var header = BitConverter.ToUInt32(dataBuffer, 0);
                            var size = (ushort)(header >> 13);
                            var message = (ushort)(header & 0x1FFF);

                            dataBuffer[0] = (byte)(0xFF & size);
                            dataBuffer[1] = (byte)(0xFF & (size >> 8));
                            dataBuffer[2] = (byte)(0xFF & message);
                            dataBuffer[3] = (byte)(0xFF & (message >> 8));

                            var length     = BitConverter.ToUInt16(dataBuffer, 0) + 4;
                            var packetData = new byte[length];

                            Buffer.BlockCopy(dataBuffer, 0, packetData, 0, length);

                            var packet = new Packet(packetData);

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
                Log.Message(LogType.Error, "{0}", ex.Message);
            }
        }

        void ProcessPacket(Packet packet)
        {
            if (packetQueue.Count > 0)
                packet = packetQueue.Dequeue();

            PacketLog.Write<ClientMessages>(packet.Header.Message, packet.Data, client.RemoteEndPoint);

            PacketManager.InvokeHandler(packet, this);
        }

        public void Send(Packet packet)
        {
            try
            {
                packet.Finish();

                if (Crypt != null && Crypt.IsInitialized)
                {
                    uint totalLength = (uint)packet.Header.Size - 2;
                    totalLength <<= 13;
                    totalLength |= ((uint)packet.Header.Message & 0x1FFF);

                    var header = BitConverter.GetBytes(totalLength);

                    Crypt.Encrypt(header, 4);

                    packet.Data[0] = header[0];
                    packet.Data[1] = header[1];
                    packet.Data[2] = header[2];
                    packet.Data[3] = header[3];
                }

                var socketEventargs = new SocketAsyncEventArgs();

                socketEventargs.SetBuffer(packet.Data, 0, packet.Data.Length);

                socketEventargs.Completed += SendCompleted;
                socketEventargs.UserToken = packet;
                socketEventargs.RemoteEndPoint = client.RemoteEndPoint;
                socketEventargs.SocketFlags = SocketFlags.None;

                client.SendAsync(socketEventargs);
            }
            catch (SocketException ex)
            {
                Log.Message(LogType.Error, "{0}", ex.Message);

                client.Close();
            }
        }

        void SendCompleted(object sender, SocketAsyncEventArgs e)
        {

        }

        public string GetClientIP()
        {
            var ipEndPoint = client.RemoteEndPoint as IPEndPoint;

            return ipEndPoint != null ? ipEndPoint.Address.ToString() : "";
        }

        public void Dispose()
        {
            isTransferInitiated = new bool[2];

            client.Dispose();
        }
    }
}
