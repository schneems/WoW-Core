// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Net;

namespace AuthServer.Packets.Server.Net
{
    class Pong : ServerPacket
    {
        public Pong() : base(AuthServerMessage.Pong) { }

        public override void Write()
        {
        }
    }
}
