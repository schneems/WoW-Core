// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Framework.Misc
{
    public static class Extensions
    {
        public static T ChangeType<T>(this object value) => (T)ChangeType(value, typeof(T));

        public static object ChangeType(this object value, Type destType)
        {
            return destType.GetTypeInfo().IsEnum ? Enum.ToObject(destType, value) : Convert.ChangeType(value, destType);
        }

        public static bool IsSigned(this Type t) => Convert.ToBoolean(t.GetTypeInfo().GetField("MinValue").GetValue(null));

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
    }
}
