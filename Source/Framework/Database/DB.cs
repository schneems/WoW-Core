// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using LappaORM.Constants;

public static class Database
{
    public static LappaORM.Database Bnet = new LappaORM.Database();

    public static string CreateConnectionString(string host, string user, string password, string database, int port, int minPoolSize, int maxPoolSize, DatabaseType dbType)
    {
        if (dbType == DatabaseType.MySql)
            return $"Server={host};User Id={user};Port={port};Password={password};Database={database};Allow Zero Datetime=True;Pooling=True;Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize};CharSet=utf8";

        if (dbType == DatabaseType.MSSql)
            return $"Data Source={host}; Initial Catalog = {database}; User ID = {user}; Password = {password};Pooling=True;Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize}";

        return null;
    }
}
