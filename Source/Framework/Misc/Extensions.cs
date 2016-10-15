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
    }
}
