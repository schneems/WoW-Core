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
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.IO
{
    public class LogWriter : IDisposable
    {
        public string LogFile { get; set; }
        FileStream logStream;

        public LogWriter(string directory, string file)
        {
            LogFile = $"{directory}/{DateTime.Now:yyyy-MM-dd_hh-mm-ss}_{file}";

            logStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
        }

        public async Task Write(string logMessage)
        {
            var logBytes = Encoding.Unicode.GetBytes(logMessage + "\r\n");

            await logStream.WriteAsync(logBytes, 0, logBytes.Length);
            await logStream.FlushAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LogFile = "";
                    logStream.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
