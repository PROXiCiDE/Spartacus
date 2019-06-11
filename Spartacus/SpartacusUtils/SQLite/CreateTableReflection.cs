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
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CreateTableSchema<T>(this IDbConnection conn)
        {
            var type = typeof(T);
            var properties = GetAllProperties(type);
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray(); //Prevent multiple enumerations
            if (!propertyInfos.Any())
                return null;

            var tableName = GetTableNameAttribute(type);

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS \"{tableName}\" (");

            propertyInfos.ForEach(propertyInfo =>
            {
                var name = GetColumnName(propertyInfo);
                if (name != null)
                    sb.AppendLine($"{name.TrimEnd()}{(propertyInfos.LastOrDefault() == propertyInfo ? "" : ",")}");
            });

            sb.AppendLine(");");
            return sb.ToString();
        }

        /// <summary>
        /// Create a table from a class object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static int CreateTable<T>(this IDbConnection conn)
        {
            var sqlQuery = conn.CreateTableSchema<T>();
            return conn.Execute(sqlQuery);
        } 
    }
}
 