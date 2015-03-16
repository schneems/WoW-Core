/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Text;
using AuthServer.Constants.Net;
using AuthServer.Network.Packets;
using Framework.Misc;

namespace Framework.Network.Packets
{
    public class AuthPacket
    {
        public AuthPacketHeader Header { get; set; }
        public byte[] Data { get; set; }
        public int ProcessedBytes { get; set; }

        BinaryReader readStream;
        BinaryWriter writeStream;

        byte bytePart;
        byte preByte;
        int count;

        public AuthPacket()
        {
            writeStream = new BinaryWriter(new MemoryStream());
        }

        public AuthPacket(byte[] data, int size)
        {
            readStream = new BinaryReader(new MemoryStream(data));

            Header = new AuthPacketHeader();
            Header.Message = Read<byte>(6);

            if (Read<bool>(1))
                Header.Channel = (AuthChannel)Read<byte>(4);

            Header.Message = (ushort)((Header.Message + 0x3F) << (byte)Header.Channel);

            Data = new byte[size];

            Buffer.BlockCopy(data, 0, Data, 0, size);
        }

        public AuthPacket(AuthServerMessage message, AuthChannel channel = AuthChannel.Authentication)
        {
            writeStream = new BinaryWriter(new MemoryStream());

            Header = new AuthPacketHeader();
            Header.Message = (ushort)message;
            Header.Channel = channel;

            var hasChannel = channel != AuthChannel.Authentication;
            var msg = Header.Message >= 0x7E ? (Header.Message >> (byte)channel) - 0x3F : Header.Message - 0x3F;

            Write(msg, 6);
            Write(hasChannel, 1);

            if (hasChannel)
                Write((byte)Header.Channel, 4);
        }

        public void Finish()
        {
            Data = new byte[writeStream.BaseStream.Length];

            writeStream.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < Data.Length; i++)
                Data[i] = (byte)writeStream.BaseStream.ReadByte();

            writeStream.Dispose();
        }

        #region Reader
        public T Read<T>()
        {
            return readStream.Read<T>();
        }

        public byte[] Read(int count)
        {
            ProcessedBytes += count;

            return readStream.ReadBytes(count);
        }

        public string ReadString(int count)
        {
            return Encoding.UTF8.GetString(Read(count));
        }

        public string ReadString()
        {
            var tmpString = new StringBuilder();
            var tmpChar = readStream.ReadChar();
            var tmpEndChar = Convert.ToChar(Encoding.UTF8.GetString(new byte[] { 0x20 }));

            while (tmpChar != tmpEndChar)
            {
                tmpString.Append(tmpChar);
                tmpChar = readStream.ReadChar();
            }

            return tmpString.ToString();
        }

        public T Read<T>(int bits)
        {
            ulong value = 0;
            var bitsToRead = 0;

            while (bits != 0)
            {
                if ((count % 8) == 0)
                {
                    bytePart = Read<byte>();

                    ProcessedBytes += 1;
                }

                var shiftedBits = count & 7;
                bitsToRead = 8 - shiftedBits;

                if (bitsToRead >= bits)
                    bitsToRead = bits;

                bits -= bitsToRead;

                value |= (uint)((bytePart >> shiftedBits) & ((byte)(1 << bitsToRead) - 1)) << bits;

                count += bitsToRead;
            }

            var type = typeof(T).IsEnum ? typeof(T).GetEnumUnderlyingType() : typeof(T);

            return (T)Convert.ChangeType(value, type);
        }

        public string ReadFourCC()
        {
            var data = BitConverter.GetBytes(Read<uint>(32));

            Array.Reverse(data);

            return Encoding.UTF8.GetString(data).Trim(new char[] { '\0' });
        }
        #endregion

        #region Writer
        public void Write(byte[] value)
        {
            Flush();

            writeStream.Write(value);
        }

        public void Write<T>(T value, int bits)
        {
            var bitsToWrite = 0;
            var shiftedBits = 0;

            var unpacked = (ulong)Convert.ChangeType(value, typeof(ulong));
            byte packedByte = 0;

            while (bits != 0)
            {
                shiftedBits = count & 7;

                if (shiftedBits != 0 && writeStream.BaseStream.Length > 0)
                    writeStream.BaseStream.Position -= 1;

                bitsToWrite = 8 - shiftedBits;

                if (bitsToWrite >= bits)
                    bitsToWrite = bits;

                packedByte = (byte)(preByte & ~(ulong)(((1 << bitsToWrite) - 1) << shiftedBits) | (((unpacked >> (bits - bitsToWrite)) & (ulong)((1 << bitsToWrite) - 1)) << shiftedBits));

                count += bitsToWrite;
                bits -= bitsToWrite;

                if (shiftedBits != 0)
                    preByte = 0;

                writeStream.Write(packedByte);
            }

            preByte = packedByte;
        }

        public void Flush()
        {
            var remainingBits = 8 - (count & 7);

            if (remainingBits < 8)
                Write(0, remainingBits);

            preByte = 0;
        }

        public void WriteString(string data, int bits, bool isCString = true, int additionalCount = 0)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            Write(bytes.Length + additionalCount, bits);
            Write(bytes);

            if (isCString)
                writeStream.Write((byte)0);

            Flush();
        }

        public void WriteFourCC(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            Write(bytes);
        }
        #endregion
    }
}
