// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Net;

namespace AuthServer.Packets.Server.Realm
{
    class ListComplete : ServerPacket
    {
        public ListComplete() : base(AuthServerMessage.ListComplete, AuthChannel.WoWRealm) { }

        public override void Write()
        {
        }
    }
}
