// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WorldServer.Constants.Character;
using WorldServer.Constants.Net;
using Framework.Network.Packets;

namespace WorldServer.Packets.Server.Character
{
    class DeleteChar : ServerPacket
    {
        public CharDeleteCode Code { get; set; }

        public DeleteChar() : base(ServerMessage.DeleteChar) { }

        public override void Write()
        {
            Packet.Write(Code);
        }
    }
}
