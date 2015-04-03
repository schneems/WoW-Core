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
using WorldServer.Packets.Structures.Movement;

namespace WorldServer.Packets.Structures.Object
{
    class ObjCreate : IServerStruct
    {
        public bool NoBirthAnim    { get; set; }
        public MovementUpdate Move { get; set; }
        public Position Stationary { get; set; }
        public bool HasRotation    { get; set; }
        public long Rotation       { get; set; }
        public bool ThisIsYou      { get; set; }

        public void Write(Packet packet)
        {
            packet.PutBit(NoBirthAnim);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(Move != null);
            packet.PutBit(false);
            packet.PutBit(Stationary != null);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(HasRotation);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(ThisIsYou);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.FlushBits();

            packet.Write(0);

            if (Move != null)
                Move.Write(packet);

            if (Stationary != null)
                Stationary.Write(packet);

            if (HasRotation)
                packet.Write(Rotation);
        }
    }
}
