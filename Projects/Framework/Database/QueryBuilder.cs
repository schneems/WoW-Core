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

using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Misc;

namespace Framework.Database
{
    public class QueryBuilder : ExpressionVisitor
    {
        StringBuilder sqlString;

        public string BuildSelect<T>()
        {
            sqlString = new StringBuilder();

            // ToDo: Add support for column selection
            sqlString.AppendFormat("SELECT * FROM `{0}`", typeof(T).Name.Pluralize());

            return sqlString.ToString();
        }

        public string BuildWhere<T>(Expression expression, string param)
        {
            sqlString = new StringBuilder();

            // ToDo: Add support for column selection
            sqlString.AppendFormat("SELECT * FROM `{0}` `{1}` WHERE ", typeof(T).Name.Pluralize(), param);

            Visit(expression);

            return sqlString.ToString();
        }

        public string BuildDelete<T>(Expression expression, string param)
        {
            sqlString = new StringBuilder();

            sqlString.AppendFormat("DELETE FROM `{1}` USING `{0}` AS `{1}` WHERE ", typeof(T).Name.Pluralize(), param);

            Visit(expression);

            return sqlString.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression bExpression)
        {
            sqlString.Append("(");

            Visit(bExpression.Left);

            var condition = "";

            switch (bExpression.NodeType)
            {
                case ExpressionType.Equal:
                    condition = " = ";
                    break;
                case ExpressionType.NotEqual:
                    condition = " <> ";
                    break;
                case ExpressionType.GreaterThan:
                    condition = " > ";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    condition = " >= ";
                    break;
                case ExpressionType.LessThan:
                    condition = " < ";
                    break;
                case ExpressionType.LessThanOrEqual:
                    condition = " <= ";
                    break;
                case ExpressionType.AndAlso:
                    condition = " AND ";
                    break;
                case ExpressionType.OrElse:
                    condition = " OR ";
                    break;
                default:
                    condition = "Not Supported";
                    break;
            }

            if (condition == " AND " || condition == " OR ")
                sqlString.Append(condition);
            else
            {
                if (bExpression.Right.NodeType == ExpressionType.MemberAccess)
                {
                    // ToDo: Handle other member types.
                    var memberExp = bExpression.Right as MemberExpression;
                    var val =(memberExp.Member as FieldInfo).GetValue((memberExp.Expression as ConstantExpression).Value);

                    sqlString.AppendFormat("{0}{1}'{2}'", Regex.Replace(bExpression.Left.ToString(), @"^Convert\(|\)$", ""), condition, val);
                }
                else if (bExpression.Right.NodeType == ExpressionType.Convert)
                {
                    var memberExp = (bExpression.Right as UnaryExpression).Operand as MemberExpression;
                    var val = (memberExp.Member as FieldInfo).GetValue((memberExp.Expression as ConstantExpression).Value);

                    sqlString.AppendFormat("{0}{1}'{2}'", Regex.Replace(bExpression.Left.ToString(), @"^Convert\(|\)$", ""), condition, val);
                }
                else
                    sqlString.AppendFormat("{0}{1}'{2}'", Regex.Replace(bExpression.Left.ToString(), @"^Convert\(|\)$", ""), condition, Regex.Replace(bExpression.Right.ToString(), "^\"|\"$", ""));
            }

            Visit(bExpression.Right);

            sqlString.Append(")");

            return bExpression;
        }
    }
}
