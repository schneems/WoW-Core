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
using System.IO;
using Framework.Objects;

namespace Framework.Network.Packets
{
    public static class PacketReaderExtensions
    {
        static Dictionary<Type, Func<BinaryReader, object>> ReadValue = new Dictionary<Type, Func<BinaryReader, object>>()
        {
            { typeof(bool),      br => br.ReadBoolean() },
            { typeof(sbyte),     br => br.ReadSByte()   },
            { typeof(byte),      br => br.ReadByte()    },
            { typeof(char),      br => br.ReadChar()    },
            { typeof(short),     br => br.ReadInt16()   },
            { typeof(ushort),    br => br.ReadUInt16()  },
            { typeof(int),       br => br.ReadInt32()   },
            { typeof(uint),      br => br.ReadUInt32()  },
            { typeof(float),     br => br.ReadSingle()  },
            { typeof(long),      br => br.ReadInt64()   },
            { typeof(ulong),     br => br.ReadUInt64()  },
            { typeof(double),    br => br.ReadDouble()  },
            { typeof(SmartGuid), br => ReadSmartGuid(br) }
        };

        public static T Read<T>(this BinaryReader br)
        {
            var type = typeof(T);
            var finalType = type.IsEnum ? type.GetEnumUnderlyingType() : type;

            return (T)ReadValue[finalType](br);
        }

        static SmartGuid ReadSmartGuid(BinaryReader br)
        {
            var smartGuid = new SmartGuid();
            var loLength = br.ReadByte();
            var hiLength = br.ReadByte();

            var guid = 0ul;

            for (var i = 0; i < 8; i++)
                if ((1 << i & loLength) != 0)
                    guid |= (ulong)br.ReadByte() << (i * 8);

            smartGuid.Low = guid;

            guid = 0;

            for (var i = 0; i < 8; i++)
                if ((1 << i & hiLength) != 0)
                    guid |= (ulong)br.ReadByte() << (i * 8);

            smartGuid.High = guid;

            return smartGuid;
        }
    }
}
