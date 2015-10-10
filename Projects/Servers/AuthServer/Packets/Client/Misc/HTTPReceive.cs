// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Packets.Client.Misc
{
    class HTTPReceive : ClientPacket
    {
        public string Path { get; private set; }

        public override void Read()
        {
            Packet.ReadString();

            Path = Packet.ReadString();
        }
    }
}
