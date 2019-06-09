using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.SQLite
{

    public class NotNullAttribute : Attribute
    {
    }

    public class UniqueKeyAttribute : Attribute
    {
    }

    public class AutoIncrementAttribute : Attribute
    {
    }

    public class PrimaryKeyAttribute : Attribute
    {
    }

    public class KeyAttribute : Attribute
    {
    }

    public static class CreateTableReflection
    {
        public static int DropTableIfExists<T>(this IDbConnection conn)
        {
            var type = typeof(T);
            return conn.Execute($"DROP TABLE IF EXISTS \"{type.Name}\"");
        }

        /// <summary>
        /// Creates a SQLite table schema from a class.
        /// The <see cref="Object"/> property types will be defined as an BLOB
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTableSchema(Type type)
        {
            var properties = GetAllProperties(type);
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray();
            if (!propertyInfos.Any())
                return null;

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE \"{type.Name}\" (");

            propertyInfos.ForEach(propertyInfo =>
            {
                var comma = ",";
                if (propertyInfos.LastOrDefault() == propertyInfo)
                    comma = "";
                var name = GetColumnName(propertyInfo);
                if (name != null) sb.AppendLine($"{name.TrimEnd()}{comma}");
            });

            sb.AppendLine(");");
            return sb.ToString();
        }

        public static string GetColumnName(PropertyInfo propertyInfo)
        {
            var sb = new StringBuilder();
            sb.Append($"\"{propertyInfo.Name}\"\t{GetColumnDeclaringType(propertyInfo)} ");

            //Order of
            //INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE
            if (DoesColumnContainAttribute(propertyInfo, typeof(NotNullAttribute)))
                sb.Append("NOT NULL ");
            if (DoesColumnContainAttribute(propertyInfo, typeof(PrimaryKeyAttribute)) || DoesColumnContainAttribute(propertyInfo, typeof(KeyAttribute)))
                sb.Append("PRIMARY KEY ");
            if (DoesColumnContainAttribute(propertyInfo, typeof(AutoIncrementAttribute)))
                sb.Append("AUTOINCREMENT ");
            if (DoesColumnContainAttribute(propertyInfo, typeof(UniqueKeyAttribute)))
                sb.Append("UNIQUE ");
            return sb.ToString();
        }

        public static string GetColumnDeclaringType(PropertyInfo propertyInfo)
        {
            var propType = propertyInfo.PropertyType;

            //INTEGER
            var integerTypes = new List<Type>()
            {
                typeof(Boolean),
                typeof(Char),
                typeof(Byte),
                typeof(SByte),
                typeof(Int16),
                typeof(Int32),
                typeof(Int64),
                typeof(UInt16),
                typeof(UInt32),
                typeof(UInt64)
            };

            //REAL
            var floatTypes = new List<Type>()
            {
                typeof(Decimal),
                typeof(Single),
                typeof(Double),
            };

            if (typeof(Object) == propType)
                return "BLOB";
            if (integerTypes.Contains(propType))
                return "INTEGER";
            if (floatTypes.Contains(propType))
                return "REAL";

            return "TEXT";
        }

        public static bool DoesColumnContainAttribute(PropertyInfo propertyInfo, Type type)
        {
            return propertyInfo.GetCustomAttributes(true).Any(attr => attr.GetType().Name == type.Name);
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            return type?.GetProperties();
        }

        private static IEnumerable<PropertyInfo> GetIdProperties(Type type)
        {
            var tp = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(PrimaryKeyAttribute).Name)).ToList();
            return tp.Any() ? tp : type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }
    }
}
