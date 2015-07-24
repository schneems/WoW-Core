// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Net;

namespace AuthServer.Network.Packets
{
    public class AuthPacketHeader
    {
        public ushort Message { get; set; }
        public AuthChannel Channel { get; set; }
    }
}
