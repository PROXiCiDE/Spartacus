using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;
using SpartacusUtils.SQLite.Annotations;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace TestUnit.Reflection
{
    public static partial class SqLiteHelperExtensions
    {
        private static bool IsNotNullAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.ContainsAttribute(typeof(NotNullAttribute));
        }

        private static bool IsWriteAttribute(this PropertyInfo propertyInfo)
        {
            if (propertyInfo != null)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(WriteAttribute), false);
                if (attributes == null)
                    return false;

                if (attributes.Length != 1) return true;

                var writeAttribute = (WriteAttribute)attributes[0];
                return writeAttribute.Write;
            }

            return true;
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

        private static bool ContainsAttribute(this PropertyInfo propertyInfo, Type type)
        {
            return propertyInfo != null && propertyInfo.GetCustomAttributes(true)
                       .Any(attr => type != null && attr.GetType() == type);
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            //Filter out non-writable properties
            return type?.GetProperties().Where(p => p.IsWriteAttribute());
        }

        private static IEnumerable<PropertyInfo> GetIdProperties(Type type)
        {
            var tp = type.GetProperties()
                .Where(p => p.GetCustomAttributes(true)
                    .Any(attr => attr.GetType()
                                     .Name ==
                                 typeof(PrimaryKeyAttribute).Name ||
                                 attr.GetType()
                                     .Name ==
                                 typeof(KeyAttribute).Name))
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
