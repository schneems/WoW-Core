// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using Framework.Attributes;
using Framework.Logging;
using Framework.Misc;
using ServerManager.Servers;

namespace ServerManager.Commands
{
    public class CommandManager
    {
        static ConcurrentDictionary<string, HandleCommand> Commands = new ConcurrentDictionary<string, HandleCommand>();
        delegate void HandleCommand(CommandArgs args);

        public static void InitializeCommands()
        {
            var currentAsm = Assembly.GetEntryAssembly();

            foreach (var type in currentAsm.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    foreach (var commandAttr in methodInfo.GetCustomAttributes<ConsoleCommandAttribute>())
                        if (commandAttr != null)
                            Commands[commandAttr.Command] = (HandleCommand)methodInfo.CreateDelegate(typeof(HandleCommand), null);
                }
            }
        }

        public static void StartCommandHandler()
        {
            while (true)
            {
                Thread.Sleep(1);

                var sLine = System.Console.ReadLine();

                if (ConsoleManager.Attached)
                    ConsoleManager.SelectedChild.StandardInput.WriteLine(sLine);
                else
                {
                    var line = sLine?.Split(new[] { " " }, StringSplitOptions.None);

                    if (line?.Length > 0)
                    {
                        var cmd = line[0].ToLower();
                        var args = line.Skip(1).ToArray();

                        HandleCommand command;

                        if (Commands.TryGetValue(cmd, out command))
                        {
                            var argCount = command.GetMethodInfo().GetCustomAttribute<ConsoleCommandAttribute>().Arguments;

                            if (args.Length == argCount)
                                command.Invoke(new CommandArgs(args));
                            else
                                Log.Message(LogTypes.Error, $"Wrong arguments for '{cmd}' command.");
                        }
                    }
                }
            }
        }
    }
}
