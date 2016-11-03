// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Framework.Network;

namespace BnetServer.Network
{
    class BnetSocketServer : SocketServerBase<BnetSession>, IDisposable
    {
        public BnetSocketServer(string address, int port, int maxConnections, int bufferSize) : base(address, port, maxConnections, bufferSize)
        {
        }
    }
}
