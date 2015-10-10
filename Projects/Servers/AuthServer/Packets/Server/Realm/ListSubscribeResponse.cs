// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using AuthServer.Constants.Net;

namespace AuthServer.Packets.Server.Realm
{
    class ListSubscribeResponse : ServerPacket
    {
        public List<ListUpdate> ListUpdates { get; } = new List<ListUpdate>();

        public ListSubscribeResponse() : base(AuthServerMessage.ListSubscribeResponse, AuthChannel.WoWRealm) { }

        public override void Write()
        {
            Packet.Write(0, 1);
            Packet.Write(0, 7);

            // Write realm info.
            ListUpdates.ForEach(lu =>
            {
                lu.Write();
                lu.Packet.Finish();

                Packet.Write(lu.Packet.Data);
            });

            // Finish the packet.
            var listComplete = new ListComplete();

            listComplete.Packet.Finish();

            Packet.Write(listComplete.Packet.Data);
        }
    }
}
