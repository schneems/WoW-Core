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
using AuthServer.Network;
using Framework.Attributes;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Network.Packets;

namespace AuthServer.Packets
{
    class PacketManager
    {
        static ConcurrentDictionary<Tuple<AuthClientMessage, AuthChannel>, HandlePacket> MessageHandlers = new ConcurrentDictionary<Tuple<AuthClientMessage, AuthChannel>, HandlePacket>();
        delegate void HandlePacket(AuthPacket packet, AuthSession session);

        public static void DefineMessageHandler()
        {
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in methodInfo.GetCustomAttributes<AuthMessageAttribute>())
                        MessageHandlers.TryAdd(Tuple.Create(msgAttr.Message, msgAttr.Channel), Delegate.CreateDelegate(typeof(HandlePacket), methodInfo) as HandlePacket);
        }

        public static bool InvokeHandler(AuthPacket reader, AuthSession session)
        {
            var message = (AuthClientMessage)reader.Header.Message;

            Log.Message(LogType.Packet, "Received Opcode: {0} (0x{0:X}), Length: {1}", message, reader.Data.Length);

            if (MessageHandlers.TryGetValue(Tuple.Create(message, reader.Header.Channel), out HandlePacket packet))
            {
                packet.Invoke(reader, session);

                return true;
            }

            return false;
        }
    }
}
