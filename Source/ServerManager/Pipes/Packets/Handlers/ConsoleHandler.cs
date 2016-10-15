// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Logging;
using Framework.Pipes.Packets;
using ServerManager.Servers;

namespace ServerManager.Pipes.Packets.Handlers
{
    public class ConsoleHandler
    {
        public static void HandleDetachConsole(DetachConsole detachConsole, ConsolePipeSession session)
        {
            ConsoleManager.Detach(detachConsole.Alias);
        }

        public static void HandleProcessStateInfo(ProcessStateInfo processStateInfo, ConsolePipeSession session)
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
