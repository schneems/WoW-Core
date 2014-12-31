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

using Framework.Network.Packets;
using Framework.Objects;

namespace Framework.Packets.Client.Character
{
    public class PlayerLogin : ClientPacket
    {
        public SmartGuid PlayerGUID { get; set; }
        public float FarClip        { get; set; }

        public override void Read()
        {
            PlayerGUID = Packet.Read<SmartGuid>();
            FarClip    = Packet.Read<float>();
        }
    }
}
