// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Framework.Pipes
{
    public abstract class IPCClientBase : IDisposable
    {
        readonly NamedPipeClientStream pipeClientStream;

        protected IPCClientBase(string pipeServer, string pipeName)
        {
            pipeClientStream = new NamedPipeClientStream(pipeServer, pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);

            pipeClientStream.Connect();
        }

        public async void Process()
        {
            while (true)
            {
                if (pipeClientStream.IsConnected)
                {
                    var ipcMessage = new byte[1];

                    if (await pipeClientStream.ReadAsync(ipcMessage, 0, 1) > 0)
                        await ProcessPacket(ipcMessage[0], pipeClientStream);
                }
            }
        }

        public abstract Task ProcessPacket(byte ipcMessage, Stream ipcDataStream);

        public async Task Send(IPCPacket ipcPacket)
        {
            ipcPacket.Finish();

            await pipeClientStream.WriteAsync(ipcPacket.Data, 0, ipcPacket.Data.Length);
        }

        public void Dispose()
        {
            pipeClientStream.Dispose();
        }
    }
}
