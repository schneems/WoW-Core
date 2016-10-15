// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Threading.Tasks;
using Framework.Pipes;

namespace BnetServer.Pipes
{
    public class ConsolePipeClient : IPCClientBase
    {
        public ConsolePipeClient(string pipeName) : base(pipeName)
        {
        }

        public override Task ProcessPacket(byte ipcMessage, Stream ipcDataStream)
        {
            throw new NotImplementedException();
        }
    }
}
