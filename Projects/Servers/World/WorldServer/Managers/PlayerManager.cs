// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Framework.Logging;
using Framework.Misc;
using Framework.Objects;
using World.Shared.Game.Entities;
using WorldServer.Network;
using WorldServer.Packets.Server.Object;
using WorldServer.Packets.Structures.Movement;

namespace WorldServer.Managers
{
    class PlayerManager : Singleton<PlayerManager>
    {
        ConcurrentDictionary<SmartGuid, Player> ConnectedPlayers;

        PlayerManager()
        {
            ConnectedPlayers = new ConcurrentDictionary<SmartGuid, Player>();
        }

        public async void EnterWorld(WorldSession session)
        {
            var objectUpdate = new ObjectUpdate
            {
                NumObjUpdates = 1,
                MapId = (ushort)session.Player.Map,
                Obj = session.Player
            };

            objectUpdate.CreateData.ThisIsYou = true;

            objectUpdate.CreateData.Move = new MovementUpdate();
            objectUpdate.CreateData.Move.Status.MoverGUID = session.Player.Guid;
            objectUpdate.CreateData.Move.Status.Position = session.Player.Position;
            objectUpdate.CreateData.Move.Status.Facing = session.Player.Facing;

            await session.Send(objectUpdate);

            if (ConnectedPlayers.TryAdd(session.Player.Guid, session.Player))
                Log.Debug($"New Player '{session.Player}' connected to world.");
        }
    }
}
