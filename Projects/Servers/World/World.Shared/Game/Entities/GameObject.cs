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
using System.Numerics;
using World.Shared.Game.Entities.Object;
using World.Shared.Game.Entities.Object.Descriptors;
using World.Shared.Game.Entities.Object.Guid;
using World.Shared.Game.Objects.Entities;

namespace World.Shared.Game.Entities
{
    sealed class GameObject : WorldObjectBase, IWorldObject
    {
        public short Map        { get; set; } = -1;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public float Facing     { get; set; } = 0;
        public long Rotation
        {
            get
            {
                var z = (float)Math.Sin(Facing / 1.9999945);
                var com = (long)(z / Math.Atan(Math.Pow(2, -20)));

                return Facing < Math.PI ? com : ((0x100000 - com) << 1) + com;
            }
        }

        public GameObject() : base(GameObjectData.End)
        {
            throw new NotImplementedException();
        }

        public void InitializeDescriptors()
        {
            throw new NotImplementedException();
        }

        public void InitializeDynamicDescriptors()
        {
            throw new NotImplementedException();
        }

        
    }
}
