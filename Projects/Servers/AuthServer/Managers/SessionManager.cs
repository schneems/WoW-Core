// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using AuthServer.Network.Sessions;
using Framework.Database;
using Framework.Misc;

namespace AuthServer.Managers
{
    class SessionManager : Singleton<SessionManager>
    {
        public long LastSessionId { get; set; }
        public ConcurrentDictionary<long, Client> Clients { get; set; }

        SessionManager()
        {
            Clients = new ConcurrentDictionary<long, Client>();


            IsInitialized = true;
        }

        public void RemoveClient(long id)
        {
            var client = Clients[id];
            var session = client.Session;

            if (session.GameAccount != null)
            {
                session.GameAccount.IsOnline = false;

                DB.Auth.Update(session.GameAccount, "IsOnline");

                Manager.SessionMgr.Clients.TryRemove(id, out client);

                client.Dispose();
            }
        }
    }
}
