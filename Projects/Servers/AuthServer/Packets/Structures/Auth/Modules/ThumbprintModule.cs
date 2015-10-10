// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets.Structures.Auth.Modules
{
    class ThumbprintModule : AuthModuleBase
    {
        public byte[] ModuleData { get; set; }

        public override void WriteData(AuthPacket packet)
        {
            packet.Write(ModuleData);
        }
    }
}
