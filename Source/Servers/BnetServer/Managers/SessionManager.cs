// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using BnetServer.Misc;
using BnetServer.Network;
using Framework.Misc;

namespace BnetServer.Managers
{
    public class SessionManager : Singleton<SessionManager>
    {
        public X509Certificate2 Certificate { get; }

        readonly ConcurrentDictionary<Guid, BnetServiceSession> ServiceSessions;

        SessionManager()
        {
            ServiceSessions = new ConcurrentDictionary<Guid, BnetServiceSession>();

            Certificate = new X509Certificate2(BnetConfig.CertificatePath);
        }

        public bool Add(BnetServiceSession session) => ServiceSessions.TryAdd(session.Guid, session);
        public bool Remove(BnetServiceSession session) => ServiceSessions.TryRemove(session.Guid, out session);

        public bool Exists(Guid guid)
        {
            BnetServiceSession dummy;

            return ServiceSessions.TryGetValue(guid, out dummy);
        }

        public bool SetLoginTicket(Guid guid, string loginTicket)
        {
            BnetServiceSession serviceSession;

            if (ServiceSessions.TryGetValue(guid, out serviceSession))
            {
                serviceSession.LoginTicket = loginTicket;

                return true;
            }

            return false;
        }
    }
}
