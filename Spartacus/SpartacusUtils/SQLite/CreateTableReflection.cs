using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dapper;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.SQLite
{
    public static partial class CreateTableReflection
    {
        /// <summary>
        /// Drops a table if it exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static int DropTableIfExists<T>(this IDbConnection conn)
        {
            var type = typeof(T);
            var tableName = GetTableNameAttribute(type);
            return conn.Execute($"DROP TABLE IF EXISTS \"{tableName}\"");
        }


        /// <summary>
        /// Creates a SQLite table schema from a class.
        /// The <see cref="Object"/> property types will be defined as an BLOB
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CreateTableSchema(Type type)
        {
            var properties = GetAllProperties(type);
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray();
            if (!propertyInfos.Any())
                return null;

            var tableName = GetTableNameAttribute(type);

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE \"{tableName}\" (");

            propertyInfos.ForEach(propertyInfo =>
            {
                var name = GetColumnName(propertyInfo);
                if (name != null)
                    sb.AppendLine($"{name.TrimEnd()}{(propertyInfos.LastOrDefault() == propertyInfo ? "" : ",")}");
            });

            sb.AppendLine(");");
            return sb.ToString();
        }
    }
}
