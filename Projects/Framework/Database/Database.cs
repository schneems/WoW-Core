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
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Framework.Attributes;
using Framework.Misc;
using MySql.Data.MySqlClient;

namespace Framework.Database
{
    public class Database : IDisposable
    {
        MySqlConnection connection;

        public bool CreateConnection(string host, string user, string password, string database, int port, bool pooling, int minPoolSize, int maxPoolSize)
        {
            var pools = string.Format(";Min Pool Size={0};Max Pool Size={1}", minPoolSize, maxPoolSize);

            var connectionString = "Server=" + host + ";User Id=" + user + ";Port=" + port + ";" +
                                   "Password=" + password + ";Database=" + database + ";Allow Zero Datetime=True;" +
                                   "Pooling=" + pooling + ";CharSet=utf8";

            if (pooling)
                connectionString += pools;

            try
            {
                connection = new MySqlConnection(connectionString);

                connection.Open();
            }
            catch
            {
            }

            return connection.State == ConnectionState.Open;
        }

        public bool Execute(string sql, params object[] args)
        {
            var sqlString = new StringBuilder();

            // Fix for floating point problems on some languages
            sqlString.AppendFormat(CultureInfo.GetCultureInfo("en-US").NumberFormat, sql);

            try
            {
                using (var sqlCommand = new MySqlCommand(sqlString.ToString(), connection))
                {
                    var mParams = new List<MySqlParameter>(args.Length);

                    foreach (var a in args)
                        mParams.Add(new MySqlParameter("", a));

                    sqlCommand.Parameters.AddRange(mParams.ToArray());

                    sqlCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        public SQLResult Select(string sql, params object[] args)
        {
            StringBuilder sqlString = new StringBuilder();
            // Fix for floating point problems on some languages
            sqlString.AppendFormat(CultureInfo.GetCultureInfo("en-US").NumberFormat, sql);

            try
            {
                using (var sqlCommand = new MySqlCommand(sqlString.ToString(), connection))
                {
                    var mParams = new List<MySqlParameter>(args.Length);

                    foreach (var a in args)
                        mParams.Add(new MySqlParameter("", a));

                    sqlCommand.Parameters.AddRange(mParams.ToArray());

                    using (var SqlData = sqlCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        using (var retData = new SQLResult())
                        {
                            retData.Load(SqlData);
                            retData.Count = retData.Rows.Count;

                            return retData;
                        }
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        public T Add<T>(T entity) where T : class
        {
            var properties = typeof(T).GetProperties();
            var values = new Dictionary<string, object>(properties.Length);
            var queryBuilder = new StringBuilder();

            for (var i = 0; i < properties.Length; i++)
            {
                var p = properties[i];
                var attr = p.GetCustomAttribute<FieldAttribute>();

                if (attr != null && attr.AutoIncrement)
                    continue;

                var pValue = p.GetValue(entity);

                if (pValue != null)
                    values.Add(p.Name, pValue);
            }

            queryBuilder.AppendFormat("INSERT INTO `{0}` (", typeof(T).Name.Pluralize());

            foreach (var name in values.Keys)
                queryBuilder.AppendFormat("`{0}`,", name);

            queryBuilder.Append(") VALUES (");

            for (var i = 0; i < values.Count; i++)
                queryBuilder.Append("?,");

            queryBuilder.Append(")");

            var query = queryBuilder.ToString().Replace(",)", ")");

            return Execute(query, values.Values.ToArray()) ? entity : null;
        }

        public bool Delete<T>(Expression<Func<T, object>> expression) where T : class
        {
            var bExpression = (expression.Body as UnaryExpression).Operand as BinaryExpression;

            if (bExpression == null)
                throw new NotSupportedException("Only BinaryExpressions are supported.");

            var builder = new QueryBuilder();

            // We support only 1 table for now
            var query = builder.BuildDelete<T>(bExpression, expression.Parameters[0].Name);

            return Execute(query);
        }

        public List<T> Select<T>() where T : new()
        {
            var builder = new QueryBuilder();

            // We support only 1 table for now
            var query = builder.BuildSelect<T>();
            var data = Select(query);
            var properties = typeof(T).GetProperties();

            if (data.Columns.Count != properties.Length)
                throw new NotSupportedException("Columns doesn't match the entity fields.");

            var entities = new List<T>();

            for (var i = 0; i < data.Count; i++)
            {
                var entity = new T();

                for (var j = 0; j < properties.Length; j++)
                {
                    var p = properties[j];
                    var val = data.Rows[i][p.Name];

                    p.SetValue(entity, Convert.ChangeType(val, p.PropertyType), null);
                }

                entities.Add(entity);
            }

            return entities;
        }

        public T Single<T>(Expression<Func<T, object>> expression) where T : new()
        {
            var bExpression = (expression.Body as UnaryExpression).Operand as BinaryExpression;
            var builder = new QueryBuilder();

            // We support only 1 table for now
            var query = builder.BuildWhere<T>(bExpression, expression.Parameters[0].Name);
            var data = Select(query);
            var properties = typeof(T).GetProperties();

            if (data.Count > 1)
                throw new NotSupportedException("Result contains more than 1 element.");

            if (data.Columns.Count != properties.Length)
                throw new NotSupportedException("Columns doesn't match the entity fields.");

            var entity = new T();

            for (var i = 0; i < properties.Length; i++)
            {
                var p = properties[i];
                var val = data.Rows[0][p.Name];

                p.SetValue(entity, Convert.ChangeType(val, p.PropertyType), null);
            }

            return entity;
        }

        public List<T> Where<T>(Expression<Func<T, object>> expression) where T : new()
        {
            var bExpression = (expression.Body as UnaryExpression).Operand as BinaryExpression;
            var builder = new QueryBuilder();

            // We support only 1 table for now
            var query = builder.BuildWhere<T>(bExpression, expression.Parameters[0].Name);
            var data = Select(query);
            var properties = typeof(T).GetProperties();

            if (data.Columns.Count != properties.Length)
                throw new NotSupportedException("Columns doesn't match the entity fields.");

            var entities = new List<T>();

            for (var i = 0; i < data.Count; i++)
            {
                var entity = new T();

                for (var j = 0; j < properties.Length; j++)
                {
                    var p = properties[j];
                    var val = data.Rows[i][p.Name];

                    p.SetValue(entity, Convert.ChangeType(val, p.PropertyType), null);
                }

                entities.Add(entity);
            }

            return entities;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
