// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
