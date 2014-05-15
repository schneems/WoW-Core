/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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
using Framework.Misc;

namespace Framework.Network.Packets
{
    public class Packet
    {
        public PacketHeader Header { get; private set; }
        public byte[] Data { get; set; }

        dynamic stream;

        #region Bit Variables
        byte bitPosition = 8;
        byte bitValue;
        bool flushed;
        #endregion

        public Packet()
        {
            stream = new BinaryWriter(new MemoryStream());

            if (stream == null)
                throw new InvalidOperationException("");
        }

        public Packet(byte[] data, bool readHeader = true)
        {
            stream = new BinaryReader(new MemoryStream(data));

            if (stream == null)
                throw new InvalidOperationException("");

            if (readHeader)
            {
                Header = new PacketHeader
                {
                    Size    = Read<ushort>(),
                    Message = Read<ushort>()
                };

                // Copy packet buffer for logging, etc.
                Data = new byte[Header.Size];
                Buffer.BlockCopy(data, 4, Data, 0, Header.Size);
            }
        }

        public Packet(object message)
        {
            stream = new BinaryWriter(new MemoryStream());

            if (stream == null)
                throw new InvalidOperationException("");

            Header = new PacketHeader
            {
                Size    = 4,
                Message = Convert.ToUInt16(message)
            };

            Write(Header.Size);
            Write(Header.Message);
        }

        #region Reader
        public T Read<T>()
        {
            return Extensions.Read<T>(stream);
        }

        public void Push<T>(T[] data, params int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
                Push(out data[indices[i]]);
        }

        public void Push<T>(out T value)
        {
            value = Extensions.Read<T>(stream);
        }

        public void PushBytes(out byte[] data, int count)
        {
            data = stream.ReadBytes(count);
        }

        public void PushDynamicString(out string value, byte bits)
        {
            PushBits(out int length, bits);
            PushBytes(out var stringArray, length);

            value = Encoding.UTF8.GetString(stringArray);
        }

        public string PushCString(out string value)
        {
            var tmpString = new StringBuilder();
            var tmpChar = stream.ReadChar();
            var tmpEndChar = Convert.ToChar(Encoding.UTF8.GetString(new byte[] { 0 }));

            while (tmpChar != tmpEndChar)
            {
                tmpString.Append(tmpChar);
                tmpChar = stream.ReadChar();
            }

            return value = tmpString.ToString();
        }
        #endregion

        #region Writer
        public void Write<T>(T value, bool isCString = false)
        {
            // Flush bit writer if necessary
            Flush();

            var type = typeof(T).IsEnum ? typeof(T).GetEnumUnderlyingType() : typeof(T);

            switch (type.Name)
            {
                case "Boolean":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "SByte":
                    stream.Write(Convert.ToSByte(value));
                    break;
                case "Byte":
                    stream.Write(Convert.ToByte(value));
                    break;
                case "Int16":
                    stream.Write(Convert.ToInt16(value));
                    break;
                case "UInt16":
                    stream.Write(Convert.ToUInt16(value));
                    break;
                case "Int32":
                    stream.Write(Convert.ToInt32(value));
                    break;
                case "UInt32":
                    stream.Write(Convert.ToUInt32(value));
                    break;
                case "Int64":
                    stream.Write(Convert.ToInt64(value));
                    break;
                case "UInt64":
                    stream.Write(Convert.ToUInt64(value));
                    break;
                case "Single":
                    stream.Write(Convert.ToSingle(value));
                    break;
                case "Byte[]":
                    var data = value as byte[];

                    if (data != null)
                        stream.Write(data);
                    break;
                case "String":
                    data = Encoding.UTF8.GetBytes(value as string);
                    data = isCString ? data.Combine(new byte[1]) : data;

                    if (data != null)
                        stream.Write(data);
                    break;
                case "SmartGuid":
                    break;
                default:
                    break;
            }
        }

        public void Finish()
        {
            Data = new byte[stream.BaseStream.Length];

            stream.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < Data.Length; i++)
                Data[i] = (byte)stream.BaseStream.ReadByte();

            if (Header != null)
            {
                Header.Size = (ushort)(Data.Length - 2);

                Data[0] = (byte)(0xFF & Header.Size);
                Data[1] = (byte)(0xFF & (Header.Size >> 8));

                if (Header.Size > 0x7FFF)
                    Data[0] = (byte)(0x80 | (0xFF & (Header.Size >> 16)));
            }

            stream.Dispose();
        }
        #endregion

        #region BitReader
        public T PushBits<T>(out T value, byte count = 1)
        {
            var ret = 0ul;

            checked
            {
                for (var i = count - 1; i >= 0; --i)
                {
                    if (bitPosition == 8)
                    {
                        Push(out bitValue);

                        bitPosition = 0;
                    }

                    int returnValue = bitValue;
                    bitValue = (byte)(2 * returnValue);
                    ++bitPosition;

                    ret = Convert.ToBoolean(returnValue >> 7) ? (ulong)(1 << i) | (ulong)returnValue : (ulong)returnValue;
                }
            }

            return value = ret.ChangeType<T>();
        }
        #endregion

        #region BitWriter
        public void WriteBits<T>(T value, int count = 1)
        {
            flushed = false;

            checked
            {
                for (int i = count - 1; i >= 0; --i)
                {
                    --bitPosition;

                    if (Convert.ToBoolean(((Convert.ToUInt64(value) >> i) & 1).ChangeType<T>()))
                        bitValue |= (byte)(1 << (bitPosition));

                    if (bitPosition == 0)
                    {
                        bitPosition = 8;
                        Write(bitValue);
                        bitValue = 0;
                    }
                }
            }
        }

        void Flush()
        {
            if (flushed || bitPosition == 8)
                return;

            Write(bitValue);

            bitValue = 0;
            bitPosition = 8;

            flushed = true;
        }
        #endregion
    }
}
