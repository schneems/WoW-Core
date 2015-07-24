// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Framework.Misc;
using Framework.Objects;
using World.Shared.Game.Entities;

namespace WorldServer.Managers
{
    class SpawnManager : Singleton<SpawnManager>
    {
        ConcurrentDictionary<SmartGuid, Creature> Creatures;
        ConcurrentDictionary<SmartGuid, GameObject> GameObjects;

        SpawnManager()
        {
            Creatures = new ConcurrentDictionary<SmartGuid, Creature>();
            GameObjects = new ConcurrentDictionary<SmartGuid, GameObject>();
        }
    }
}
