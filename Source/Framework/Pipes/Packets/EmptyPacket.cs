// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Constants.IPC;

namespace Framework.Pipes.Packets
{
    public class EmptyPacket : IPCPacket
    {
        public EmptyPacket(IPCMessage ipcMessage) : base(ipcMessage)
        {
        }

        public EmptyPacket(int msg, Stream data) : base(msg, data)
        {
        }

        public override void Write()
        {
        }
    }
}
