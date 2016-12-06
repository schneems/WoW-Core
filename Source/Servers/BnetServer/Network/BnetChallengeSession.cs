// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading.Tasks;
using Framework.Logging;
using Framework.Network;
using Framework.Web;

namespace BnetServer.Network
{
    public class BnetChallengeSession : SessionBase, IDisposable
    {
        Stream networkStream;
        SslStream tlsStream;

        public override async void Accept()
        {
            try
            {
                if (Sockets[0].AcceptSocket != null)
                {
                    networkStream = new NetworkStream(Sockets[0].AcceptSocket);
                    tlsStream = new SslStream(networkStream, false);

                    await tlsStream.AuthenticateAsServerAsync(Manager.Session.Certificate, false, SslProtocols.Tls12, false);

                    var buffer = Sockets[0].Buffer;
                    int numReadBytes;

                    do
                    {
                        numReadBytes = await tlsStream.ReadAsync(buffer, 0, buffer.Length);

                        if (numReadBytes == 0)
                            break;

                        var httpRequest = HttpRequest.Parse(buffer, numReadBytes);

                        if (httpRequest == null)
                            break;

                        Manager.RestPacket.CallService(httpRequest, this);
                    } while (numReadBytes != 0);
                }
            }
            catch (Exception ex)
            {
                Log.Message(LogTypes.Error, ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        public override void Process(object sender, SocketAsyncEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task Send(byte[] data) => tlsStream.WriteAsync(data, 0, data.Length);

        public void Dispose()
        {
            tlsStream.Dispose();

            Disconnect(this);
        }
    }
}
