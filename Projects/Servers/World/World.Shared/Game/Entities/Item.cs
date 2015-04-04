// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using World.Shared.Game.Entities.Object.Guid;
using World.Shared.Game.Objects.Entities;

namespace World.Shared.Game.Entities
{
    sealed class Item : IWorldObject
    {
        public PlayerGuid Guid { get; }

        public Item()
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
