// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets.Structures.Auth
{
    class RegulatorInfo : IServerStruct
    {
        public int Threshold { get; set; }
        public int Rate      { get; set; }

        public void Write(AuthPacket packet)
        {
            packet.Write(Threshold, 32);
            packet.Write(Rate, 32);
        }
    }
}
