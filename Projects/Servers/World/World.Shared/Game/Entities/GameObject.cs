// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Framework.Objects;
using World.Shared.Game.Entities.Object;
using World.Shared.Game.Entities.Object.Descriptors;
using World.Shared.Game.Objects.Entities;

namespace World.Shared.Game.Entities
{
    sealed class GameObject : WorldObjectBase, IWorldObject
    {
        public short Map        { get; set; } = -1;
        public Vector3 Position { get; set; }
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
