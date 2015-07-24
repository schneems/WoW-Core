// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
using Framework.Objects;

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
