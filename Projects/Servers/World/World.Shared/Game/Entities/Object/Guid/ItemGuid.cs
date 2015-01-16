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
