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
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Misc
{
    public abstract class Singleton<T> where T : class
    {
        public bool IsInitialized { get; set; } = false;

        static Dictionary<string, T> objects = new Dictionary<string, T>();
        static object sync = new object();

        public static T GetInstance()
        {
            var typeName = typeof(T).FullName;

            lock (sync)
            {
                if (objects.ContainsKey(typeName))
                    return objects[typeName];
            }

            var constructorInfo = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            var instance = constructorInfo.Invoke(new object[0]) as T;

            objects.Add(instance.ToString(), instance);

            return objects[typeName];
        }
    }
}
