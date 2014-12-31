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
using Framework.Misc;
using Framework.Network.Remoting;
using WorldServer.Network;

namespace WorldServer.Managers
{
    class SessionManager : Singleton<SessionManager>
    {
        public long LastSessionId { get; set; }
        public ConcurrentDictionary<long, WorldSession> Sessions;

        public RemoteObject Remote { get; set; }

        SessionManager()
        {
            Sessions = new ConcurrentDictionary<long, WorldSession>();
        }

        public bool Add(long id, WorldSession session)
        {
            return Sessions.TryAdd(id, session);
        }

        public bool Remove(long id)
        {
            WorldSession session;

            return Sessions.TryRemove(id, out session);
        }
    }
}
