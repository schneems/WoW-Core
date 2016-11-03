// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Framework.Logging;
using Framework.Logging.IO;
using Framework.Misc;
using LappaORM.Constants;

namespace BnetServer.Misc
{
    class BnetConfig
    {
        static bool initialized;
        static Config config;

        #region Config Options
        public static LogTypes LogLevel;
        public static string LogDirectory;
        public static string LogConsoleFile;
        public static string LogDatabaseFile;
        public static string LogPacketFile;

        public static string ConsoleServiceServer;
        public static string ConsoleServiceName;

        public static string CertificatePath;

        public static string BnetBindHost;
        public static int BnetBindPort;
        public static int BnetMaxConnections;

        public static string BnetChallengeBindHost;
        public static int BnetChallengeBindPort;

        public static string BnetChallengeHost;
        public static int BnetChallengeMaxConnections;

        public static DatabaseType AuthDBType;

        public static string AuthDBHost;  
        public static int AuthDBPort; 
        public static string AuthDBUser; 
        public static string AuthDBPassword; 
        public static string AuthDBDataBase; 

        public static int AuthDBMinPoolSize; 
        public static int AuthDBMaxPoolSize;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file, true);

            if (config != null)
            {
                LogLevel        = config.Read("Log.Level", LogTypes.Success | LogTypes.Info);
                LogDirectory    = config.Read("Log.Directory", "Logs/Bnet");
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
            ConsoleServiceServer = config.Read("ConsoleService.Server", ".");
            ConsoleServiceName   = config.Read("ConsoleService.Name", "ae/console");

            CertificatePath = config.Read("Certificate.Path", "Certificates/dev.pfx");

            BnetBindHost = config.Read("Bnet.Bind.Host", "0.0.0.0");
            BnetBindPort = config.Read("Bnet.Bind.Port", 1119);
            BnetMaxConnections = config.Read("Bnet.MaxConnections", 1000);

            BnetChallengeBindHost = config.Read("BnetChallenge.Bind.Host", "0.0.0.0");
            BnetChallengeBindPort = config.Read("BnetChallenge.Bind.Port", 2229);

            BnetChallengeHost = config.Read("BnetChallenge.Host", "127.0.0.1");
            BnetChallengeMaxConnections = config.Read("BnetChallenge.MaxConnections", 1000);

            AuthDBType     = config.Read("AuthDB.Type", DatabaseType.MySql);
            AuthDBHost     = config.Read("AuthDB.Host", "127.0.0.1");
            AuthDBPort     = config.Read("AuthDB.Port", 3306);
            AuthDBUser     = config.Read("AuthDB.User", "root");
            AuthDBPassword = config.Read("AuthDB.Password", "");
            AuthDBDataBase = config.Read("AuthDB.Database", "AuthDB");

            AuthDBMinPoolSize = config.Read("AuthDB.MinPoolSize", 5);
            AuthDBMaxPoolSize = config.Read("AuthDB.MaxPoolSize", 30);
        }
    }
}
