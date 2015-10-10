// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Net;
using AuthServer.Network.Packets;

namespace AuthServer.Packets
{
    public abstract class ServerPacket
    {
        public AuthPacket Packet { get; private set; }

        protected ServerPacket()
        {
            Packet = new AuthPacket();
        }

        protected ServerPacket(AuthServerMessage netMessage, AuthChannel channel = AuthChannel.Authentication)
        {
            Packet = new AuthPacket(netMessage, channel);
        }

        public abstract void Write();
    }
}
