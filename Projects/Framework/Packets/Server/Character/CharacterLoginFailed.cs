// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Character;
using Framework.Constants.Net;
using Framework.Network.Packets;

namespace Framework.Packets.Server.Character
{
    public class CharacterLoginFailed : ServerPacket
    {
        public CharLoginCode Code { get; set; }

        public CharacterLoginFailed() : base(GlobalServerMessage.CharacterLoginFailed) { }

        public override void Write()
        {
            Packet.Write(Code);
        }
    }
}
