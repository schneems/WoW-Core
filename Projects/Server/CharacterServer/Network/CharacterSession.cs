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
using System.Net.Sockets;
using Framework.Constants.Misc;
using Framework.Cryptography.WoW;
using Framework.Logging;
using Framework.Network.Packets;

namespace CharacterServer.Network
{
    class CharacterSession
    {
        public WoWCrypt Crypt { get; private set; }

        Socket client;
        Queue<Packet> packetQueue;
        byte[] dataBuffer = new byte[0x2000];

        public CharacterSession(Socket socket)
        {
            client = socket;
            packetQueue = new Queue<Packet>();
        }

        public void Accept()
        {
            var socketEventargs = new SocketAsyncEventArgs();

            socketEventargs.SetBuffer(dataBuffer, 0, dataBuffer.Length);

            socketEventargs.Completed += Process;
            socketEventargs.UserToken = client;
            socketEventargs.SocketFlags = SocketFlags.None;

            client.ReceiveAsync(socketEventargs);
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

                            var length = BitConverter.ToUInt16(dataBuffer, 0) + 4;

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
            packet = packetQueue.Count > 0 ? packetQueue.Dequeue() : packet;

            //TODO Invoke packet handlers
        }

        void Decode(ref byte[] data)
        {
        }

        public void Send(Packet packet)
        {
            try
            {
                packet.Finish();

                /*if (Crypt != null && Crypt.IsInitialized)
                {
                    uint totalLength = (uint)packet.Size - 2;
                    totalLength <<= 13;
                    totalLength |= ((uint)packet.Opcode & 0x1FFF);

                    var header = BitConverter.GetBytes(totalLength);

                    Crypt.Encrypt(header, 4);

                    buffer[0] = header[0];
                    buffer[1] = header[1];
                    buffer[2] = header[2];
                    buffer[3] = header[3];
                }*/

                var socketEventargs = new SocketAsyncEventArgs();

                //socketEventargs.SetBuffer(packet.Data, 0, packet.Data.Length);

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
    }
}
