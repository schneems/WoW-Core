// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Sockets;

namespace Framework.Network
{
    public abstract class SessionBase
    {
        public Guid Guid { get; set; }

        public SocketAsyncEventArgs[] Sockets { get; set; }
        public Action<SessionBase> Disconnect { get; set; }

        public abstract void Accept();
        public abstract void Process(object sender, SocketAsyncEventArgs e);

        public string GetClientInfo()
        {
            var endPoint = Sockets[0].AcceptSocket.RemoteEndPoint as IPEndPoint;

            return endPoint != null ? $"{endPoint.Address}:{endPoint.Port}" : "";
        }
    }
}
