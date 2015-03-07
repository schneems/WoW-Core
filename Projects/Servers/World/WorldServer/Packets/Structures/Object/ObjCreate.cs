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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Framework.Network.Packets;
using Framework.Objects;

namespace WorldServer.Packets.Structures.Object
{
    class ObjCreate : IServerStruct
    {
        public SmartGuid MoverGUID { get; set; }
        public Vector3 Position    { get; set; }
        public float Facing        { get; set; }

        public void Write(Packet packet)
        {
            packet.PutBit(0);
            packet.PutBit(1);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(1);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(1);
            packet.PutBit(0);
            packet.PutBit(0);
            packet.PutBit(0);                          

            packet.FlushBits();

            packet.Write(0);

            if (true)
            {
                packet.Write(MoverGUID);
                packet.Write(0);
                packet.Write(Position);
                packet.Write(Facing);
                packet.Write<float>(0);
                packet.Write<float>(0);
                packet.Write(0);
                packet.Write(0);

                packet.PutBits(0, 30);
                packet.PutBits(0, 15);

                packet.PutBit(0);
                packet.PutBit(0);
                packet.PutBit(0);
                packet.PutBit(0);
                packet.PutBit(0);

                packet.FlushBits();

                packet.Write(2.5f);
                packet.Write(7f);
                packet.Write(2.5f);
                packet.Write(4.72222f);
                packet.Write(4.5f);
                packet.Write(7f);
                packet.Write(4.5f);
                packet.Write((float)Math.PI);
                packet.Write((float)Math.PI);

                packet.Write(0);

                packet.PutBit(0);
                packet.FlushBits();
            }
        }
    }
}
