// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CharacterServer.Constants.Net;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Misc
{
    class TutorialFlags : ServerPacket
    {
        public TutorialFlags() : base(ServerMessage.TutorialFlags) { }

        public override void Write()
        {
            // ToDo: Use correct data.
            for (var i = 0; i < 32; i++)
                Packet.Write<byte>(0);
        }
    }
}
