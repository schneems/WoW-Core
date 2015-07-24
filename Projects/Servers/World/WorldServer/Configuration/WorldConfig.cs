// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;
using Lappa_ORM;

namespace WorldServer.Configuration
{

    class WorldConfig
    {
        public static bool IsInitialized = false;
        static Config config;

        #region Config Options
        public static LogType LogLevel;
        public static string LogDirectory;
        public static string LogConsoleFile;
        public static string LogPacketFile;

        public static ConnectionType AuthDBType;

        public static string AuthDBHost;
        public static int AuthDBPort;
        public static string AuthDBUser;
        public static string AuthDBPassword;
        public static string AuthDBDataBase;

        public static int AuthDBMinPoolSize;
        public static int AuthDBMaxPoolSize;

        public static ConnectionType CharacterDBType;

        public static string CharacterDBHost;
        public static int CharacterDBPort;
        public static string CharacterDBUser;
        public static string CharacterDBPassword;
        public static string CharacterDBDataBase;

        public static int CharacterDBMinPoolSize;
        public static int CharacterDBMaxPoolSize;

        public static ConnectionType DataDBType;

        public static string DataDBHost;
        public static int DataDBPort;
        public static string DataDBUser;
        public static string DataDBPassword;
        public static string DataDBDataBase;

        public static int DataDBMinPoolSize;
        public static int DataDBMaxPoolSize;

        public static string BindIP;
        public static int BindPort;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file);

            if (config != null)
            {
                IsInitialized = true;

                LogLevel       = (LogType)config.Read("Log.Level", 0x7, true);
                LogDirectory   = config.Read("Log.Directory", "Logs/World");
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

            AuthDBType             = config.Read("AuthDB.Type", ConnectionType.MYSQL);
            AuthDBHost             = config.Read("AuthDB.Host", "127.0.0.1");
            AuthDBPort             = config.Read("AuthDB.Port", 3306);
            AuthDBUser             = config.Read("AuthDB.User", "root");
            AuthDBPassword         = config.Read("AuthDB.Password", "");
            AuthDBDataBase         = config.Read("AuthDB.Database", "AuthDB");

            AuthDBMinPoolSize      = config.Read("AuthDB.MinPoolSize", 5);
            AuthDBMaxPoolSize      = config.Read("AuthDB.MaxPoolSize", 30);

            CharacterDBType        = config.Read("CharacterDB.Type", ConnectionType.MYSQL);
            CharacterDBHost        = config.Read("CharacterDB.Host", "127.0.0.1");
            CharacterDBPort        = config.Read("CharacterDB.Port", 3306);
            CharacterDBUser        = config.Read("CharacterDB.User", "root");
            CharacterDBPassword    = config.Read("CharacterDB.Password", "");
            CharacterDBDataBase    = config.Read("CharacterDB.Database", "CharacterDB");

            CharacterDBMinPoolSize = config.Read("CharacterDB.MinPoolSize", 5);
            CharacterDBMaxPoolSize = config.Read("CharacterDB.MaxPoolSize", 30);

            DataDBType             = config.Read("DataDB.Type", ConnectionType.MYSQL);
            DataDBHost             = config.Read("DataDB.Host", "127.0.0.1");
            DataDBPort             = config.Read("DataDB.Port", 3306);
            DataDBUser             = config.Read("DataDB.User", "root");
            DataDBPassword         = config.Read("DataDB.Password", "");
            DataDBDataBase         = config.Read("DataDB.Database", "DataDB");

            DataDBMinPoolSize      = config.Read("DataDB.MinPoolSize", 5);
            DataDBMaxPoolSize      = config.Read("DataDB.MaxPoolSize", 30);

            BindIP                 = config.Read("Bind.IP", "0.0.0.0");
            BindPort               = config.Read("Bind.Port", 8100);
        }
    }
}
