// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
using World.Shared.Game.Entities.Object.Guid;

namespace WorldServer.Packets.Client.Character
{
    class CharDelete : ClientPacket
    {
        public PlayerGuid Guid { get; set; }

        public override void Read()
        {
            Guid = Packet.ReadGuid<PlayerGuid>();
        }
    }
}
