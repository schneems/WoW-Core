// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Misc
{
    public struct RaceClassAvailability : IServerStruct
    {
        public byte RaceOrClassID     { get; set; }
        public byte RequiredExpansion { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(RaceOrClassID);
            packet.Write(RequiredExpansion);
        }
    }
}
