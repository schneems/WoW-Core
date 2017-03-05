// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bgs.Protocol;
using BnetServer.Attributes;
using BnetServer.Constants.Service;
using BnetServer.Network;
using Framework.Logging;
using Framework.Misc;
using Google.Protobuf;

namespace BnetServer.Packets
{
    public class BnetPacketManager : Singleton<BnetPacketManager>
    {
        readonly ConcurrentDictionary<BnetServiceHash, Dictionary<uint, Tuple<MethodInfo, Type>>> serviceHandlers;

        BnetPacketManager()
        {
            serviceHandlers = new ConcurrentDictionary<BnetServiceHash, Dictionary<uint, Tuple<MethodInfo, Type>>>();

            var assembly = Assembly.GetEntryAssembly();

            foreach (var type in assembly.GetTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<BnetServiceAttribute>() != null))
            {
                var ServiceMethods = new Dictionary<uint, Tuple<MethodInfo, Type>>();

                foreach (var method in type.GetMethods().Where(m => m.GetCustomAttribute<BnetServiceMethodAttribute>() != null))
                {
                    var methodAttributeInfo = method.GetCustomAttribute<BnetServiceMethodAttribute>();

                    ServiceMethods.Add(methodAttributeInfo.MethodId, Tuple.Create(method, method.GetParameters()[0].ParameterType));
                }

                var serviceAttributeInfo = type.GetTypeInfo().GetCustomAttribute<BnetServiceAttribute>();

                if (serviceHandlers.TryAdd(serviceAttributeInfo.Hash, ServiceMethods))
                {
                    Log.Message(LogTypes.Info, $"Registered service handlers for {serviceAttributeInfo.Hash}:");

                    foreach (var m in ServiceMethods)
                        Log.Message(LogTypes.Info, $"- Id: {m.Key}, Name: {m.Value.Item1.Name}");
                }
            }
        }

        public async Task CallHandler(Header header, byte[] messageData, BnetServiceSession session)
        {
            Dictionary<uint, Tuple<MethodInfo, Type>> ServiceMethods;

            if (serviceHandlers.TryGetValue((BnetServiceHash)header.ServiceHash, out ServiceMethods))
            {
                Tuple<MethodInfo, Type> method;

                if (ServiceMethods.TryGetValue(header.MethodId, out method))
                {
                    var message = Activator.CreateInstance(method.Item2) as IMessage;

                    message.MergeFrom(messageData);

                    if (method.Item1.ReturnType == typeof(Task))
                        await Task.FromResult(method.Item1.Invoke(null, new object[] { message, session }));
                    else
                        method.Item1.Invoke(null, new object[] { message, session });
                }
                else
                    Log.Message(LogTypes.Error, $"Got unhandled method id '{header.MethodId}' for '{(BnetServiceHash)header.ServiceHash}'.");
            }
            else
            {
                if (!Enum.IsDefined(typeof(BnetServiceHash), header.ServiceHash))
                    Log.Message(LogTypes.Error, $"Got unknown service '{header}'.");
                else
                    Log.Message(LogTypes.Error, $"Got unhandled service '{(BnetServiceHash)header.ServiceHash}'.");
            }
        }
    }
}
