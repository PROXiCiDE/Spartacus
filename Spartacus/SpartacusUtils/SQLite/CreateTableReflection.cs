using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.SQLite
{
    public static partial class CreateTableReflection
    {
        public static void DropAllTablesIfExists(this IDbConnection conn)
        {
            var sqlCommand = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_sequence'";
            var tables = conn.Query<string>(sqlCommand);
            tables.ForEach(table =>
            {
                conn.Execute($"DROP TABLE IF EXISTS \"{table}\""); 
            });

            conn.Execute("VACUUM");
        }

        /// <summary>
        ///     Drops a table if it exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static int DropTableIfExists<T>(this IDbConnection conn)
        {
            var type = typeof(T);
            var tableName = GetTableNameAttribute(type);
            return conn.Execute($"DROP TABLE IF EXISTS '{tableName}'");
        }


        /// <summary>
        ///     Creates a SQLite table schema from a class.
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
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{tableName}' (");

            propertyInfos.ForEach(propertyInfo =>
            {
                var name = GetColumnName(propertyInfo);
                if (!string.IsNullOrEmpty(name))
                    sb.AppendLine($"{name.TrimEnd()}{(propertyInfos.LastOrDefault() == propertyInfo ? "" : ",")}");
            });

            sb.AppendLine(");");
            return sb.ToString();
        }

        /// <summary>
        ///     Create a table from a class object
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