/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;

namespace RemoteServer.Configuration
{
    class RemoteConfig
    {
        public static bool IsInitialized = false;
        static Config config;

        #region Config Options
        public static LogType LogLevel;
        public static string LogDirectory;
        public static string LogConsoleFile;
        public static string LogPacketFile;

        public static string IPCBindIP;
        public static int IPCBindPort;
        #endregion

        public static void Initialize(string file)
        {
            // Initialize exception logger
            if (!Directory.Exists("Crashes"))
                Directory.CreateDirectory("Crashes");

            var el = new LogWriter("Crashes", "RemoteServer.log");

            ExceptionLog.Initialize(el);

            // Initialize unhandled exception handler/logger
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                var ex = (Exception)e.ExceptionObject;

                ExceptionLog.Write(ex);
            };

            config = new Config(file);

            if (config != null)
            {
                IsInitialized = true;

                LogLevel       = (LogType)config.Read("Log.Level", 0x7, true);
                LogDirectory   = config.Read("Log.Directory", "Logs/Remote");
                LogConsoleFile = config.Read("Log.Console.File", "");
                LogPacketFile  = config.Read("Log.Packet.File", "");

                LogWriter fl = null;

                if (LogConsoleFile != "")
                {
                    if (!Directory.Exists(LogDirectory))
                        Directory.CreateDirectory(LogDirectory);

                    fl = new LogWriter(LogDirectory, LogConsoleFile);
                }

                Log.Initialize(LogLevel, fl);

                if (LogPacketFile != "")
                    PacketLog.Initialize(LogDirectory, LogPacketFile);
            }

            ReadConfig();
        }

        static void ReadConfig()
        {
            if (!IsInitialized)
                throw new InvalidOperationException("WorldServer config not initialized.");

            IPCBindIP   = config.Read("Bind.IP", "127.0.0.1");
            IPCBindPort = config.Read("Bind.Port", 9000);
        }
    }
}
