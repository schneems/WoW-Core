// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO.Pipes;
using System.Threading;

namespace Framework.Pipes
{
    public class IPCServer<TSession> : IDisposable where TSession : IPCSessionBase, new()
    {
        public string PipeName { get; }

        readonly NamedPipeServerStream pipeServerStream;
        bool initialized;

        public IPCServer(string pipeName)
        {
            PipeName = pipeName;

            pipeServerStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 100, PipeTransmissionMode.Byte, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
        }

        public void Start()
        {
            new Thread(AcceptAsync).Start();
        }

        async void AcceptAsync()
        {
            while (true)
            {
                // Accept one new IPC connection per 100 ms.
                Thread.Sleep(100);

                // Called on first connection only.
                if (!initialized)
                {
                    if (pipeServerStream == null)
                        break;

                    await pipeServerStream.WaitForConnectionAsync();

                    if (pipeServerStream.IsConnected)
                    {
                        initialized = true;

                        new TSession().Process(pipeServerStream);
                    }
                }
                else
                {
                    var pipe = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 100, PipeTransmissionMode.Byte, PipeOptions.WriteThrough | PipeOptions.Asynchronous);

                    await pipe.WaitForConnectionAsync();

                    if (pipe.IsConnected)
                        new TSession().Process(pipe);
                }
            }
        }

        public void Dispose()
        {
            pipeServerStream.Dispose();

            initialized = false;
        }
    }
}
