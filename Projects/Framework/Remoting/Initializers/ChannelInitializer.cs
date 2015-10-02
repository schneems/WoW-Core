// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Framework.Remoting.Services;

namespace Framework.Remoting.Initializers
{
    public class ChannelInitializer<T> : IChannelInitializer
    {
        internal static int ConnectedClientCount = 0;
        static IService instance;

        public ChannelInitializer(T serverInfo)
        {
            instance = serverInfo as IService;
        }

        public void Initialize(IClientChannel channel)
        {
            channel.Closed += ClientDisconnected;

            // Only use the last part of the full SessionId.
            var sessionInfo = channel.SessionId.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            instance.LastSessionId = uint.Parse(sessionInfo[sessionInfo.Length - 1]);
        }

        static void ClientDisconnected(object sender, EventArgs e)
        {
            // Only use the last part of the full SessionId.
            var sessionInfo = ((IClientChannel)sender).SessionId.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            instance.Unregister(uint.Parse(sessionInfo[sessionInfo.Length - 1]));
        }
    }
}
