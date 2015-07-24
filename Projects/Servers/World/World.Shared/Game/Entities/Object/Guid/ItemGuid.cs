// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Object;
using Framework.Objects;

namespace World.Shared.Game.Entities.Object.Guid
{
    class ItemGuid : SmartGuid
    {
        public ItemGuid()
        {
            High = (ulong)GuidType.Item << 58;
        }

        public ushort RealmId
        {
            get { return (ushort)((High >> 42) & 0x1FFF); }
            set { High |= (ulong)value << 42; }
        }

        public uint Id
        {
            get { return (uint)(High & 0xFFFFFF) >> 18; }
            set { High |= (ulong)value << 18; }
        }

        public ulong CreationBits
        {
            get { return Low & 0xFFFFFFFFFFFFFFFF; }
            set { Low |= value; }
        }

    }
}
