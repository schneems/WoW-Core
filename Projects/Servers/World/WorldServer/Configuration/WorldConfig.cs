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

        public static bool AuthDBPooling;
        public static int AuthDBMinPoolSize;
        public static int AuthDBMaxPoolSize;

        public static ConnectionType CharacterDBType;

        public static string CharacterDBHost;
        public static int CharacterDBPort;
        public static string CharacterDBUser;
        public static string CharacterDBPassword;
        public static string CharacterDBDataBase;

        public static bool CharacterDBPooling;
        public static int CharacterDBMinPoolSize;
        public static int CharacterDBMaxPoolSize;

        public static ConnectionType DataDBType;

        public static string DataDBHost;
        public static int DataDBPort;
        public static string DataDBUser;
        public static string DataDBPassword;
        public static string DataDBDataBase;

        public static bool DataDBPooling;
        public static int DataDBMinPoolSize;
        public static int DataDBMaxPoolSize;

        public static string BindIP;
        public static int BindPort;
        #endregion

        public static void Initialize(string file)
        {
            // Initialize exception logger
            if (!Directory.Exists("Crashes"))
                Directory.CreateDirectory("Crashes");

            var el = new LogWriter("Crashes", "WorldServer.log");

            ExceptionLog.Initialize(el);

            // Initialize unhandled exception handler/logger
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                try
                {
                    var ex = (Exception)e.ExceptionObject;

                    ExceptionLog.Write(ex);
                }
                catch (Exception)
                {
                    throw;
                }
            };

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

            AuthDBPooling          = config.Read("AuthDB.Pooling", true);
            AuthDBMinPoolSize      = config.Read("AuthDB.MinPoolSize", 5);
            AuthDBMaxPoolSize      = config.Read("AuthDB.MaxPoolSize", 30);

            CharacterDBType        = config.Read("CharacterDB.Type", ConnectionType.MYSQL);
            CharacterDBHost        = config.Read("CharacterDB.Host", "127.0.0.1");
            CharacterDBPort        = config.Read("CharacterDB.Port", 3306);
            CharacterDBUser        = config.Read("CharacterDB.User", "root");
            CharacterDBPassword    = config.Read("CharacterDB.Password", "");
            CharacterDBDataBase    = config.Read("CharacterDB.Database", "CharacterDB");

            CharacterDBPooling     = config.Read("CharacterDB.Pooling", true);
            CharacterDBMinPoolSize = config.Read("CharacterDB.MinPoolSize", 5);
            CharacterDBMaxPoolSize = config.Read("CharacterDB.MaxPoolSize", 30);

            DataDBType             = config.Read("DataDB.Type", ConnectionType.MYSQL);
            DataDBHost             = config.Read("DataDB.Host", "127.0.0.1");
            DataDBPort             = config.Read("DataDB.Port", 3306);
            DataDBUser             = config.Read("DataDB.User", "root");
            DataDBPassword         = config.Read("DataDB.Password", "");
            DataDBDataBase         = config.Read("DataDB.Database", "DataDB");

            DataDBPooling          = config.Read("DataDB.Pooling", false);
            DataDBMinPoolSize      = config.Read("DataDB.MinPoolSize", 1);
            DataDBMaxPoolSize      = config.Read("DataDB.MaxPoolSize", 1);

            BindIP                 = config.Read("Bind.IP", "0.0.0.0");
            BindPort               = config.Read("Bind.Port", 8100);
        }
    }
}
