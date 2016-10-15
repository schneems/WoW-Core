// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Framework.Logging.IO;

namespace Framework.Logging
{
    public class Logger
    {
        public bool Initialized { get; private set; }
        public Dictionary<LogTypes, Tuple<ConsoleColor, string>> LogTypeInfo { get; }

        readonly BlockingCollection<Tuple<LogTypes, string, string>> logQueue;
        LogTypes logLevel;

        public Logger()
        {
            LogTypeInfo = new Dictionary<LogTypes, Tuple<ConsoleColor, string>>
            {
                { LogTypes.None,    Tuple.Create(ConsoleColor.White, "") },
                { LogTypes.Success, Tuple.Create(ConsoleColor.Green, " Success ") },
                { LogTypes.Info,    Tuple.Create(ConsoleColor.DarkGreen, " Info    ") },
                { LogTypes.Warning, Tuple.Create(ConsoleColor.Yellow, " Warning ") },
                { LogTypes.Error,   Tuple.Create(ConsoleColor.Red, " Error   ") },
                { LogTypes.Input,   Tuple.Create(ConsoleColor.Gray, " Input   ") },
            };

            logQueue = new BlockingCollection<Tuple<LogTypes, string, string>>();
        }

        public void Initialize(LogTypes logTypes, LogFile logFile = null)
        {
            Console.CancelKeyPress += (o, e) => e.Cancel = true;
            Console.OutputEncoding = Encoding.UTF8;

            logLevel = logTypes;

            var logThread = new Thread(async () =>
            {
                while (true)
                {
                    Thread.Sleep(1);

                    Tuple<LogTypes, string, string> log;

                    if (!logQueue.TryTake(out log))
                        continue;

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"{log.Item2} |");

                    Console.ForegroundColor = LogTypeInfo[log.Item1].Item1;
                    Console.Write(LogTypeInfo[log.Item1].Item2);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine($"| {log.Item3}");

                    if (logFile != null)
                        await logFile.WriteAsync($"{log.Item2} |{LogTypeInfo[log.Item1].Item2}| {log.Item3}");
                }
            });

            logThread.IsBackground = true;
            logThread.Start();

            Initialized = true;
        }

        public void Message(LogTypes logType, string text)
        {
            SetLogger(logType, text);
        }

        public void NewLine()
        {
            logQueue.Add(Tuple.Create(LogTypes.None, "", Environment.NewLine));
        }

        public void WaitForKey()
        {
            Console.ReadKey(true);
        }

        public void Clear() => Console.Clear();

        void SetLogger(LogTypes type, string text)
        {
            if ((logLevel & type) == type)
            {
                if (type.Equals(LogTypes.None))
                    logQueue.Add(Tuple.Create(type, "", text));
                else
                    logQueue.Add(Tuple.Create(type, DateTime.Now.ToString("T"), text));
            }
        }
    }
}
