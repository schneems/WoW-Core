// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Constants.Net;
using AuthServer.Network.Packets;
using AuthServer.Packets;
using Framework.Cryptography.BNet;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;

namespace AuthServer.Network.Sessions
{
    class AuthSession : IDisposable
    {
        public Account Account { get; set; }
        public List<GameAccount> GameAccounts { get; set; }
        public GameAccount GameAccount { get; set; }
        public SRP6a SecureRemotePassword { get; set; }
        public BNetCrypt Crypt { get; set; }
        public string Program { get; set; }
        public string Platform { get; set; }
        public string ConnectionInfo { get; set; }

        Socket client;
        byte[] dataBuffer = new byte[0x800];

        public AuthSession(Socket socket)
        {
            client = socket;
        }

        public void Accept()
        {
            ConnectionInfo = GetClientInfo();

            var socketEventargs = new SocketAsyncEventArgs();

            socketEventargs.SetBuffer(dataBuffer, 0, dataBuffer.Length);

            socketEventargs.Completed += Process;
            socketEventargs.UserToken = client;
            socketEventargs.SocketFlags = SocketFlags.None;

            client.ReceiveAsync(socketEventargs);
        }

        async void Process(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var socket = e.UserToken as Socket;
                var recievedBytes = e.BytesTransferred;

                if (recievedBytes != 0)
                {
                    var buff = new byte[recievedBytes];

                    Buffer.BlockCopy(dataBuffer, 0, buff, 0, recievedBytes);

                    // Enable packet encryption
                    if (Crypt == null && buff[0] == 0x45 && buff[1] == 0x01)
                    {
                        Crypt = new BNetCrypt(SecureRemotePassword.SessionKey);

                        Buffer.BlockCopy(buff, 2, buff, 0, recievedBytes -= 2);

                        Log.Debug($"Encryption for account '{Account.Id}' enabled");
                    }
                    
                    Crypt?.Decrypt(buff, recievedBytes);

                    client.ReceiveAsync(e);

                    await ProcessPacket(buff, recievedBytes);
                }
                else
                    socket.Close();
            }
            catch (Exception ex)
            {
                Dispose();

                ExceptionLog.Write(ex);

                Log.Error(ex.Message);
            }
        }

        async Task ProcessPacket(byte[] data, int size)
        {
            var packet = new AuthPacket(data, size);
            
            if (PacketLog.Initialized)
                PacketLog.Write<AuthClientMessage>(packet.Header.Message, packet.Data, client.RemoteEndPoint);

            if (packet != null)
                await PacketManager.InvokeHandler(packet, this);
        }

        public async Task Send(AuthPacket packet)
        {
            try
            {
                await Task.Run(() => packet.Finish());

                client.Send(packet.Data);
            }
            catch (Exception ex)
            {
                Dispose();

                Log.Error(ex.Message);
            }
        }

        public async Task Send(ServerPacket packet)
        {
            try
            {
                await Task.Run(() =>
                {
                    packet.Write();
                    packet.Packet.Finish();
                });

                if (packet.Packet.Header != null && PacketLog.Initialized)
                    PacketLog.Write<AuthServerMessage>(packet.Packet.Header.Message, packet.Packet.Data, client.RemoteEndPoint);

                if (Crypt != null && Crypt.IsInitialized)
                    Crypt.Encrypt(packet.Packet.Data, packet.Packet.Data.Length);

                var socketEventargs = new SocketAsyncEventArgs();

                socketEventargs.SetBuffer(packet.Packet.Data, 0, packet.Packet.Data.Length);

                socketEventargs.Completed += SendCompleted;
                socketEventargs.UserToken = packet;
                socketEventargs.RemoteEndPoint = client.RemoteEndPoint;
                socketEventargs.SocketFlags = SocketFlags.None;

                client.Send(packet.Packet.Data);
            }
            catch (Exception ex)
            {
                Dispose();

                ExceptionLog.Write(ex);

                Log.Error(ex.Message);
            }
        }

        void SendCompleted(object sender, SocketAsyncEventArgs e)
        {
        }

        public void GenerateSessionKey(byte[] clientSalt, byte[] serverSalt)
        {
            var hmac = new HMACSHA1(SecureRemotePassword.SessionKey);
            var wow = Encoding.ASCII.GetBytes("WoW\0");
            var wowSessionKey = new byte[0x28];

            hmac.TransformBlock(wow, 0, wow.Length, wow, 0);
            hmac.TransformBlock(clientSalt, 0, clientSalt.Length, clientSalt, 0);
            hmac.TransformFinalBlock(serverSalt, 0, serverSalt.Length);

            Buffer.BlockCopy(hmac.Hash, 0, wowSessionKey, 0, hmac.Hash.Length);

            hmac.Initialize();
            hmac.TransformBlock(wow, 0, wow.Length, wow, 0);
            hmac.TransformBlock(serverSalt, 0, serverSalt.Length, serverSalt, 0);
            hmac.TransformFinalBlock(clientSalt, 0, clientSalt.Length);

            Buffer.BlockCopy(hmac.Hash, 0, wowSessionKey, hmac.Hash.Length, hmac.Hash.Length);

            GameAccount.SessionKey = wowSessionKey.ToHexString();

            // Update SessionKey in database
            DB.Auth.Update(GameAccount);
        }

        public string GetClientInfo()
        {
            var ipEndPoint = client.RemoteEndPoint as IPEndPoint;

            return ipEndPoint != null ? ipEndPoint.Address + ":" + ipEndPoint.Port : "";
        }

        public void Dispose()
        {
            client = null;
            Account = null;
            SecureRemotePassword = null;
        }
    }
}
