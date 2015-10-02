// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.ServiceModel;
using Framework.Remoting.Objects;

namespace Framework.Remoting.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CharacterService : IService, IServiceCallback
    {
        public uint LastSessionId { get; set; }
        public ConcurrentDictionary<uint, ServerInfoBase> Servers { get; set; }

        ConcurrentDictionary<uint, IServiceCallback> clients;

        public CharacterService()
        {
            Servers = new ConcurrentDictionary<uint, ServerInfoBase>();
            clients = new ConcurrentDictionary<uint, IServiceCallback>();
        }

        public void Register(ServerInfoBase info)
        {
            if (info != null)
            {
                if (info.SessionId == 0)
                    info.SessionId = LastSessionId;

                Servers.AddOrUpdate(info.SessionId, (CharacterServerInfo)info, (k, v) => (CharacterServerInfo)info);
                NotifyClients(info.SessionId, info);
            }
            else
            {
                var callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();

                clients.AddOrUpdate(LastSessionId, callback, (k, v) => callback);
            }
        }

        public void Update(ServerInfoBase info)
        {
            foreach (var s in Servers)
            {
                if (s.Value.Compare(info))
                {
                    info.SessionId = s.Key;

                    Servers[s.Key] = info;

                    NotifyClients(info.SessionId, info);
                    break;
                }
            }
        }

        public void Unregister(uint sessionId)
        {
            ServerInfoBase info;
            Servers.TryRemove(sessionId, out info);

            NotifyClients(sessionId, null);
        }

        public void NotifyClients(uint sessionId, ServerInfoBase info)
        {
            if (info == null)
            {
                IServiceCallback dummy;

                clients.TryRemove(sessionId, out dummy);

                foreach (var c in clients)
                    c.Value.NotifyClients(sessionId, null);
            }
            else
            {
                foreach (var c in clients)
                    c.Value.NotifyClients(sessionId, info);
            }
        }
    }
}
