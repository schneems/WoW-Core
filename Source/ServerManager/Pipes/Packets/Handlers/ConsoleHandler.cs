// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Constants.IPC;
using Framework.Logging;
using Framework.Pipes.Packets;
using ServerManager.Servers;

namespace ServerManager.Pipes.Packets.Handlers
{
    public class ConsoleHandler
    {
        [IPCMessage(IPCMessage.DetachConsole)]
        public static void HandleDetachConsole(DetachConsole detachConsole, IPCSession session)
        {
            ConsoleManager.Detach(detachConsole.Alias);
        }

        [IPCMessage(IPCMessage.ProcessStateInfo)]
        public static void HandleProcessStateInfo(ProcessStateInfo processStateInfo, IPCSession session)
        {
            switch (processStateInfo.State)
            {
                default:
                    Log.Message(LogTypes.Error, $"Received unhandled process state '{processStateInfo.State}' from '{processStateInfo.Alias}'.");
                    break;
            }
        }
    }
}
