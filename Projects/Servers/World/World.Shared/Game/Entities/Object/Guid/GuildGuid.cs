// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Object;
using Framework.Objects;

namespace World.Shared.Game.Entities.Object.Guid
{
    class GuildGuid : SmartGuid
    {
        public GuildGuid()
        {
            High = (ulong)GuidType.Guild << 58;
        }

        public ushort RealmId
        {
            get { return (ushort)((High >> 42) & 0x1FFF); }
            set { High |= (ulong)value << 42; }
        }

        public ulong CreationBits
        {
            get { return Low & 0xFFFFFFFFFFFFFFFF; }
            set { Low |= value; }
        }
    }
}
