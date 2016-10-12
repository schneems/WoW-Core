// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Constants.IPC;

namespace Framework.Pipes
{
    public class IPCPacket
    {
        public IPCMessage Message { get; set; }
        public byte[] Data { get; set; }

        BinaryReader readStream;
        BinaryWriter writeStream;

        public IPCPacket(IPCMessage message)
        {
            writeStream = new BinaryWriter(new MemoryStream());

            Message = message;

            writeStream.Write((byte)message);
        }

        public IPCPacket(int msg, Stream data)
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
            Data = (writeStream.BaseStream as MemoryStream).ToArray();
        }
    }
}
