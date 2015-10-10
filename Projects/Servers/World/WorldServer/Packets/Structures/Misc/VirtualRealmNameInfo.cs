// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace WorldServer.Packets.Structures.Misc
{
    public struct VirtualRealmNameInfo : IServerStruct
    {
        public bool IsLocal               { get; set; }
        public string RealmNameActual     { get; set; }
        public string RealmNameNormalized { get; set; }

        public void Write(Packet packet)
        {
            packet.PutBit(IsLocal);
            packet.PutBits(RealmNameActual.Length, 8);
            packet.PutBits(RealmNameNormalized.Length, 8);
            packet.FlushBits();

            packet.WriteString(RealmNameActual);
            packet.WriteString(RealmNameNormalized);
        }
    }
}
