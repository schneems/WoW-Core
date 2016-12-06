// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Constants.IPC;
using Framework.Logging;
using Framework.Pipes.Packets;

namespace ServerManager.Pipes.Packets.Handlers
{
    public class ConsoleServiceHandler
    {
        [IPCMessage(IPCMessage.RegisterConsole)]
        public static void HandleRegisterConsole(RegisterConsole registerConsole, IPCSession session)
        {
            ConsoleManager.AddConsoleClient(registerConsole.Alias, session);
        }

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
                // TODO: Implement on childs. Called in ConsoleManager.Stop for now.
                case ProcessState.Stopped:
                    ConsoleManager.RemoveConsoleClient(processStateInfo.Alias);
                    break;
                default:
                    Log.Message(LogTypes.Error, $"Received unhandled process state '{processStateInfo.State}' from '{processStateInfo.Alias}'.");
                    break;
            }
        }
    }
}
