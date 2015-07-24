// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;
using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Network.Sessions;
using Framework.Logging;
using Framework.Network.Packets;

namespace AuthServer.Network.Packets
{
    class PacketManager
    {
        static readonly ConcurrentDictionary<Tuple<AuthClientMessage, AuthChannel>, HandlePacket> MessageHandlers = new ConcurrentDictionary<Tuple<AuthClientMessage, AuthChannel>, HandlePacket>();
        delegate void HandlePacket(AuthPacket packet, Client client);

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in methodInfo.GetCustomAttributes<AuthMessageAttribute>())
                        MessageHandlers.TryAdd(Tuple.Create(msgAttr.Message, msgAttr.Channel), Delegate.CreateDelegate(typeof(HandlePacket), methodInfo) as HandlePacket);
        }

        public static bool InvokeHandler(AuthPacket reader, Client client)
        {
            var message = (AuthClientMessage)reader.Header.Message;

            Log.Packet($"Received Opcode: {message} (0x{message:X}), Length: {reader.Data.Length}");

            HandlePacket packet;

            if (MessageHandlers.TryGetValue(Tuple.Create(message, reader.Header.Channel), out packet))
            {
                packet.Invoke(reader, client);

                return true;
            }

            return false;
        }
    }
}
