// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Framework.Remoting.Initializers;

namespace Framework.Remoting.Behaviors
{
    public class EndpointBehavior<T> : IEndpointBehavior
    {
        T instance;

        public EndpointBehavior(T instance)
        {
            this.instance = instance;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // Not needed
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // Not needed
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ChannelInitializers.Add(new ChannelInitializer<T>(instance));
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // Not needed
        }
    }
}
