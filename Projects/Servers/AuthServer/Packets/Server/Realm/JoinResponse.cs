// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using AuthServer.Constants.Net;

namespace AuthServer.Packets.Server.Realm
{
    class JoinResponse : ServerPacket
    {
        public int RealmCount { get; set; }
        public uint ServerSalt { get; set; }
        public List<Tuple<byte[], byte[]>> RealmInfo { get; } = new List<Tuple<byte[], byte[]>>();

        public JoinResponse() : base(AuthServerMessage.JoinResponse, AuthChannel.WoWRealm) { }

        public override void Write()
        {
            Packet.Write(RealmCount == 0, 1);
            Packet.Write(ServerSalt, 32);

            Packet.Write(RealmCount, 5);

            RealmInfo.ForEach(ri =>
            {
                Packet.Write(ri.Item1); // IP
                Packet.Write(ri.Item2); // Port
            });

            // Battlenet::WoW::IP6AddressList, not implemented
            Packet.Write(0, 5);
        }
    }
}
