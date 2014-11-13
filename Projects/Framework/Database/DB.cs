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

using Lappa_ORM;

namespace Framework.Database
{
    public class DB
    {
        public static Lappa_ORM.Database Auth = new Lappa_ORM.Database();
        public static Lappa_ORM.Database Character = new Lappa_ORM.Database();
        public static Lappa_ORM.Database Data = new Lappa_ORM.Database();
        public static Lappa_ORM.Database World = new Lappa_ORM.Database();

        public static string CreateConnectionString(string host, string user, string password, string database, int port, bool pooling, int minPoolSize, int maxPoolSize, ConnectionType connType)
        {
            if (connType == ConnectionType.MYSQL)
            {
                var connectionString = "Server=" + host + ";User Id=" + user + ";Port=" + port + ";" +
                                       "Password=" + password + ";Database=" + database + ";Allow Zero Datetime=True;" +
                                       "Pooling=" + pooling + ";CharSet=utf8";

                if (pooling)
                    connectionString += ";Min Pool Size=\{minPoolSize};Max Pool Size=\{maxPoolSize}";

                return connectionString;
            }
            else if (connType == ConnectionType.MSSQL)
            {
                var connectionString = "Data Source=" + host + "; Initial Catalog = " + database + "; User ID = " + user + "; Password = " + password + ";Pooling=" + pooling;

                if (pooling)
                    connectionString += ";Min Pool Size=\{minPoolSize};Max Pool Size=\{maxPoolSize}";

                return connectionString;
            }

            return null;
        }

    }
}
