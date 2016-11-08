// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Constants.IPC;

namespace Framework.Pipes
{
    public abstract class IPCPacket
    {
        public IPCMessage Message { get; set; }
        public byte[] Data { get; set; }

        protected BinaryReader readStream;
        protected BinaryWriter writeStream;

        protected IPCPacket(IPCMessage message)
        {
            writeStream = new BinaryWriter(new MemoryStream());

            Message = message;

            writeStream.Write((byte)message);
        }

        protected IPCPacket(int msg, Stream data)
        {
            readStream = new BinaryReader(data);

            Message = (IPCMessage)msg;
        }

        public byte[] ReadBytes(int count) => readStream.ReadBytes(count);

        public string ReadString() => readStream.ReadString();

        public void Write(byte[] value, int count = 0)
        {
            writeStream.Write(value, 0, count == 0 ? value.Length : count);
        }

        public void WriteString(string value) => writeStream.Write(value);

        public void Finish()
        {
            Write();

            Data = (writeStream.BaseStream as MemoryStream)?.ToArray();
        }

        public abstract void Write();
    }
}
