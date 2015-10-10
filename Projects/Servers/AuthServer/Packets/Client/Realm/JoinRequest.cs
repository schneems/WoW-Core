// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Packets.Client.Realm
{
    class JoinRequest : ClientPacket
    {
        public uint ClientSalt { get; private set; }

        public override void Read()
        {
            ClientSalt = Packet.Read<uint>(32);
        }
    }
}
