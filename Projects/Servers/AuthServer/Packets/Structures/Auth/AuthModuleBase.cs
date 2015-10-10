// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using AuthServer.Network.Packets;
using Framework.Constants.Account;
using Framework.Database.Auth.Entities;
using Framework.Misc;

namespace AuthServer.Packets.Structures.Auth
{
    class AuthModuleBase : IServerStruct
    {
        public Module Data { get; set; }
        public uint Size   { get; set; } = 0;
        public Region AccountRegion { get; set; }

        public void Write(AuthPacket packet)
        {
            packet.WriteFourCC(Data.Type);
            packet.WriteFourCC("\0\0" + Enum.GetName(typeof(Region), AccountRegion));
            packet.Write(Data.Hash.ToByteArray());
            packet.Write(Size == 0 ? Data.Size : Size, 10);

            WriteData(packet);
        }

        public virtual void WriteData(AuthPacket packet) { }
    }
}
