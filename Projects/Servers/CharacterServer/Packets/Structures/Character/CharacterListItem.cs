// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Character
{
    struct CharacterListItem : IServerStruct
    {
        public uint DisplayID        { get; set; }
        public uint DisplayEnchantID { get; set; }
        public byte InvType          { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(DisplayID);
            packet.Write(DisplayEnchantID);
            packet.Write(InvType);
        }
    }
}
