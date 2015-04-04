// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Framework.Database.Auth.Entities;
using Framework.Network.Packets;

namespace AuthServer.Network.Sessions
{
    class Client : IDisposable
    {
        public long Id { get; set; }
        public AuthSession Session { get; set; }
        public IEnumerable<Module> Modules { get; set; }
        public string ConnectionInfo => Session.GetClientInfo();
        public string Game { get; set; }
        public string OS { get; set; }

        public void SendPacket(AuthPacket packet)
        {
            Session?.Send(packet);
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
