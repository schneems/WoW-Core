// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Arctium.Core.Configuration;
using Arctium.Core.Database;
using Lappa.ORM.Constants;

namespace Arctium.API.Misc
{
    public class ApiConfig : ConfigBase<ApiConfig>
    {
        [ConfigEntry("API.Bind.Host", "")]
        public static string BindHost;

        [ConfigEntry("API.Bind.Port", 5543)]
        public static int BindPort;

        [ConfigEntry("API.Auth.Enabled", false)]
        public static bool AuthEnabled;

        [ConfigEntry("API.Auth.Type", "")]
        public static string AuthType;

        [ConfigEntry("API.Certificate", "")]
        public static string Certificate;

        [ConfigEntry("Database.Type", DatabaseType.MySql)]
        public static DatabaseType DatabaseType;

        [ConfigEntry("Database.Aurora", default(DatabaseSettings))]
        public static DatabaseSettings AuroraDatabase;
    }
}
