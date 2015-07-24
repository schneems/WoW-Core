// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using Framework.Objects;

namespace Framework.Network.Packets
{
    public static class PacketWriterExtensions
    {
        static Dictionary<Type, Action<BinaryWriter, object>> WriteValue = new Dictionary<Type, Action<BinaryWriter, object>>()
        {
            { typeof(bool),      (bw, val) => bw.Write((bool)val)   },
            { typeof(sbyte),     (bw, val) => bw.Write((sbyte)val)  },
            { typeof(byte),      (bw, val) => bw.Write((byte)val)   },
            { typeof(short),     (bw, val) => bw.Write((short)val)  },
            { typeof(ushort),    (bw, val) => bw.Write((ushort)val) },
            { typeof(int),       (bw, val) => bw.Write((int)val)    },
            { typeof(uint),      (bw, val) => bw.Write((uint)val)   },
            { typeof(float),     (bw, val) => bw.Write((float)val)  },
            { typeof(long),      (bw, val) => bw.Write((long)val)   },
            { typeof(ulong),     (bw, val) => bw.Write((ulong)val)  },
            { typeof(double),    (bw, val) => bw.Write((double)val) },
            { typeof(byte[]),    (bw, val) => bw.Write((byte[])val) },
            { typeof(Packet),    (bw, val) => bw.Write(((Packet)val).Data) },
            { typeof(SmartGuid), (bw, val) => WriteSmartGuid(bw, (SmartGuid)val) },
            { typeof(Vector3),   (bw, val) =>
              {
                  var vector = (Vector3)val;

                  bw.Write(vector.X);
                  bw.Write(vector.Y);
                  bw.Write(vector.Z);
              }
            },
        };

        public static void Write<T>(this BinaryWriter bw, T value)
        {
            var type = typeof(T);
            var finalType = type.IsEnum ? type.GetEnumUnderlyingType() : type;

            WriteValue[finalType](bw, value);
        }

        static void WriteSmartGuid(BinaryWriter bw, SmartGuid guid)
        {
            byte loLength, hiLength, wLoLength, wHiLength;

            var loGuid = GetPackedGuid(guid.Low, out loLength, out wLoLength);
            var hiGuid = GetPackedGuid(guid.High, out hiLength, out wHiLength);

            bw.Write(loLength);
            bw.Write(hiLength);
            bw.Write(loGuid, 0, wLoLength);
            bw.Write(hiGuid, 0, wHiLength);
        }

        static byte[] GetPackedGuid(ulong guid, out byte gLength, out byte written)
        {
            var packedGuid = new byte[8];
            byte gLen = 0;
            byte length = 0;

            for (byte i = 0; guid != 0; i++)
            {
                if ((guid & 0xFF) != 0)
                {
                    gLen |= (byte)(1 << i);
                    packedGuid[length] = (byte)(guid & 0xFF);
                    ++length;
                }

                guid >>= 8;
            }

            gLength = gLen;
            written = length;

            return packedGuid;
        }
    }
}
