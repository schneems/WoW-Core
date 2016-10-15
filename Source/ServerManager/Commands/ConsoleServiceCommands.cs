// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Logging;
using Framework.Misc;
using ServerManager.Servers;

namespace ServerManager.Commands.Console
{
    class ConsoleServiceCommands
    {
        [ConsoleCommand("attach", 1, "Attach...")]
        public static void Attach(CommandArgs args)
        {
            var alias = args.Read<string>();

            ConsoleManager.Attach(alias);
        }

        [ConsoleCommand("start", 2, "Start...")]
        public static void Start(CommandArgs args)
        {
            var server = args.Read<string>();
            var alias = args.Read<string>();

            ConsoleManager.Start(server, alias, $"--alias {alias}");
        }

        [ConsoleCommand("stop", 1, "Stop...")]
        public static void Stop(CommandArgs args)
        {
            var alias = args.Read<string>();

            ConsoleManager.Stop(alias);
        }

        [ConsoleCommand("servers", 0, "Show servers...")]
        public static void Show(CommandArgs args)
        {
            Log.Message(LogTypes.Success, $"Running servers ({ConsoleManager.Childs.Count}):");
            Log.NewLine();

            foreach (var child in ConsoleManager.Childs)
                Log.Message(LogTypes.Info, $"Alias: {child.Key}, Path: {child.Value.Item1}.");
        }
    }
}
