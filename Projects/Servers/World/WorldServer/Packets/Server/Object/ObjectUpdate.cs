// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
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
