// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Constants.IPC;

namespace Framework.Pipes.Packets
{
    public class RegisterConsole : IPCPacket
    {
        public string Alias { get; set; }

        public RegisterConsole() : base(IPCMessage.RegisterConsole)
        {
        }

        public RegisterConsole(byte ipcMessage, Stream ipcMessageData) : base(ipcMessage, ipcMessageData)
        {
            Alias = readStream.ReadString();
        }

        public override void Write()
        {
            writeStream.Write(Alias);
        }
    }
}
