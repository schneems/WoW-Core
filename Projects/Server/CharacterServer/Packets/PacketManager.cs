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
using System.Collections.Concurrent;
using System.Reflection;
using CharacterServer.Attributes;
using CharacterServer.Network;
using Framework.Attributes;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Network.Packets;

namespace CharacterServer.Packets
{
    class PacketManager
    {
        static ConcurrentDictionary<ushort, Tuple<MethodInfo, Type>> MessageHandlers = new ConcurrentDictionary<ushort, Tuple<MethodInfo, Type>>();

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    foreach (dynamic msgAttr in methodInfo.GetCustomAttributes())
                    {
                        if (msgAttr is GlobalMessageAttribute || msgAttr is MessageAttribute)
                            MessageHandlers.TryAdd((ushort)msgAttr.Message, Tuple.Create(methodInfo, methodInfo.GetParameters()[0].ParameterType));
                    }
                }
            }
        }

        public static bool InvokeHandler<T>(Packet reader, CharacterSession session)
        {
            var message = reader.Header.Message;

            Log.Message(LogType.Packet, "Received Opcode: {0} (0x{1:X}), Length: {2}", Enum.GetName(typeof(T), message), message, reader.Data.Length);

            if (MessageHandlers.TryGetValue(message, out var data))
            {
                var handlerObj = Activator.CreateInstance(data.Item2) as IClientPacket;

                handlerObj.Packet = reader;

                data.Item1.Invoke(null, new object[] { handlerObj.Read(), session });

                return true;
            }

            return false;
        }
    }
}
