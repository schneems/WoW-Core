// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Constants.IPC;

namespace Framework.Pipes.Packets
{
    public class ProcessStateInfo : IPCPacket
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public ProcessState State { get; set; }

        public ProcessStateInfo() : base(IPCMessage.ProcessStateInfo)
        {
        }

        public ProcessStateInfo(int msg, Stream data) : base(msg, data)
        {
            Id = readStream.ReadInt32();
            Alias = readStream.ReadString();
            State = (ProcessState)readStream.ReadByte();
        }

        public override void Write()
        {
            writeStream.Write(Id);
            writeStream.Write(Alias);
            writeStream.Write((byte)State);
        }
    }
}
