// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Objects;

namespace World.Shared.Game.Entities.Object.Guid
{
    class CreatureGuid : SmartGuid
    {
        public ushort RealmId
        {
            get { return (ushort)((High >> 42) & 0x1FFF); }
            set { High |= (ulong)value << 42; }
        }

        public ushort MapId
        {
            get { return (ushort)((High >> 29) & 0x1FFF); }
            set { High |= (ulong)value << 29; }
        }

        public ushort ServerId
        {
            get { return (ushort)((Low >> 40) & 0xFFFF); }
            set { Low |= (ulong)value << 40; }
        }

        public uint Id
        {
            get { return (uint)(High & 0x7FFFFF) >> 6; }
            set { High |= (ulong)value << 6; }
        }

        public ulong CreationBits
        {
            get { return Low & 0xFFFFFFFFFF; }
            set { Low |= value; }
        }
    }
}
