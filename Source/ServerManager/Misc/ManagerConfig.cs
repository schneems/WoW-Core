// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;

namespace ServerManager.Misc
{
    public class ManagerConfig
    {
        static bool initialized;
        static Config config;

        #region Config options
        public static LogTypes LogLevel;
        public static string LogDirectory;
        public static string LogConsoleFile;
        public static string LogDatabaseFile;
        public static string LogPacketFile;

        public static string ServerDirectory;

        public static string ConsoleServiceName;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file, true);

            if (config != null)
            {
                LogLevel        = config.Read("Log.Level", LogTypes.Success | LogTypes.Info);
                LogDirectory    = config.Read("Log.Directory", "Logs/Manager");
                LogConsoleFile  = config.Read("Log.Console.File", "");
                LogDatabaseFile = config.Read("Log.Database.File", "");
                LogPacketFile   = config.Read("Log.Packet.File", "");

                LogFile fl = null;

                if (LogConsoleFile != "")
                {
                    if (!Directory.Exists(LogDirectory))
                        Directory.CreateDirectory(LogDirectory);

                    fl = new LogFile(LogDirectory, LogConsoleFile);
                }

                Log.Initialize(LogLevel, fl);

                initialized = true;
            }

            if (initialized)
                ReadConfig();
        }

        static void ReadConfig()
        {
            ServerDirectory = config.Read("Server.Directory", "Servers");

            ConsoleServiceName = config.Read("ConsoleService.Name", "ae/console");
        }
    }
}
