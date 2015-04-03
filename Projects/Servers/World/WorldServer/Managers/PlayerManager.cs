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
