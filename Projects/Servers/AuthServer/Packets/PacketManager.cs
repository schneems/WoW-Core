// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Network.Packets;
using AuthServer.Network.Sessions;
using Framework.Logging;

namespace AuthServer.Packets
{
    class PacketManager
    {
        static readonly ConcurrentDictionary<AuthClientMessage, Tuple<MethodInfo, Type, AuthChannel>> MessageHandlers = new ConcurrentDictionary<AuthClientMessage, Tuple<MethodInfo, Type, AuthChannel>>();

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in methodInfo.GetCustomAttributes<AuthMessageAttribute>())
                        MessageHandlers.TryAdd(msgAttr.Message, Tuple.Create(methodInfo, methodInfo.GetParameters()[0].ParameterType, msgAttr.Channel));
        }

        public static async Task InvokeHandler(AuthPacket reader, AuthSession session)
        {
            var message = (AuthClientMessage)reader.Header.Message;

            Tuple<MethodInfo, Type, AuthChannel> data;

            if (MessageHandlers.TryGetValue(message, out data))
            {
                var handlerObj = Activator.CreateInstance(data.Item2) as ClientPacket;

                handlerObj.Packet = reader;

                await Task.Run(() => handlerObj.Read());

                /*if (handlerObj.IsReadComplete)
                    data.Item1.Invoke(null, new object[] { handlerObj, session });
                else
                    Log.Packet($"Packet read for '{data.Item2.Name}' failed.");*/
                data.Item1.Invoke(null, new object[] { handlerObj, session });
            }
            else
            {
                var msgName = Enum.GetName(typeof(AuthClientMessage), message);

                if (msgName == null)
                    Log.Packet($"Received unknown opcode '0x{message:X}, Length: {reader.Data.Length}'.");
                else
                    Log.Packet($"Packet handler for '{msgName} (0x{message:X}), Length: {reader.Data.Length}' not implemented.");
            }
        }
    }
}
