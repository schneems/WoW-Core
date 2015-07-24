// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Framework.Misc
{
    public abstract class Singleton<T> where T : class
    {
        public bool IsInitialized { get; set; }

        static object sync = new object();
        static T instance;

        public static T GetInstance()
        {
            lock (sync)
            {
                if (instance != null)
                    return instance;
            }

            var constructorInfo = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

            return instance = constructorInfo.Invoke(new object[0]) as T;
        }
    }
}
