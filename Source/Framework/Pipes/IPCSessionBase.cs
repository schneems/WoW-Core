// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO.Pipes;
using System.Threading.Tasks;

namespace Framework.Pipes
{
    public abstract class IPCSessionBase
    {
        NamedPipeServerStream serverStream;

        public async void Process(NamedPipeServerStream pipeServerStream)
        {
            serverStream = pipeServerStream;

            while (serverStream.IsConnected)
            {
                var ipcMessage = new byte[1];

                if (await serverStream.ReadAsync(ipcMessage, 0, 1) > 0)
                    await ProcessPacket(new IPCPacket(ipcMessage[0], serverStream));
            }
        }

        public abstract Task ProcessPacket(IPCPacket ipcPacket);

        public async Task Send(IPCPacket ipcPacket)
        {
            ipcPacket.Finish();

            await serverStream.WriteAsync(ipcPacket.Data, 0, ipcPacket.Data.Length);
        }
    }
}
