// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Objects;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Character
{
    class CharDelete : ClientPacket
    {
        public CharacterGuid Guid { get; set; }

        public override void Read()
        {
            Guid = Packet.ReadGuid<CharacterGuid>();
        }
    }
}
