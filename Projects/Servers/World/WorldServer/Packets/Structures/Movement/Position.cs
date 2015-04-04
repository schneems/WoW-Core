// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Framework.Network.Packets;

namespace WorldServer.Packets.Structures.Movement
{
    class Position : IServerStruct
    {
        public Vector3 Pos  { get; set; }
        public float Facing { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(Pos);
            packet.Write(Facing);
        }
    }
}
