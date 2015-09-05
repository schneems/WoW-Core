// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Framework.Misc;
using WorldNode.Network;

namespace WorldNode.Managers
{
    class SessionManager : Singleton<SessionManager>
    {
        public long LastSessionId { get; set; }
        public ConcurrentDictionary<long, NodeSession> Sessions;

        SessionManager()
        {
            Sessions = new ConcurrentDictionary<long, NodeSession>();
        }

        public bool Add(long id, NodeSession session)
        {
            return Sessions.TryAdd(id, session);
        }

        public bool Remove(long id)
        {
            NodeSession session;

            return Sessions.TryRemove(id, out session);
        }
    }
}
