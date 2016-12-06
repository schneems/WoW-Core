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
using BnetServer.Constants.Bnet;
using BnetServer.Network;
using Framework.Logging;
using Framework.Misc;
using Google.Protobuf;

namespace BnetServer.Packets
{
    public class BnetPacketManager : Singleton<BnetPacketManager>
    {
        readonly ConcurrentDictionary<BnetServiceHash, Dictionary<uint, Tuple<MethodInfo, Type>>> bnetHandlers;

        BnetPacketManager()
        {
            bnetHandlers = new ConcurrentDictionary<BnetServiceHash, Dictionary<uint, Tuple<MethodInfo, Type>>>();

            var assembly = Assembly.GetEntryAssembly();

            foreach (var type in assembly.GetTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<BnetServiceAttribute>() != null))
            {
                var bnetMethods = new Dictionary<uint, Tuple<MethodInfo, Type>>();

                foreach (var method in type.GetMethods().Where(m => m.GetCustomAttribute<BnetMethodAttribute>() != null))
                {
                    var methodAttributeInfo = method.GetCustomAttribute<BnetMethodAttribute>();

                    bnetMethods.Add(methodAttributeInfo.MethodId, Tuple.Create(method, method.GetParameters()[0].ParameterType));
                }

                var serviceAttributeInfo = type.GetTypeInfo().GetCustomAttribute<BnetServiceAttribute>();

                if (bnetHandlers.TryAdd(serviceAttributeInfo.Hash, bnetMethods))
                {
                    Log.Message(LogTypes.Info, $"Registered Bnet handlers for {serviceAttributeInfo.Hash}:");

                    foreach (var m in bnetMethods)
                        Log.Message(LogTypes.Info, $"- Id: {m.Key}, Name: {m.Value.Item1.Name}");
                }
            }
        }

        public async Task CallHandler(Header header, byte[] messageData, BnetSession session)
        {
            Dictionary<uint, Tuple<MethodInfo, Type>> bnetMethods;

            if (bnetHandlers.TryGetValue((BnetServiceHash)header.ServiceHash, out bnetMethods))
            {
                Tuple<MethodInfo, Type> method;

                if (bnetMethods.TryGetValue(header.MethodId, out method))
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
                    Log.Message(LogTypes.Error, $"Got unknown Bnet service '{header}'.");
                else
                    Log.Message(LogTypes.Error, $"Got unhandled Bnet service '{(BnetServiceHash)header.ServiceHash}'.");
            }
        }
    }
}
