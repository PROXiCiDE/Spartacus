using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper.Contrib.Extensions;
using SpartacusUtils.SQLite.Annotations;

namespace SpartacusUtils.SQLite
{
    //Separated code to partial to make the main class easier to read
    public static partial class CreateTableReflection
    {
        private static string GetColumnName(PropertyInfo propertyInfo)
        {
            //Skip non-writable properties
            if (!propertyInfo.IsWriteAttribute())
                return null;

            var sb = new StringBuilder();
            sb.Append($"\"{propertyInfo.Name}\"\t{GetColumnDeclaringType(propertyInfo)} ");

            //Order of
            //INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE
            if (propertyInfo.IsNotNullAttribute())
                sb.Append("NOT NULL ");

            //Check for our PrimaryKey Attribute or normal Key attribute which can be found in Dapper, System.ComponentModel.DataAnnotations, SQLite.Core
            if (propertyInfo.IsPrimaryKeyAttribute())
                sb.Append("PRIMARY KEY ");
            if (propertyInfo.IsAutoIncrementAttribute())
                sb.Append("AUTOINCREMENT ");
            if (propertyInfo.IsUniqueKeyAttribute())
                sb.Append("UNIQUE ");

            return sb.ToString();
        }

        private static bool IsNotNullAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.ContainsAttribute(typeof(NotNullAttribute));
        }

        private static bool IsWriteAttribute(this PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(WriteAttribute), false).ToList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute)attributes[0];
            return writeAttribute.Write;
        }

        private static bool IsUniqueKeyAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.ContainsAttribute(typeof(UniqueKeyAttribute));
        }

        private static bool IsAutoIncrementAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.ContainsAttribute(typeof(AutoIncrementAttribute));
        }

        private static bool IsPrimaryKeyAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.ContainsAttribute(typeof(PrimaryKeyAttribute)) ||
                   propertyInfo.ContainsAttribute(typeof(KeyAttribute));
        }

        private static string GetColumnDeclaringType(PropertyInfo propertyInfo)
        {
            var propType = propertyInfo.PropertyType;

            if (propType.IsEnum)
                propType = Enum.GetUnderlyingType(propType);
            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propType = Nullable.GetUnderlyingType(propType);

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

            if (typeof(byte[]) == propType)
                return "BLOB";
            if (integerTypes.Contains(propType))
                return "INTEGER";
            if (floatTypes.Contains(propType))
                return "REAL";

            return "TEXT";
        }

        private static bool ContainsAttribute(this PropertyInfo propertyInfo, Type type)
        {
            return propertyInfo.GetCustomAttributes(true).Any(attr => attr.GetType() == type);
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            //Filter out non-writable properties
            return type?.GetProperties().Where( p => p.IsWriteAttribute());
        }

        private static IEnumerable<PropertyInfo> GetIdProperties(Type type)
        {
            var tp = type.GetProperties()
                .Where(p => p.GetCustomAttributes(true)
                    .Any(attr => (attr.GetType()
                                      .Name ==
                                  typeof(PrimaryKeyAttribute).Name) ||
                                 (attr.GetType()
                                      .Name ==
                                  typeof(KeyAttribute).Name)))
                .ToList();
            return tp.Any()
                ? tp
                : type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }

        private static string GetTableNameAttribute(Type type)
        {
            var tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            var tableName = tableAttributes.Any() ? (tableAttributes[0] as TableAttribute)?.Name : type.Name;
            return tableName;
        }
    }
}