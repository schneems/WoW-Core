// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;
using Lappa_ORM;

namespace AuthServer.Configuration
{
    class AuthConfig
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

        public static string BindIP;
        public static int BindPort;

        public static int RealmListUpdateTime;

        public static string PatchFileDirectory;

        public static string CharacterServiceHost;
        public static string CharacterServiceName;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file);

            if (config != null)
            {
                IsInitialized = true;

                LogLevel         = (LogType)config.Read("Log.Level", 0x7, true);
                LogDirectory     = config.Read("Log.Directory", "Logs/Auth");
                LogConsoleFile   = config.Read("Log.Console.File", "");
                LogPacketFile    = config.Read("Log.Packet.File", "");

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
                throw new InvalidOperationException("AuthServer config not initialized.");

            AuthDBType          = config.Read("AuthDB.Type", ConnectionType.MySql);
            AuthDBHost          = config.Read("AuthDB.Host", "127.0.0.1");
            AuthDBPort          = config.Read("AuthDB.Port", 3306);
            AuthDBUser          = config.Read("AuthDB.User", "root");
            AuthDBPassword      = config.Read("AuthDB.Password", "");
            AuthDBDataBase      = config.Read("AuthDB.Database", "AuthDB");

            AuthDBMinPoolSize   = config.Read("AuthDB.MinPoolSize", 5);
            AuthDBMaxPoolSize   = config.Read("AuthDB.MaxPoolSize", 30);

            BindIP              = config.Read("Bind.IP", "0.0.0.0");
            BindPort            = config.Read("Bind.Port", 1119);

            RealmListUpdateTime = config.Read("RealmList.UpdateTime", 5) * 60000;

            PatchFileDirectory  = config.Read("Patch.File.Directory", "PatchFiles");

            CharacterServiceHost = config.Read("CharacterService.Host", "127.0.0.1");
            CharacterServiceName = config.Read("CharacterService.Name", "CharacterService");
        }
    }
}
