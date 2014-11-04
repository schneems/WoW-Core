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
using System.Net.Sockets;
using System.Threading.Tasks;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Misc;
using Framework.Network;
using Framework.Network.Packets;
using WorldServer.Constants.Net;
using WorldServer.Packets;
using ClientPacket = Framework.Packets.Client.Authentication;
using ServerPacket = Framework.Packets.Server.Authentication;

namespace WorldServer.Network
{
    class WorldSession : SessionBase
    {
        public uint Challenge    { get; private set; }
        public byte[] ClientSeed { get; private set; }
        public byte[] ServerSeed { get; private set; }

        public WorldSession(Socket clientSocket) : base(clientSocket) { }

        public override void OnConnection(object sender, SocketAsyncEventArgs e)
        {
            if (!isTransferInitiated[1])
            {
                var clientToServer = "WORLD OF WARCRAFT CONNECTION - CLIENT TO SERVER";
                var data = new byte[0x32];

                Buffer.BlockCopy(dataBuffer, 0, data, 0, data.Length);

                var transferInit = new ClientPacket.TransferInitiate { Packet = new Packet(data, false) } as ClientPacket.TransferInitiate;

                transferInit.Read();

                if (transferInit.Msg == clientToServer)
                {
                    isTransferInitiated[1] = true;

                    e.Completed -= OnConnection;
                    e.Completed += Process;

                    Log.Message(LogType.Debug, "Initial packet transfer for Client '{0}' successfully initialized.", GetClientIP());

                    client.ReceiveAsync(e);

                    // Assign server challenge for auth digest calculations
                    Challenge  = BitConverter.ToUInt32(new byte[0].GenerateRandomKey(4), 0);
                    ClientSeed = new byte[16].GenerateRandomKey(16);
                    ServerSeed = new byte[16].GenerateRandomKey(16);

                    Send(new ServerPacket.AuthChallenge
                    {
                        Challenge    = Challenge,
                        DosChallenge = ClientSeed.Combine(ServerSeed)
                    });
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

        public override async Task ProcessPacket(Packet packet)
        {
            if (packetQueue.Count > 0)
                packet = packetQueue.Dequeue();

            PacketLog.Write<ClientMessage>(packet.Header.Message, packet.Data, client.RemoteEndPoint);

            await PacketManager.InvokeHandler<ClientMessage>(packet, this);
        }

        public override void Send(IServerPacket packet)
        {
            try
            {
                packet.Write();
                packet.Packet.Finish();

                if (packet.Packet.Header != null)
                    PacketLog.Write<ServerMessage>(packet.Packet.Header.Message, packet.Packet.Data, client.RemoteEndPoint);

                if (Crypt != null && Crypt.IsInitialized)
                    Encrypt(packet.Packet);

                var socketEventargs = new SocketAsyncEventArgs();

                socketEventargs.SetBuffer(packet.Packet.Data, 0, packet.Packet.Data.Length);

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
