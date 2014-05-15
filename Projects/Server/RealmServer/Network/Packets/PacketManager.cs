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
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Network.Packets;
using RealmServer.Attributes;
using RealmServer.Constants.Net;

namespace RealmServer.Network.Packets
{
    class PacketManager
    {
        static ConcurrentDictionary<ClientMessages, HandlePacket> MessageHandlers = new ConcurrentDictionary<ClientMessages, HandlePacket>();
        delegate void HandlePacket(Packet packet, RealmSession session);

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in methodInfo.GetCustomAttributes<MessageAttribute>())
                        MessageHandlers.TryAdd(msgAttr.Message, Delegate.CreateDelegate(typeof(HandlePacket), methodInfo) as HandlePacket);
        }

        public static bool InvokeHandler(Packet reader, RealmSession session)
        {
            var message = (ClientMessages)reader.Header.Message;

            Log.Message(LogType.Packet, "Received Opcode: {0} (0x{0:X}), Length: {1}", message, reader.Data.Length);

            if (MessageHandlers.TryGetValue(message, out HandlePacket packet))
            {
                packet.Invoke(reader, session);

                return true;
            }

            return false;
        }
    }
}
