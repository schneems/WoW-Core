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
using Framework.Network.Packets;
using Framework.Objects;
using WorldServer.Constants.Net;

namespace WorldServer.Packets.Server.Misc
{
    class AccountDataTimes : ServerPacket
    {
        public SmartGuid PlayerGuid { get; set; }
        public uint[] AccountTimes { get; } = new uint[8];

        public AccountDataTimes() : base(ServerMessage.AccountDataTimes) { }

        public override void Write()
        {
            Packet.Write(PlayerGuid);
            Packet.Write((uint)DateTimeOffset.Now.ToUnixTimeSeconds());

            for (var i = 0; i < AccountTimes.Length; i++)
                Packet.Write(AccountTimes[i]);
        }
    }
}
