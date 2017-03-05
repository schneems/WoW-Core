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

        public static string ConsoleBnetServer;
        public static string ConsoleServiceName;

        public static string CertificatePath;

        public static string BnetServiceBindHost;
        public static int BnetServiceBindPort;
        public static int BnetServiceMaxConnections;

        public static string RestServiceBindHost;
        public static int RestServiceBindPort;

        public static string RestServiceHost;
        public static int RestServiceMaxConnections;

        public static DatabaseType BnetServiceDatabaseType;

        public static string BnetServiceDatabaseHost;
        public static int BnetServiceDatabasePort;
        public static string BnetServiceDatabaseUser;
        public static string BnetServiceDatabasePassword;
        public static string BnetServiceDatabaseDataBase;

        public static int BnetServiceDatabaseMinPoolSize;
        public static int BnetServiceDatabaseMaxPoolSize;
        #endregion

        public static void Initialize(string file)
        {
            config = new Config(file, true);

            if (config != null)
            {
                LogLevel        = config.Read("Log.Level", LogTypes.Success | LogTypes.Info);
                LogDirectory    = config.Read("Log.Directory", "Logs/BnetServer");
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
            ConsoleBnetServer = config.Read("ConsoleService.Server", ".");
            ConsoleServiceName   = config.Read("ConsoleService.Name", "ae/console");

            CertificatePath = config.Read("Certificate.Path", "Certificates/dev.pfx");

            BnetServiceBindHost = config.Read("BnetService.Bind.Host", "0.0.0.0");
            BnetServiceBindPort = config.Read("BnetService.Bind.Port", 1119);
            BnetServiceMaxConnections = config.Read("BnetService.MaxConnections", 1000);

            RestServiceBindHost = config.Read("RestService.Bind.Host", "0.0.0.0");
            RestServiceBindPort = config.Read("RestService.Bind.Port", 2229);

            RestServiceHost = config.Read("RestService.Host", "127.0.0.1");
            RestServiceMaxConnections = config.Read("RestService.MaxConnections", 1000);

            BnetServiceDatabaseType     = config.Read("BnetServiceDatabase.Type", DatabaseType.MySql);
            BnetServiceDatabaseHost     = config.Read("BnetServiceDatabase.Host", "127.0.0.1");
            BnetServiceDatabasePort     = config.Read("BnetServiceDatabase.Port", 3306);
            BnetServiceDatabaseUser     = config.Read("BnetServiceDatabase.User", "root");
            BnetServiceDatabasePassword = config.Read("BnetServiceDatabase.Password", "");
            BnetServiceDatabaseDataBase = config.Read("BnetServiceDatabase.Database", "ServiceDatabase");

            BnetServiceDatabaseMinPoolSize = config.Read("BnetServiceDatabase.MinPoolSize", 5);
            BnetServiceDatabaseMaxPoolSize = config.Read("BnetServiceDatabase.MaxPoolSize", 30);
        }
    }
}
