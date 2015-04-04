// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Framework.Network.Packets;
using Framework.Objects;

namespace WorldServer.Packets.Structures.Movement
{
    class MovementStatus : IServerStruct
    {
        public SmartGuid MoverGUID { get; set; }
        public uint MoveTime       { get; set; }
        public Vector3 Position    { get; set; }
        public float Facing        { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(MoverGUID);
            packet.Write(MoveTime);
            packet.Write(Position);
            packet.Write(Facing);
            packet.Write<float>(0);
            packet.Write<float>(0);
            packet.Write(0);
            packet.Write(0);

            packet.PutBits(0, 30);
            packet.PutBits(0, 15);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.FlushBits();
        }
    }
}
