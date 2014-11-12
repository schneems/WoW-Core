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

using Framework.Constants.Net;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Net
{
    class ConnectTo : ServerPacket
    {
        public ulong Key    { get; set; }
        public uint Serial  { get; set; }
        public byte[] Where { get; set; } = new byte[256];
        public byte Con     { get; set; }

        public ConnectTo() : base(GlobalServerMessage.ConnectTo) { }

        public override void Write()
        {
            Packet.Write(Key);
            Packet.Write(Serial);
            Packet.Write(Where);
            Packet.Write(Con);
        }
    }
}
