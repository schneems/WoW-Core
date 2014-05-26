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
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Constants.Misc;
using Framework.Logging;

namespace Framework.Database
{
    public abstract class Database : DbContext
    {
        protected Database(IDbConnection conn) : base(conn as DbConnection, true)
        {
            Configuration.AutoDetectChangesEnabled = true;
        }

        public bool Add<T>(T entity) where T : class
        {
            try
            {
                var table = Set<T>();

                if (table != null)
                    table.Add(entity);

                return SaveChangesAsync().Result > 0;
            }
            catch (Exception ex)
            {
                Log.Message(LogType.Error, "{0}", ex.Message);
            }

            return false;
        }

        public bool Update()
        {
            try
            {
                return SaveChangesAsync().Result > 0;
            }
            catch (Exception ex)
            {
                Log.Message(LogType.Error, "{0}", ex.Message);
            }

            return false;
        }

        public bool Remove<T>(T entity) where T : class
        {
            try
            {
                var table = Set<T>();

                if (table != null)
                    table.Remove(entity);

                return SaveChangesAsync().Result > 0;
            }
            catch (Exception ex)
            {
                Log.Message(LogType.Error, "{0}", ex.Message);
            }

            return false;
        }
    }
}
