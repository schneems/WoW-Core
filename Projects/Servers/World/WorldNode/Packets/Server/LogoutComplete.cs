// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
using Framework.Objects;
using WorldNode.Constants.Net;

namespace WorldNode.Packets.Server
{
    class LogoutComplete : ServerPacket
    {
        public SmartGuid SwitchToCharacter { get; set; }

        public LogoutComplete() : base(ServerMessage.LogoutComplete) { }

        public override void Write()
        {
            // Disabled
            //Packet.Write(SwitchToCharacter);
        }
    }
}
