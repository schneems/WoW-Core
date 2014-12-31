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
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using CharacterServer.Attributes;
using CharacterServer.Constants.Net;
using CharacterServer.Network;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Network.Packets;

namespace CharacterServer.Packets
{
    class PacketManager
    {
        static ConcurrentDictionary<ushort, Tuple<MethodInfo, Type, SessionState>> MessageHandlers = new ConcurrentDictionary<ushort, Tuple<MethodInfo, Type, SessionState>>();

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
                            MessageHandlers.TryAdd((ushort)msgAttr.Message, Tuple.Create(methodInfo, methodInfo.GetParameters()[0].ParameterType, msgAttr.State));
                    }
                }
            }
        }

        public static async Task InvokeHandler<T>(Packet reader, CharacterSession session)
        {
            var message = reader.Header.Message;

            Tuple<MethodInfo, Type, SessionState> data;

            if (MessageHandlers.TryGetValue(message, out data))
            {
                if ((session.State & data.Item3) == SessionState.None)
                {
                    var clientInfo = session.GetClientInfo();

                    Log.Message(LogType.Debug, "Client '\{clientInfo}': Received not allowed packet for state '\{session.State}'.");
                    Log.Message(LogType.Debug, "Disconnecting '\{clientInfo}'.");

                    session.Dispose();

                    return;
                }

                var handlerObj = Activator.CreateInstance(data.Item2) as ClientPacket;

                handlerObj.Packet = reader;

                await Task.Run(() => handlerObj.Read());

                if (handlerObj.IsReadComplete)
                    data.Item1.Invoke(null, new object[] { handlerObj, session });
                else
                    Log.Message(LogType.Error, "Packet read for '\{data.Item2.Name}' failed.");
            }
            else
            {
                var msgName = Enum.GetName(typeof(ClientMessage), message) ?? Enum.GetName(typeof(GlobalClientMessage), message);

                if (msgName == null)
                    Log.Message(LogType.Error, "Received unknown opcode '0x\{message:X}, Length: \{reader.Data.Length}'.");
                else
                    Log.Message(LogType.Error, "Packet handler for '\{msgName} (0x\{message:X}), Length: \{reader.Data.Length}' not implemented.");
            }
        }
    }
}
