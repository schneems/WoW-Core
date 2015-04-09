// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Misc;
using Framework.Objects;
using World.Shared.Game.Entities.Object.Descriptors;

namespace World.Shared.Game.Entities.Object
{
    public abstract class WorldUnitBase : WorldObjectBase
    {
        public static readonly float InRangeDistance = 10000.0f;

        public UnitData UnitData { get; }
        public short Map        { get; set; } = -1;
        public Vector3 Position { get; set; }
        public float Facing     { get; set; } = 0;

        protected WorldUnitBase(int descriptorLength) : base(descriptorLength)
        {
            UnitData = new UnitData();
        }

        public bool IsInRange(WorldUnitBase unit2) => (Position.Distance(unit2.Position) <= InRangeDistance);
    }
}
