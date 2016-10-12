// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Logging.IO;

namespace Framework.Logging
{
    public class Log
    {
        public static Logger Logger;

        public static void Initialize(LogTypes logTypes, LogFile logFile = null)
        {
            Logger = Logger ?? new Logger();
            Logger.Initialize(logTypes, logFile);
        }

        public static void Message(LogTypes logType, string text)
        {
            Logger.Message(logType, text);
        }

        public static void NewLine()
        {
            Logger.NewLine();
        }

        public static void WaitForKey()
        {
            Logger.WaitForKey();
        }

        public static void Clear() => Logger.Clear();
    }
}
