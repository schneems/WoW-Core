// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Framework.Logging;

namespace Framework.Network
{
    public class SocketServerBase<TSession> where TSession : SessionBase, IDisposable, new()
    {
        public bool IsListening { get; private set; }

        public int ActiveConnections => activeConnections;

        public IPAddress Address { get; }
        public int Port { get; }
        public int MaxConnections { get; }
        public int BufferSize { get; }

        Socket serverSocket;
        SocketAsyncEventArgs acceptEventArgs;
        ConcurrentStack<SocketAsyncEventArgs> sockets;

        int activeConnections;
        bool acceptConnections;

        public SocketServerBase(string address, int port, int maxConnections, int bufferSize)
        {
            IPAddress ipAddress;

            if (!IPAddress.TryParse(address, out ipAddress))
            {
                Log.Message(LogTypes.Error, $"'{address}' is not a valid IP address");
                return;
            }

            Address = ipAddress;
            Port = port;
            MaxConnections = maxConnections;
            BufferSize = bufferSize;

            sockets = new ConcurrentStack<SocketAsyncEventArgs>();

            // Create read/write sockets.
            for (var i = 0; i < MaxConnections * 2; i++)
            {
                var socketEventArgs = new SocketAsyncEventArgs();

                socketEventArgs.SetBuffer(new byte[BufferSize], 0, BufferSize);

                sockets.Push(socketEventArgs);
            }
        }

        public bool Start(int acceptDelay = 10)
        {
            try
            {
                serverSocket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                serverSocket.Bind(new IPEndPoint(Address, Port));
                serverSocket.Listen(MaxConnections);

                acceptEventArgs = new SocketAsyncEventArgs();
                acceptEventArgs.Completed += async (sender, args) => await ProcessAcceptAsync();

                new Thread(AcceptAsync) { IsBackground = true }.Start(acceptDelay);

                IsListening = true;
            }
            catch (Exception ex)
            {
                Log.Message(LogTypes.Error, ex.Message);

                IsListening = false;
            }

            return IsListening;
        }

        async void AcceptAsync(object acceptDelay)
        {
            acceptConnections = true;

            while (acceptConnections)
            {
                await Task.Delay((int)acceptDelay);

                // Check for pending connections.
                if (serverSocket.Poll(0, SelectMode.SelectRead))
                {
                    // Reset the accept socket for each new connection.
                    acceptEventArgs.AcceptSocket = null;

                    if (!serverSocket.AcceptAsync(acceptEventArgs))
                        await ProcessAcceptAsync();
                }
            }

            if (!disposedValue)
            {
                while (!acceptConnections)
                    await Task.Delay((int)acceptDelay);

                // Restart the AcceptAsync loop after 'StartAcceptConnections' call.
                AcceptAsync(acceptDelay);
            }
        }

        async Task ProcessAcceptAsync()
        {
            // Check if the socket was at least connected one time.
            if (acceptEventArgs.AcceptSocket?.Connected == true)
            {
                var readWriteEventArgs = new SocketAsyncEventArgs[2];

                // Get one event arg object for reading and one for writing.
                if (sockets.TryPopRange(readWriteEventArgs) == readWriteEventArgs.Length)
                {
                    // Increment the current connection count.
                    Interlocked.Increment(ref activeConnections);

                    // Assign the client socket to the read/write socket event args.
                    readWriteEventArgs[0].AcceptSocket = acceptEventArgs.AcceptSocket;
                    readWriteEventArgs[1].AcceptSocket = acceptEventArgs.AcceptSocket;

                    await CreateSession(readWriteEventArgs);
                }
                else
                {
                    acceptEventArgs.AcceptSocket.Dispose();

                    Log.Message(LogTypes.Info, "Maximum number of client connections has been reached.");
                }
            }
        }

        public virtual Task CreateSession(SocketAsyncEventArgs[] sockets)
        {
            return Task.Factory.StartNew(new TSession
            {
                Guid = Guid.NewGuid(),
                Disconnect = DeleteSession,
                Sockets = sockets
            }.Accept);
        }

        public void DeleteSession(SessionBase session)
        {
            // Dispose the client socket.
            session.Sockets[0].AcceptSocket?.Dispose();
            session.Sockets[1].AcceptSocket?.Dispose();

            // Put the free sockets back into the stack.
            sockets.PushRange(session.Sockets);

            // Decrement the current connection count.
            Interlocked.Decrement(ref activeConnections);
        }

        // Restarts the accept connection loop. No effect without calling 'StopAcceptConnections' before.
        public void StartAcceptConnections()
        {
            acceptConnections = true;
        }

        public void StopAcceptConnections()
        {
            acceptConnections = false;
        }

        public void Stop()
        {
            StopAcceptConnections();

            serverSocket.Dispose();
        }

        #region IDisposable Support
        bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
