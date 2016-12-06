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

        readonly ConcurrentDictionary<Guid, BnetSession> bnetSessions;

        SessionManager()
        {
            bnetSessions = new ConcurrentDictionary<Guid, BnetSession>();

            Certificate = new X509Certificate2(BnetConfig.CertificatePath);
        }

        public bool Add(BnetSession session) => bnetSessions.TryAdd(session.Guid, session);
        public bool Remove(BnetSession session) => bnetSessions.TryRemove(session.Guid, out session);

        public bool Exists(Guid guid)
        {
            BnetSession dummy;

            return bnetSessions.TryGetValue(guid, out dummy);
        }

        public bool SetLoginTicket(Guid guid, string loginTicket)
        {
            BnetSession bnetSessiom;

            if (bnetSessions.TryGetValue(guid, out bnetSessiom))
            {
                bnetSessiom.LoginTicket = loginTicket;

                return true;
            }

            return false;
        }
    }
}
