// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets.Structures.Misc
{
    class Component : IClientStruct
    {
        public string Program  { get; set; }
        public string Platform { get; set; }
        public int Version     { get; set; }

        public void Read(AuthPacket packet)
        {
            Program  = packet.ReadFourCC();
            Platform = packet.ReadFourCC();
            Version  = packet.Read<int>(32);
        }
    }
}
