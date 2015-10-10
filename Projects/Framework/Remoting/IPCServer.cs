// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ServiceModel;
using Framework.Remoting.Behaviors;

namespace Framework.Remoting
{
    public class IPCServer<T, TBase> where T : class, new()
    {
        ServiceHost serviceHost;

        public IPCServer(string bindIP, string name)
        {
            serviceHost = new ServiceHost(new T(), new Uri($"net.pipe://{bindIP}/{name}"));

            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None)
            {
                // Set 'infinite' timeouts.
                SendTimeout = TimeSpan.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue
            };

            var endpoint = serviceHost.AddServiceEndpoint(typeof(TBase), binding, "");

            endpoint.EndpointBehaviors.Add(new EndpointBehavior<T>(serviceHost.SingletonInstance as T));

            serviceHost.Open();
        }
    }
}
