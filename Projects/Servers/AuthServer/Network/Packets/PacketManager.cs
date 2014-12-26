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
using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Network.Sessions;
using Framework.Constants.Misc;
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

            Log.Message(LogType.Packet, "Received Opcode: \{message} (0x\{message:X}), Length: \{reader.Data.Length}");

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
