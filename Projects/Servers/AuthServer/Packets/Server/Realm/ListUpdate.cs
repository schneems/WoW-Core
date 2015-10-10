// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Net;

namespace AuthServer.Packets.Server.Realm
{
    class ListUpdate : ServerPacket
    {
        public Framework.Database.Auth.Entities.Realm RealmInfo { get; set; }
        public int Index { get; set; }

        public ListUpdate() : base(AuthServerMessage.ListUpdate, AuthChannel.WoWRealm) { }

        public override void Write()
        {
            Packet.Write(true, 1);
            Packet.Write(RealmInfo.Category, 32);          // RealmCategory
            Packet.Write(0, 32);                           // RealmPopulation, float written as uint32
            Packet.Write(RealmInfo.State, 8);              // RealmState
            Packet.Write(RealmInfo.Id, 19);                // RealmId
            Packet.Write(0x80000000 + RealmInfo.Type, 32); // RealmType
            Packet.WriteString(RealmInfo.Name, 10, false); // RealmName
            Packet.Write(false, 1);                        // Battlenet::VersionString, not used for now
            Packet.Write(RealmInfo.Flags, 8);              // RealmInfoFlags
            Packet.Write(0, 8);
            Packet.Write(0, 12);
            Packet.Write(0, 8);
            Packet.Write(Index, 32);
        }
    }
}
