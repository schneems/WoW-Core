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
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace Framework.Misc
{
    public static class Extensions
    {
        // Create only one service
        static PluralizationService pluralService = PluralizationService.CreateService(new CultureInfo("en-US"));

        #region BinaryReader
        public static Dictionary<Type, Func<BinaryReader, object>> ReadValue = new Dictionary<Type, Func<BinaryReader, object>>()
        {
            { typeof(bool),   br => br.ReadBoolean() },
            { typeof(sbyte),  br => br.ReadSByte()   },
            { typeof(byte),   br => br.ReadByte()    },
            { typeof(char),   br => br.ReadChar()    },
            { typeof(short),  br => br.ReadInt16()   },
            { typeof(ushort), br => br.ReadUInt16()  },
            { typeof(int),    br => br.ReadInt32()   },
            { typeof(uint),   br => br.ReadUInt32()  },
            { typeof(float),  br => br.ReadSingle()  },
            { typeof(long),   br => br.ReadInt64()   },
            { typeof(ulong),  br => br.ReadUInt64()  },
            { typeof(double), br => br.ReadDouble()  },
        };

        public static T Read<T>(this BinaryReader br)
        {
            var type = typeof(T).IsEnum ? typeof(T).GetEnumUnderlyingType() : typeof(T);

            return (T)ReadValue[type](br);
        }
        #endregion
        #region UInt32
        public static uint LeftRotate(this uint value, int shiftCount)
        {
            return (value << shiftCount) | (value >> (0x20 - shiftCount));
        }
        #endregion
        #region String
        public static byte[] ToByteArray(this string s)
        {
            var data = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)
                data[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);

            return data;
        }

        public static string Pluralize(this string s)
        {
            return pluralService.Pluralize(s);
        }
        #endregion
        #region ByteArray
        public static byte[] GenerateRandomKey(this byte[] s, int length)
        {
            var random = new Random((int)((uint)(Guid.NewGuid().GetHashCode() ^ 1 >> 89 << 2 ^ 42)).LeftRotate(13));
            var key = new byte[length];

            for (int i = 0; i < length; i++)
            {
                int randValue = -1;

                do
                {
                    randValue = (int)((uint)random.Next(0xFF)).LeftRotate(1) ^ i;
                } while (randValue > 0xFF && randValue <= 0);

                key[i] = (byte)randValue;
            }

            return key;
        }

        public static bool Compare(this byte[] b, byte[] b2)
        {
            for (int i = 0; i < b2.Length; i++)
                if (b[i] != b2[i])
                    return false;

            return true;
        }

        public static byte[] Combine(this byte[] data, params byte[][] pData)
        {
            var combined = data;

            foreach (var arr in pData)
            {
                var currentSize = combined.Length;

                Array.Resize(ref combined, currentSize + arr.Length);

                Buffer.BlockCopy(arr, 0, combined, currentSize, arr.Length);
            }

            return combined;
        }

        public static string ToHexString(this byte[] data)
        {
            var hex = "";

            foreach (var b in data)
                hex += string.Format("{0:X2}", b);

            return hex.ToUpper();
        }
        #endregion
        #region Type
        public static IList CreateList(this Type type)
        {
            var genericType = typeof(List<>).MakeGenericType(type);

            return Activator.CreateInstance(genericType) as IList;
        }
        #endregion
        #region Generic
        public static BigInteger ToBigInteger<T>(this T value, bool isBigEndian = false)
        {
            var ret = BigInteger.Zero;

            switch (typeof(T).Name)
            {
                case "Byte[]":
                    var data = value as byte[];

                    if (isBigEndian)
                        Array.Reverse(data);

                    ret = new BigInteger(data.Combine(new byte[] { 0 }));
                    break;
                case "BigInteger":
                    ret = (BigInteger)Convert.ChangeType(value, typeof(BigInteger));
                    break;
                default:
                    throw new NotSupportedException(string.Format("'{0}' conversion to 'BigInteger' not supported.", typeof(T).Name));
            }

            return ret;
        }
        public static T ChangeType<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.GetCultureInfo("en-US").NumberFormat);
        }

        public static object ChangeType(this object value, Type destType)
        {
            return destType.IsEnum ? Enum.ToObject(destType, value) : Convert.ChangeType(value, destType);
        }
        #endregion
    }
}
