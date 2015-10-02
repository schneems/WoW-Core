// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.ServiceModel;
using Framework.Logging;
using Framework.Remoting.Objects;
using Framework.Remoting.Services;

namespace Framework.Remoting
{
    public class IPCClient : IService, IServiceCallback
    {
        public uint LastSessionId { get; set; }
        public ConcurrentDictionary<uint, ServerInfoBase> Servers { get; set; }

        IService proxy;
        IClientChannel proxyChannel;

        public IPCClient(string serverIP, string hostName)
        {
            var factory = new DuplexChannelFactory<IService>(new InstanceContext(this), new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), new EndpointAddress($"net.pipe://{serverIP}/{hostName}"));

            proxy = factory.CreateChannel();
            proxyChannel = proxy as IClientChannel;

            proxyChannel.Opened += (o, e) => Servers = proxy.Servers;

            proxyChannel.Open();
        }

        public void Register(ServerInfoBase info)
        {
            proxy.Register(info);
        }

        public void Update(ServerInfoBase info)
        {
            proxy.Update(info);
        }

        public void Unregister(uint sessionId)
        {
            proxy.Unregister(sessionId);
        }

        public void NotifyClients(uint sessionId, ServerInfoBase info)
        {
            if (info == null)
            {
                Servers.TryRemove(sessionId, out info);

                Log.Debug($"CharacterServer (Realm: {info.RealmId}, Host: {info.IPAddress}, Port: {info.Port}) disconnected.");
            }
            else
            {
                Servers.AddOrUpdate(sessionId, info, (k, v) => info);

                Log.Debug($"CharacterServer (Realm: {info.RealmId}, Host: {info.IPAddress}, Port: {info.Port}) connected/updated.");
            }
        }
    }
}
