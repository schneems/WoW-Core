// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        public static bool IsInitialized;
        static Config config;

        #region Config Options
        public static LogType LogLevel;
        public static string LogDirectory;
        public static string LogConsoleFile;

        public static string CharacterServiceName;
        public static string CharacterServiceBindIP;

        public static string WorldServiceName;
        public static string WorldServiceBindIP;

        public static string NodeServiceName;
        public static string NodeServiceBindIP;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file);

            if (config != null)
            {
                IsInitialized = true;

                LogLevel       = (LogType)config.Read("Log.Level", 0x7, true);
                LogDirectory   = config.Read("Log.Directory", "Logs/Remote");
                LogConsoleFile = config.Read("Log.Console.File", "");

                LogWriter fl = null;

                if (LogConsoleFile != "")
                {
                    if (!Directory.Exists(LogDirectory))
                        Directory.CreateDirectory(LogDirectory);

                    fl = new LogWriter(LogDirectory, LogConsoleFile);
                }

                Log.Initialize(LogLevel, fl);
            }

            ReadConfig();
        }

        static void ReadConfig()
        {
            if (!IsInitialized)
                throw new InvalidOperationException("CharacterServer config not initialized.");

            CharacterServiceName   = config.Read("CharacterService.Name", "CharacterService");
            CharacterServiceBindIP = config.Read("CharacterService.Bind.IP", "0.0.0.0");

            WorldServiceName   = config.Read("WorldService.Name", "WorldService");
            WorldServiceBindIP = config.Read("WorldService.Bind.IP", "0.0.0.0");

            NodeServiceName   = config.Read("NodeService.Name", "NodeService");
            NodeServiceBindIP = config.Read("NodeService.Bind.IP", "0.0.0.0");
        }
    }
}
