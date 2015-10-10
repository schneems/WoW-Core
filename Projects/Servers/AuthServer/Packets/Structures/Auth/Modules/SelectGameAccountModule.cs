// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets.Structures.Auth.Modules
{
    class SelectGameAccountModule : AuthModuleBase
    {
        public byte[] GameAccountData { get; set; }

        public override void WriteData(AuthPacket packet)
        {
            packet.Write(GameAccountData);
        }
    }
}
