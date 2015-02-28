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

using System.Numerics;
using Framework.Misc;
using World.Shared.Game.Entities.Object.Descriptors;

namespace World.Shared.Game.Entities.Object
{
    abstract class WorldUnitBase : WorldObjectBase
    {
        public static readonly float InRangeDistance = 10000.0f;

        public UnitData UnitData { get; }
        public short Map        { get; set; } = -1;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public float Facing     { get; set; } = 0;

        protected WorldUnitBase(int descriptorLength) : base(descriptorLength)
        {
            UnitData = new UnitData();
        }

        public bool IsInRange(WorldUnitBase unit2) => (Position.Distance(unit2.Position) <= InRangeDistance);
    }
}
