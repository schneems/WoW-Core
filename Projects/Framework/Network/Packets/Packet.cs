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

        public Packet(byte[] data)
        {
            stream = new BinaryReader(new MemoryStream(data));

            if (stream == null)
                throw new InvalidOperationException("");

            Header = new PacketHeader
            {
                Size    = stream.Read<ushort>(),
                Message = stream.Read<ushort>()
            };

            // Copy packet buffer for logging, etc.
            Data = new byte[Header.Size];
            Buffer.BlockCopy(data, 4, Data, 0, Header.Size);
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
        public T Read<T>(int count, bool isCString = false)
        {
            switch (typeof(T).Name)
            {
                case "String":
                    if (isCString)
                    {
                        var tmpString = new StringBuilder();
                        var tmpChar = stream.ReadChar();
                        var tmpEndChar = Convert.ToChar(Encoding.UTF8.GetString(new byte[] { 0 }));

                        while (tmpChar != tmpEndChar)
                        {
                            tmpString.Append(tmpChar);
                            tmpChar = stream.ReadChar();
                        }

                        return tmpString.ToString().ChangeType<T>();
                    }
                    else
                    {
                        var stringArray = stream.ReadBytes(count);

                        return Encoding.UTF8.GetString(stringArray).ChangeType<T>();
                    }
                default:
                    return stream.Read<T>();
            }
        }

        public byte[] ReadBytes(int count)
        {
            return stream.ReadBytes(count);
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
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "Byte":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "Int32":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "UInt32":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "Int64":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "UInt64":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "Single":
                    stream.Write(Convert.ToBoolean(value));
                    break;
                case "Byte[]":
                    var data = value as byte[];

                    if (data != null)
                        stream.Write(data);
                    break;
                case "String":
                    data = Encoding.UTF8.GetBytes(value as string);
                    data = isCString ? data : data.Combine(new byte[1]);

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

            Header.Size = (ushort)(Data.Length - 2);

            if (Header.Size > 0x7FFF)
                Data[0] = (byte)(0x80 | (0xFF & (Header.Size >> 16)));

            stream.Dispose();
        }
        #endregion
        #region BitReader
        public T GetBit<T>(byte count = 1)
        {
            var ret = 0ul;

            checked
            {
                for (var i = count - 1; i >= 0; --i)
                {
                    if (bitPosition == 8)
                    {
                        bitValue = stream.Read<byte>();
                        bitPosition = 0;
                    }

                    int returnValue = bitValue;
                    bitValue = (byte)(2 * returnValue);
                    ++bitPosition;

                    ret = Convert.ToBoolean(returnValue >> 7) ? (ulong)(1 << i) | (ulong)returnValue : (ulong)returnValue;
                }
            }

            return ret.ChangeType<T>();
        }
        #endregion
        #region BitWriter
        public void PutBit<T>(T value, int count = 1)
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
                        stream.Write(bitValue);
                        bitValue = 0;
                    }
                }
            }
        }

        void Flush()
        {
            if (flushed || bitPosition == 8)
                return;

            stream.Write(bitValue);

            bitValue = 0;
            bitPosition = 8;

            flushed = true;
        }
        #endregion
    }
}
