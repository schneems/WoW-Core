// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Framework.Pipes;

namespace ServerManager.Pipes
{
    public class ConsolePipeSession : IPCSessionBase
    {
        public override Task ProcessPacket(IPCPacket ipcPacket)
        {
            throw new NotImplementedException();
        }
    }
}
