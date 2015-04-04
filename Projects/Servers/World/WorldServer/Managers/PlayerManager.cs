// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Misc;
using WorldServer.Network;
using WorldServer.Packets.Server.Object;
using WorldServer.Packets.Structures.Movement;

namespace WorldServer.Managers
{
    class PlayerManager : Singleton<PlayerManager>
    {
        PlayerManager()
        {

        }

        public void EnterWorld(WorldSession session)
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

            session.Send(objectUpdate);
        }
    }
}
