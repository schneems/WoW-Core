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

using System;
using System.Data.Common;
using Framework.Database.Auth;
using Framework.Database.Character;
using Framework.Database.World;
using MySql.Data.Entity;

namespace Framework.Database
{
    public class DB
    {
        public static AuthDB Auth;
        public static CharacterDB Character;
        public static WorldDB World;

        public static void Initialize<T>(out T context, DbConnection conn)
        {
            context = (T)Activator.CreateInstance(typeof(T), conn);
        }

        public static DbConnection CreateConnection(string host, string user, string password, string database, int port, bool pooling, int minPoolSize, int maxPoolSize)
        {
            var pools = string.Format(";Min Pool Size={0};Max Pool Size={1}", minPoolSize, maxPoolSize);

            var connectionString = "Server=" + host + ";User Id=" + user + ";Port=" + port + ";" +
                                   "Password=" + password + ";Database=" + database + ";Allow Zero Datetime=True;" +
                                   "Pooling=" + pooling + ";CharSet=utf8";

            if (pooling)
                connectionString += pools;

            var factory = new MySqlConnectionFactory();

            return factory.CreateConnection(connectionString);
        }
    }
}
