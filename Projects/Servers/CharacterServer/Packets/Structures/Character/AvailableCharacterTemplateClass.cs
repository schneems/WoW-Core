// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Character
{
    public struct AvailableCharacterTemplateClass : IServerStruct
    {
        public byte ClassID      { get; set; }
        public byte FactionGroup { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(ClassID);
            packet.Write(FactionGroup);
        }
    }
}
