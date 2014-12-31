/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Framework.Constants.Misc;
using Framework.Logging.IO;

namespace Framework.Logging.IO
{
    public class ExceptionLog
    {
        static BlockingCollection<string> logQueue = new BlockingCollection<string>();

        public static async void Initialize(LogWriter fileLogger = null)
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

        public static void Write(Exception ex)
        {
            var sb = new StringBuilder();

            sb.Append("Time: [\{DateTime.Now}]");
            sb.AppendLine();

            sb.Append("Message: \{ex.Message}");
            sb.AppendLine();

            sb.Append("StackTrace: \{ex.StackTrace}");
            sb.AppendLine();
            sb.AppendLine();

            logQueue.Add(sb.ToString());
        }
    }
}
