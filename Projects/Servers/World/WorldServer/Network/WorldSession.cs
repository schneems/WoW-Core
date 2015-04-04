// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;
using Framework.Network;
using Framework.Network.Packets;
using World.Shared.Game.Entities;
using WorldServer.Constants.Net;
using WorldServer.Packets;
using ClientPacket = Framework.Packets.Client.Authentication;
using ServerPacket = Framework.Packets.Server.Authentication;

namespace WorldServer.Network
{
    public class WorldSession : SessionBase
    {
        public Realm Realm { get; set; }
        public Account Account { get; set; }
        public GameAccount GameAccount { get; set; }
        public Player Player { get; set; }

        public uint Challenge    { get; private set; }
        public byte[] ClientSeed { get; private set; }
        public byte[] ServerSeed { get; private set; }

        public WorldSession(Socket clientSocket) : base(clientSocket) { }

        public override void OnConnection(object sender, SocketAsyncEventArgs e)
        {
            var recievedBytes = e.BytesTransferred;

            if (!isTransferInitiated[1])
            {
                var clientToServer = "WORLD OF WARCRAFT CONNECTION - CLIENT TO SERVER\0";

                var transferInit = new ClientPacket.TransferInitiate { Packet = new Packet(dataBuffer, 2) } as ClientPacket.TransferInitiate;

                transferInit.Read();

                if (transferInit.Msg == clientToServer)
                {
                    State = SessionState.Initiated;

                    isTransferInitiated[1] = true;

                    e.Completed -= OnConnection;
                    e.Completed += Process;

                    Log.Debug($"Initial packet transfer for Client '{GetClientInfo()}' successfully initialized.");

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
                    Log.Debug($"Wrong initial packet transfer data for Client '{GetClientInfo()}'.");

                    Dispose();
                }
            }
            else
                Dispose();
        }

        public override void Process(object sender, SocketAsyncEventArgs e)
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

                        new Task(async () => await ProcessPacket(packet)).Start();

                        recievedBytes -= length;

                        Buffer.BlockCopy(dataBuffer, length, dataBuffer, 0, recievedBytes);
                    }
                }
                else
                {
                    var packet = new Packet(dataBuffer);

                    new Task(async () => await ProcessPacket(packet)).Start();
                }

                client.ReceiveAsync(e);
            }
        }

        public override async Task ProcessPacket(Packet packet)
        {
            if (packetQueue.Count > 0)
                packet = packetQueue.Dequeue();

            PacketLog.Write<ClientMessage>(packet.Header.Message, packet.Data, client.RemoteEndPoint);

            await PacketManager.InvokeHandler<ClientMessage>(packet, this);
        }

        public override void Send(Framework.Network.Packets.ServerPacket packet)
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

                if (!client.Connected)
                    return;

                socketEventargs.Completed += SendCompleted;
                socketEventargs.UserToken = packet;
                socketEventargs.RemoteEndPoint = client.RemoteEndPoint;
                socketEventargs.SocketFlags = SocketFlags.None;

                client.SendAsync(socketEventargs);
            }
            catch (SocketException ex)
            {
                Log.Error($"{ex}");

                client.Close();
            }
        }

        void SendCompleted(object sender, SocketAsyncEventArgs e)
        {
        }
    }
}
