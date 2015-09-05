// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace World.Shared.Game.Objects.Entities
{
    public interface IWorldObject
    {
        void InitializeDescriptors();
        void InitializeDynamicDescriptors();
    }
}
