// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Misc;
using Framework.Pipes.Packets;

namespace BnetServer.Console.Commands
{
    public class ConsoleServiceCommands
    {
        [ConsoleCommand("detach", 0, "Detach...")]
        public static void Detach(CommandArgs args)
        {
            BnetServer.ConsoleClient.Send(new DetachConsole { Alias = BnetServer.Alias }).GetAwaiter().GetResult();
        }
    }
}
