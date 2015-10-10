// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;

namespace AuthServer.Packets
{
    interface IServerStruct
    {
        void Write(AuthPacket packet);
    }
}
