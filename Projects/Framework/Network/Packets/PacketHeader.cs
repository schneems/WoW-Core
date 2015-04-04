// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Network.Packets
{
    public class PacketHeader
    {
        public ushort Message { get; set; }
        public ushort Size    { get; set; }
    }
}
