// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Bgs.Protocol;
using BnetServer.Constants.Bnet;
using Framework.Database.Bnet;
using Framework.Logging;
using Framework.Network;
using Google.Protobuf;

namespace BnetServer.Network
{
    public class BnetSession : SessionBase, IDisposable
    {
        public Account Account { get; set; }
        public GameAccount GameAccount { get; set; }

        public string LoginTicket { get; set; }
        public byte[] RealmListSecret { get; set; }
        public byte[] RealmListTicket { get; set; }

        Stream networkStream;
        SslStream tlsStream;
        SemaphoreSlim tlsSemaphore;

        uint messageToken;

        public override async void Accept()
        {
            try
            {
                if (Sockets[0].AcceptSocket != null)
                {
                    networkStream = new NetworkStream(Sockets[0].AcceptSocket);
                    tlsStream = new SslStream(networkStream, false);
                    tlsSemaphore = new SemaphoreSlim(1, 1);

                    await tlsStream.AuthenticateAsServerAsync(Manager.Session.Certificate, false, SslProtocols.Tls12, false);

                    if (tlsStream.IsAuthenticated && Manager.Session.Add(this))
                    {
                        var buffer = Sockets[0].Buffer;
                        int numReadBytes;

                        do
                        {
                            numReadBytes = await tlsStream.ReadAsync(buffer, 0, buffer.Length);

                            if (numReadBytes == 0)
                                break;

                            var headerLength = (buffer[0] << 8) | buffer[1];
                            var header = Header.Parser.ParseFrom(new CodedInputStream(buffer, 2, headerLength));
                            var data = new byte[header.Size];

                            if (header.Size > 0)
                                Buffer.BlockCopy(buffer, 2 + headerLength, data, 0, data.Length);

                            await Manager.BnetPacket.CallHandler(header, data, this);
                        } while (numReadBytes != 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Message(LogTypes.Error, ex.Message);
            }
            finally
            {
                if (Manager.Session.Remove(this))
                    Dispose();
            }
        }

        public override void Process(object sender, SocketAsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task Send(IMessage message, BnetServiceHash serviceHash = BnetServiceHash.None, uint methodId = 0)
        {
            await tlsSemaphore.WaitAsync();

            try
            {
                var messageData = message.ToByteArray();
                var header = new Header
                {
                    Token = messageToken++,
                    ServiceId = 0xFE,
                    Size = (uint)messageData.Length
                };

                if (serviceHash != BnetServiceHash.None)
                {
                    header.ServiceId = 0;
                    header.ServiceHash = (uint)serviceHash;
                    header.MethodId = methodId;
                }

                var headerData = header.ToByteArray();
                var packetData = new byte[2 + headerData.Length + messageData.Length];

                packetData[0] = (byte)(headerData.Length >> 8);
                packetData[1] = (byte)(headerData.Length & 0xFF);

                Buffer.BlockCopy(headerData, 0, packetData, 2, headerData.Length);
                Buffer.BlockCopy(messageData, 0, packetData, 2 + headerData.Length, messageData.Length);

                await tlsStream.WriteAsync(packetData, 0, packetData.Length);
            }
            finally
            {
                // Always release the semaphore to prevent a permanent wait/lock of it.
                tlsSemaphore.Release();
            }
        }

        public void Dispose()
        {
            tlsStream.Dispose();

            Disconnect(this);
        }
    }
}
