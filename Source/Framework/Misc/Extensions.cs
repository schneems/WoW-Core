// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Reflection;

namespace Framework.Misc
{
    public static class Extensions
    {
        public static uint LeftRotate(this uint value, int shiftCount) => (value << shiftCount) | (value >> (0x20 - shiftCount));

        public static T ChangeType<T>(this object value) => (T)ChangeType(value, typeof(T));

        public static object ChangeType(this object value, Type destType)
        {
            return destType.GetTypeInfo().IsEnum ? Enum.ToObject(destType, value) : Convert.ChangeType(value, destType);
        }

        public static bool IsSigned(this Type t) => Convert.ToBoolean(t.GetTypeInfo().GetField("MinValue").GetValue(null));

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

        public static byte[] ToByteArray(this string s)
        {
            var data = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)
                data[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);

            return data;
        }

        public static bool Compare(this byte[] b, byte[] b2)
        {
            for (int i = 0; i < b2.Length; i++)
                if (b[i] != b2[i])
                    return false;

            return true;
        }

        public static bool Compare(this int[] b, int[] b2)
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
                hex += $"{b:X2}";

            return hex.ToUpper();
        }

        public static BigInteger ToBigInteger(this byte[] value, bool isBigEndian = false)
        {
            var data = value as byte[];

            if (isBigEndian)
                Array.Reverse(data);

            return new BigInteger(data.Combine(new byte[] { 0 }));
        }
    }
}
