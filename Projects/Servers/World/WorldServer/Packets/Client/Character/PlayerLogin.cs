// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
using World.Shared.Game.Entities.Object.Guid;

namespace WorldServer.Packets.Client.Character
{
    public class PlayerLogin : ClientPacket
    {
        public PlayerGuid PlayerGUID { get; set; }
        public float FarClip        { get; set; }

        public override void Read()
        {
            PlayerGUID = Packet.ReadGuid<PlayerGuid>();
            FarClip    = Packet.Read<float>();
        }
    }
}
