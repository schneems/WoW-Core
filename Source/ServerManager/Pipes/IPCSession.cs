// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading.Tasks;
using Framework.Pipes;
using ServerManager.Pipes.Packets;

namespace ServerManager.Pipes
{
    public class IPCSession : IPCSessionBase
    {
        public override Task ProcessPacket(byte ipcMessage, Stream ipcDataStream)
        {
            return IPCPacketManager.InvokeHandler(ipcMessage, ipcDataStream, this);
        }
    }
}
