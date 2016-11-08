// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Framework.Attributes;
using Framework.Constants.IPC;
using Framework.Logging;
using Framework.Pipes;

namespace ServerManager.Pipes
{
    public class IPCPacketManager
    { 
        static readonly ConcurrentDictionary<IPCMessage, Tuple<MethodInfo, Type>> messageHandlers = new ConcurrentDictionary<IPCMessage, Tuple<MethodInfo, Type>>();

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetEntryAssembly();

            foreach (var type in currentAsm.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    foreach (var msgAttr in methodInfo.GetCustomAttributes<IPCMessageAttribute>())
                        messageHandlers.TryAdd(msgAttr.Message, Tuple.Create(methodInfo, methodInfo.GetParameters()[0].ParameterType));
                }
            }
        }

        public static async Task CallHandler(byte ipcMessage, Stream ipcMessageData, IPCSession session)
        {
            
            var message = (IPCMessage)ipcMessage;

            Tuple<MethodInfo, Type> data;

            if (messageHandlers.TryGetValue(message, out data))
            {
                var handlerObj = Activator.CreateInstance(data.Item2, ipcMessage, ipcMessageData) as IPCPacket;

                await Task.Run(() => data.Item1.Invoke(null, new object[] { handlerObj, session }));
            }
            else
            {
                var msgName = Enum.GetName(typeof(IPCMessage), message);

                if (msgName == null)
                    Log.Message(LogTypes.Warning, $"Received unknown ipc message '0x{message:X}'.");
                else
                    Log.Message(LogTypes.Warning, $"Handler for '{msgName} (0x{message:X}) not implemented.");
            }
        }
    }
}
