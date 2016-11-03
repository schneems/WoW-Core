// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Constants.IPC;
using Framework.Logging;
using Framework.Pipes;
using Framework.Pipes.Packets;

namespace BnetServer.Pipes.Packets.Handlers
{
    public class ConsoleServiceHandler
    {
        [IPCMessage(IPCMessage.ProcessStateInfo)]
        public static void HandleProcessStateInfo(ProcessStateInfo processStateInfo, IPCClientBase client)
        {
            switch (processStateInfo.State)
            {
                case ProcessState.Stop:
                    BnetServer.Shutdown();
                    break;
                default:
                    Log.Message(LogTypes.Error, $"Received unhandled process state '{processStateInfo.State}' from '{processStateInfo.Alias}'.");
                    break;
            } 
        }
    }
}
