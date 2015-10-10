// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using AuthServer.Constants.Net;
using AuthServer.Packets.Structures.Auth;
using Framework.Constants.Account;

namespace AuthServer.Packets.Server.Auth
{
    class ProofRequest : ServerPacket
    {
        public Region AccountRegion { get; set; }
        public List<AuthModuleBase> Modules { get; private set; } = new List<AuthModuleBase>();

        public ProofRequest() : base(AuthServerMessage.ProofRequest) { }

        public override void Write()
        {
            Packet.Write(Modules.Count, 3);

            Modules.ForEach(m =>
            {
                m.AccountRegion = AccountRegion;

                m.Write(Packet);
            });

        }
    }
}
