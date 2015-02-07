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
using Framework.Misc;
using Framework.Objects;

namespace Framework.Network.Packets
{
    public class Packet
    {
        public byte[] Data { get; set; }
        public PacketHeader Header { get; }

        public bool IsReadComplete => readStream.BaseStream.Position >= Data.Length;

        BinaryReader readStream;
        BinaryWriter writeStream;

        #region Bit Variables
        byte bitPosition = 8;
        byte bitValue;
        #endregion

        public Packet()
        {
            writeStream = new BinaryWriter(new MemoryStream());
        }

        public Packet(byte[] data, int headerSize)
        {
            if (headerSize >= 2)
            {
                Header = new PacketHeader
                {
                    Size    = BitConverter.ToUInt16(data, 0),
                    Message = BitConverter.ToUInt16(data, 2)
                };

                Data = new byte[Header.Size];
                Buffer.BlockCopy(data, headerSize, Data, 0, Header.Size);

                readStream = new BinaryReader(new MemoryStream(Data));
            }
            else
            {
                Data = data;

                readStream = new BinaryReader(new MemoryStream(Data));
            }
        }

        public Packet(byte[] data)
        {
            Header = new PacketHeader
            {
                Size    = (ushort)(BitConverter.ToUInt16(data, 0) - 4),
                Message = (ushort)BitConverter.ToUInt32(data, 2)
            };

            Data = new byte[Header.Size];
            Buffer.BlockCopy(data, 6, Data, 0, Header.Size);

            readStream = new BinaryReader(new MemoryStream(Data));
        }

        public Packet(object message, bool authHeader = false)
        {
            writeStream = new BinaryWriter(new MemoryStream());

            Header = new PacketHeader
            {
                Size    = 4,
                Message = Convert.ToUInt16(message)
            };

            Write(Header.Size);
            Write(Header.Message);

            if (authHeader)
                Write<ushort>(0);
        }

        #region Reader
        public T Read<T>() => readStream.Read<T>();

        public byte[] ReadBytes(int count) => readStream.ReadBytes(count);

        public string ReadString()
        {
            var tmpString = new StringBuilder();
            var tmpChar = readStream.ReadChar();
            var tmpEndChar = Convert.ToChar(Encoding.UTF8.GetString(new byte[] { 0 }));

            while (tmpChar != tmpEndChar)
            {
                tmpString.Append(tmpChar);
                tmpChar = readStream.ReadChar();
            }

            return tmpString.ToString();
        }

        public string ReadString(int count)
        {
            var stringArray = readStream.ReadBytes(count);

            return Encoding.UTF8.GetString(stringArray);
        }

        public string ReadDynamicString(byte bits)
        {
            var length = GetBits<int>(bits);

            bitPosition = 8;
            bitValue = 0;

            return ReadString(length);
        }

        public T[] Read<T>(T[] data, params int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
                data[indices[i]] = Read<T>();

            return data;
        }

        public T ReadGuid<T>() where T : SmartGuid, new()
        {
            var smartGuid = new T();
            var loLength = readStream.ReadByte();
            var hiLength = readStream.ReadByte();

            var guid = 0ul;

            for (var i = 0; i < 8; i++)
                if ((1 << i & loLength) != 0)
                    guid |= (ulong)readStream.ReadByte() << (i * 8);

            smartGuid.Low = guid;

            guid = 0;

            for (var i = 0; i < 8; i++)
                if ((1 << i & hiLength) != 0)
                    guid |= (ulong)readStream.ReadByte() << (i * 8);

            smartGuid.High = guid;

            return smartGuid;
        }


        public void Skip(int count) => readStream.BaseStream.Position += count;
        #endregion

        #region Writer
        public void Write<T>(T value) => writeStream.Write(value);

        public void WriteBytes(byte[] value, int count = 0)
        {
            if (count == 0)
                writeStream.Write(value);
            else
                writeStream.Write(value, 0, count);
        }

        public void WriteString(string value, bool isCString = false)
        {
            var data = Encoding.UTF8.GetBytes(value as string);

            data = isCString ? data.Combine(new byte[1]) : data;

            writeStream.Write(data);
        }

        public void Finish()
        {
            Data = new byte[writeStream.BaseStream.Length];

            writeStream.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < Data.Length; i++)
                Data[i] = (byte)writeStream.BaseStream.ReadByte();

            if (Header != null)
            {
                Header.Size = (ushort)(Data.Length - 2);

                Data[0] = (byte)(0xFF & Header.Size);
                Data[1] = (byte)(0xFF & (Header.Size >> 8));

                if (Header.Size > 0x7FFF)
                    Data[0] = (byte)(0x80 | (0xFF & (Header.Size >> 16)));
            }

            writeStream.Dispose();
        }
        #endregion

        #region BitReader
        public bool GetBit()
        {
            if (bitPosition == 8)
            {
                bitValue = Read<byte>();
                bitPosition = 0;
            }

            int returnValue = bitValue;
            bitValue = (byte)(returnValue << 1);
            ++bitPosition;

            return Convert.ToBoolean(returnValue >> 7);
        }

        public T GetBits<T>(byte bitCount)
        {
            int returnValue = 0;

            checked
            {
                for (var i = bitCount - 1; i >= 0; --i)
                    returnValue = GetBit() ? (1 << i) | returnValue : returnValue;
            }

            return returnValue.ChangeType<T>();
        }
        #endregion

        #region BitWriter
        public void PutBit(object bit)
        {
            --bitPosition;

            if (Convert.ToBoolean(bit))
                bitValue |= (byte)(1 << (bitPosition));

            if (bitPosition == 0)
            {
                Write(bitValue);

                bitPosition = 8;
                bitValue = 0;
            }
        }

        public void PutBits(object bit, int count)
        {
            checked
            {
                for (int i = count - 1; i >= 0; --i)
                    PutBit((Convert.ToInt32(bit) >> i) & 1);
            }
        }

        public void Flush()
        {
            if (bitPosition == 8)
                return;

            Write(bitValue);

            bitValue = 0;
            bitPosition = 8;
        }
        #endregion
    }
}
