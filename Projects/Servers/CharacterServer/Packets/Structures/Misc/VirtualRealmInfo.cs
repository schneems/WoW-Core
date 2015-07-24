// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Misc
{
    public struct VirtualRealmInfo : IServerStruct
    {
        public uint RealmAddress                  { get; set; }
        public VirtualRealmNameInfo RealmNameInfo { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(RealmAddress);

            RealmNameInfo.Write(packet);
        }
    }
}
