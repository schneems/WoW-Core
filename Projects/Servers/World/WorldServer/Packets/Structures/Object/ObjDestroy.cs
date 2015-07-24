// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Network.Packets;
using World.Shared.Game.Entities.Object;

namespace WorldServer.Packets.Structures.Object
{
    class ObjDestroy : IServerStruct
    {
        public ushort MapId { get; set; }
        public List<WorldObjectBase> Objects { get; } = new List<WorldObjectBase>();

        public void Write(Packet packet)
        {
            packet.Write(MapId);
            packet.Write(Objects.Count);

            Objects.ForEach(o => packet.Write(o.Guid));
        }
    }
}
