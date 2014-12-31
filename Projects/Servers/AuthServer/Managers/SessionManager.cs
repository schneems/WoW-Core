/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Concurrent;
using AuthServer.Network.Sessions;
using Framework.Database;
using Framework.Database.Auth.Entities;
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
