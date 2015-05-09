// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.IO
{
    public class ExceptionLog
    {
        static BlockingCollection<string> logQueue = new BlockingCollection<string>();
        static bool initialized;

        static async Task Initialize(LogWriter fileLogger = null)
        {
            await Task.Delay(1).ContinueWith(async _ =>
            {
                while (true)
                {
                    var log = logQueue.Take();

                    if (log != null && fileLogger != null)
                        await fileLogger.Write(log);
                }
            });
        }

        public static async void Write(Exception ex)
        {
            if (!initialized)
            {
                // Initialize exception logger
                if (!Directory.Exists("Crashes"))
                    Directory.CreateDirectory("Crashes");

                var el = new LogWriter("Crashes", "Crash.log");

                await Initialize(el);

                initialized = true;
            }

            var sb = new StringBuilder();

            sb.Append($"Time: [{DateTime.Now}]");
            sb.AppendLine();

            sb.Append($"Message: {ex.Message}");
            sb.AppendLine();

            sb.Append($"StackTrace: {ex.StackTrace}");
            sb.AppendLine();
            sb.AppendLine();

            logQueue.Add(sb.ToString());
        }
    }
}
