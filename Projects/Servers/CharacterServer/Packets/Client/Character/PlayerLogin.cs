// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Objects;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Character
{
    public class PlayerLogin : ClientPacket
    {
        public CharacterGuid PlayerGUID { get; set; }
        public float FarClip        { get; set; }

        public override void Read()
        {
            PlayerGUID = Packet.ReadGuid<CharacterGuid>();
            FarClip    = Packet.Read<float>();
        }
    }
}
