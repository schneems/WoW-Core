// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Authentication
{
    public struct AuthWaitInfo : IServerStruct
    {
        public uint WaitCount { get; set; }
        public bool HasFCM    { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(WaitCount);
            packet.PutBit(HasFCM);
            packet.FlushBits();
        }
    }
}
