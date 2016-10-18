// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Framework.Constants.IPC;
using Framework.Logging;
using Framework.Pipes.Packets;
using ServerManager.Pipes;

namespace ServerManager.Servers
{
    class ConsoleManager
    {
        public static bool Attached { get; private set; }
        public static Process SelectedChild { get; private set; }
        public static Dictionary<string, Tuple<string, Process>> Childs { get; }

        static readonly Dictionary<string, string> servers;
        static readonly Dictionary<string, IPCSession> consolePipeClients;

        static ConsoleManager()
        {
            Childs = new Dictionary<string, Tuple<string, Process>>();

            servers = new Dictionary<string, string>
            {
                { "bnet", "Servers/bnet.server" },
                { "char", "Servers/char.server" }
            };

            consolePipeClients = new Dictionary<string, IPCSession>();
        }

        public static void AddConsoleClient(string alias, IPCSession session)
        {
            consolePipeClients.Add(alias, session);
        }

        public static void Attach(string alias)
        {
            Tuple<string, Process> child;

            Attached = Childs.TryGetValue(alias, out child);

            if (Attached && child != null)
            {
                // Clear the console.
                Log.Clear();

                // Show current console int title.
                Console.Title = $"Current console: {child.Item1} ({alias})";

                SelectedChild = child.Item2;
                SelectedChild.BeginOutputReadLine();
            }
        }

        public static void Detach(string alias)
        {
            Tuple<string, Process> child;

            if (Childs.TryGetValue(alias, out child))
            {
                // Clear the console.
                Log.Clear();

                child?.Item2.CancelOutputRead();

                SelectedChild = null;
                Attached = false;

                // Show current console int title.
                Console.Title = "Current console: ServerManager";
            }
        }

        public static void Start(string server, string alias, string args)
        {
            if (Childs.ContainsKey(alias))
            {
                Log.Message(LogTypes.Error, $"Server with name '{alias}' already exists.");
                return;
            }

            if (!servers.ContainsKey(server))
            {
                Log.Message(LogTypes.Error, $"Server '{server}' doesn't exists.");
                return;
            }

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo
                {
                    FileName = servers[server],
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };

            process.OutputDataReceived += (sender, obj) =>
            {
                var splitIndex = obj.Data.IndexOf("|");

                LogTypes logTypes;

                if (splitIndex != -1)
                {
                    var splitIndex2 = obj.Data.IndexOf('|', splitIndex + 1) - splitIndex - 1;

                    if (splitIndex2 != -1 && Enum.TryParse(obj.Data.Substring(splitIndex + 1, splitIndex2), out logTypes))
                    {
                        var logMessage = obj.Data.Split(new[] { "|" }, StringSplitOptions.None);

                        if (logTypes != LogTypes.None)
                        {
                            Console.Write($"{logMessage[0]}|");

                            Console.ForegroundColor = Log.Logger.LogTypeInfo[logTypes].Item1;
                            Console.Write(Log.Logger.LogTypeInfo[logTypes].Item2);
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine($"|{logMessage[2]}");
                        }
                        else
                            Console.WriteLine(logMessage[2]);
                    }
                }
            };

            process.Exited += (s, o) =>
            {
                if (Childs.Remove(alias))
                    Log.Message(LogTypes.Error, $"Server '{alias}' exited without shutdown command!!!");
            };

            process.Start();

            Childs.Add(alias, Tuple.Create(servers[server], process));

            Log.Message(LogTypes.Success, $"Started '{servers[server]}' with name '{alias}'.");
        }

        public static void Stop(string alias)
        {
            Tuple<string, Process> process;

            if (Childs.TryGetValue(alias, out process))
            {
                Log.Message(LogTypes.Info, $"Shutting down '{alias}' ({process.Item1})...");

                Childs.Remove(alias);

                // Send exit state to the server.
                var processStateInfo = new ProcessStateInfo
                {
                    Id = process.Item2.Id,
                    Alias = alias,
                    State = ProcessState.Stop
                };

                consolePipeClients[alias].Send(processStateInfo).GetAwaiter().GetResult();

                // Wait for the server to exit.
                process.Item2.WaitForExit();

                Log.Message(LogTypes.Success, "Done.");
            }
            else
                Log.Message(LogTypes.Error, $"Server with '{alias}' doesn't exists.");
        }
    }
}
