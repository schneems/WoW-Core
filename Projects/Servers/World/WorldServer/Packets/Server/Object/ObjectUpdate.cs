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

using Framework.Network.Packets;
using Framework.Objects;
using World.Shared.Game.Entities;
using World.Shared.Game.Entities.Object;
using WorldServer.Constants.Net;
using WorldServer.Packets.Structures.Object;

namespace WorldServer.Packets.Server.Object
{
    class ObjectUpdate : ServerPacket
    {
        public WorldObjectBase Obj      { get; set; }
        public uint NumObjUpdates       { get; set; }
        public ushort MapId             { get; set; }
        public bool DestroyOrOutOfRange { get; set; }
        public ObjCreate CreateData   { get; } = new ObjCreate();
        public ObjDestroy DestroyData { get; } = new ObjDestroy();

        public ObjectUpdate() : base(ServerMessage.ObjectUpdate) { }

        public override void Write()
        {
            Packet.Write(NumObjUpdates);
            Packet.Write(MapId);

            Packet.PutBit(DestroyOrOutOfRange);
            Packet.FlushBits();

            if (DestroyOrOutOfRange)
                DestroyData.Write(Packet);

            Packet.Write(0);

            if (NumObjUpdates > 0)
            {
                // UpdateType 1 (CreateObject)
                Packet.Write<byte>(1);
                Packet.Write(Obj.Guid);
                Packet.Write<byte>(4);

                CreateData.Write(Packet);

                // Descriptors.
                Obj.WriteToPacket(Packet);

                // Write data length to packet.
                Packet.Write(Packet.Written - 15, 11);
            }
        }
    }
}
