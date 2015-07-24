// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace Framework.Packets.Client.Authentication
{
    public class TransferInitiate : ClientPacket
    {
        public string Msg { get; private set; }

        public override void Read()
        {
            Msg = Packet.ReadString(Packet.Header.Size);
        }
    }
}
