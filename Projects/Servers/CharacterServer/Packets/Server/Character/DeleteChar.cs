// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Constants.Character;
using CharacterServer.Constants.Net;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Character
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
