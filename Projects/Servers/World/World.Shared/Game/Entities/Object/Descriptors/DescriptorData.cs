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
using System.Collections;
using Framework.Misc;
using Framework.Network.Packets;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class DescriptorData
    {
        public Hashtable Data { get; }
        public BitArray Mask { get; }

        byte maskSize;

        public DescriptorData(int descriptorLength)
        {
            Data = new Hashtable(descriptorLength);

            Mask = new BitArray(descriptorLength, false);
            maskSize = (byte)((Mask.Length + 32) / 32);
        }

        public void WriteToPacket(Packet pkt)
        {
            pkt.Write(maskSize);

            var length = maskSize << 2;
            var maskArray = new byte[((Mask.Length + 8) / 8) + 1];

            if (length > maskArray.Length)
                maskArray = maskArray.Combine(new byte[length - maskArray.Length]);

            Mask.CopyTo(maskArray, 0);

            pkt.WriteBytes(maskArray, length);

            for (var i = 0; i < Mask.Count; i++)
            {
                if (Mask.Get(i) && Data.ContainsKey(i))
                {
                    if (Data[i] is uint)
                        pkt.Write((uint)Data[i]);
                    else if (Data[i] is float)
                        pkt.Write((float)Data[i]);
                    else
                        pkt.Write((int)Data[i]);
                }
            }

            Mask.SetAll(false);

            // Dynamic Descriptors...
            pkt.Write<byte>(0);
        }
    }
}
