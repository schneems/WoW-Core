// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Framework.Misc;
using WorldServer.Network;

namespace WorldServer.Managers
{
    class SessionManager : Singleton<SessionManager>
    {
        public long LastSessionId { get; set; }
        public ConcurrentDictionary<long, WorldSession> Sessions;

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
