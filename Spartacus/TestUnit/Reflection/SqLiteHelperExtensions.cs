using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Dapper.Contrib.Extensions;
using SpartacusUtils.SQLite.Annotations;

namespace TestUnit.Reflection
{
    public static class GenericClassifier
    {
        public static bool IsICollection(Type type)
        {
            return Array.Exists(type.GetInterfaces(), IsGenericCollectionType);
        }
        public static bool IsIEnumerable(Type type)
        {
            return Array.Exists(type.GetInterfaces(), IsGenericEnumerableType);
        }
        public static bool IsISet(Type type)
        {
            return Array.Exists(type.GetInterfaces(), IsGenericSetType);
        }
        static bool IsGenericCollectionType(Type type)
        {
            return type.IsGenericType && (typeof(ICollection<>) == type.GetGenericTypeDefinition());
        }
        static bool IsGenericEnumerableType(Type type)
        {
            return type.IsGenericType && (typeof(IEnumerable<>) == type.GetGenericTypeDefinition());
        }
        static bool IsGenericSetType(Type type)
        {
            return type.IsGenericType && (typeof(ISet<>) == type.GetGenericTypeDefinition());
        }
    }

    public static partial class SqLiteHelperExtensions
    {
        public static Dictionary<string, string> SqlLinkedSchemas = new Dictionary<string, string>();
        public static Dictionary<Type, string> ColumnTypeRename = new Dictionary<Type, string>();
        public static Dictionary<string, string> ColumnReplace = new Dictionary<string, string>();
        public static Dictionary<string, Type> keyConstraint = new Dictionary<string, Type>();

        public static void AddColumnRename(Type type, string newName)
        {
            ColumnTypeRename.Add(type, newName);
        }

        public static void AddColumnReplace(string source, string dest)
        {
            ColumnReplace.Add(source, dest);
        }

        public static string CreateTableSchemaDevChild(this IDbConnection conn, string idTableLink,
            PropertyInfo info, Type type, bool parentNode = false,
            string appendChildName = "")
        {
            var properties = GetAllProperties(type);
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray(); //Prevent multiple enumerations

            var tableName = info.Name;

            var sb = new StringBuilder();

            //Debug.WriteLine($"{idTableLink}, {parentNode}, {appendChildName}");

            if (!string.IsNullOrEmpty(appendChildName))
                tableName = $"{appendChildName}{tableName}";
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{tableName}' (");

            if (!parentNode)
            {
                sb.AppendLine("'Id' INTEGER PRIMARY KEY AUTOINCREMENT,");
                sb.AppendLine($"'{idTableLink}' INTEGER,");
            }

            if (!propertyInfos.Any() || type == typeof(string))
            {
                var name = GetColumnDeclaringType(type);
                sb.AppendLine($"'Value' {name}{(type.IsEnum ? "," : "")}");
            }

            if (type.IsEnum)
                sb.AppendLine("'EnumValue_Remove_AFTERCODE_CLEANUP' TEXT ");

            if (type != typeof(string))
                foreach (var propertyInfo in propertyInfos)
                {
                    var propType = propertyInfo.PropertyType;
                    var name = GetColumnName(propertyInfo);

                    if (propType.IsArray)
                        continue;

                    var propDecl = GetColumnDeclaringType(propType);
                    if (!propType.IsGenericType && propType.IsClass && string.IsNullOrEmpty(propDecl))
                    {
                        GetPropertyStrings(type, sb);
                        continue;
                    }

                    if (propType.IsGenericType && propType.GetGenericTypeDefinition()
                        == typeof(List<>))
                    {
                        var itemType = propType.GenericTypeArguments[0];
                        var key = $"{itemType.Name} {propertyInfo.Name}";

                        if (!SqlLinkedSchemas.ContainsKey(key))
                            SqlLinkedSchemas.Add(key,
                                CreateTableSchemaDevChild(conn, idTableLink, propertyInfo, itemType,
                                    appendChildName: tableName));
                        continue;
                    }

                    if (!string.IsNullOrEmpty(name))
                        sb.AppendLine($"{name.TrimEnd()}{(propertyInfos.LastOrDefault() == propertyInfo ? "" : ",")}");
                }

            sb.AppendLine(");");
            sb.AppendLine("");

            return sb.ToString();
        }

        public static string CreateTableSchemaDev<T>(this IDbConnection conn, string idTableLink,
            bool parentNode = true, string appendChildName = "", string tableNameOverride = "")
        {

            var type = typeof(T);
            var properties = GetAllProperties(type);


            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray(); //Prevent multiple enumerations
            if (!propertyInfos.Any())
                return null;

            var tableName = tableNameOverride;
            if (string.IsNullOrEmpty(tableName))
                tableName = GetTableNameAttribute(type);

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(appendChildName))
                tableName = $"{tableName}_{appendChildName}";

            sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{tableName}' (");

            if (!parentNode)
            {
                sb.AppendLine("'Id' INTEGER PRIMARY KEY AUTOINCREMENT,");
                sb.AppendLine($"'{idTableLink}' INTEGER,");
            }

            foreach (var propertyInfo in propertyInfos)
            {
                var name = GetColumnName(propertyInfo, propertyInfo.PropertyType);

                var propType = propertyInfo.PropertyType;

                if (propType.IsArray)
                    continue;

                var propDecl = GetColumnDeclaringType(propType);
                if (!propType.IsGenericType && propType.IsClass && string.IsNullOrEmpty(propDecl))
                {
                    GetPropertyStrings(type, sb);
                    continue;
                }

                if (GenericClassifier.IsIEnumerable(propType))
                {
                    var itemType = propType;
                    if (propType.GenericTypeArguments.Any())
                        itemType = propType.GenericTypeArguments[0];
                    var key = $"{itemType.Name}";

                    if (!SqlLinkedSchemas.ContainsKey(key))
                        SqlLinkedSchemas.Add(key,
                            CreateTableSchemaDevChild(conn, idTableLink, propertyInfo, itemType, false, tableName));
                    continue;
                }


                if (!string.IsNullOrEmpty(name))
                    sb.AppendLine($"{name.TrimEnd()}{(propertyInfos.LastOrDefault() == propertyInfo ? "" : ",")}");
            }

            sb.AppendLine(");");
            sb.AppendLine("");

            return sb + string.Join(Environment.NewLine, SqlLinkedSchemas.Values);
        }

        public static void GetPropertyStrings(Type type, StringBuilder stringBuilder,
            PropertyInfo parentPropertyInfo = null)
        {
            var propertyInfos = GetAllProperties(type);
            var enumerable = propertyInfos as PropertyInfo[] ?? propertyInfos.ToArray();

            foreach (var propertyInfo in enumerable)
            {
                var name = GetColumnName(propertyInfo);
                if (parentPropertyInfo != null)
                    name = GetColumnName(propertyInfo, type);
                var propType = propertyInfo.PropertyType;
                if (propType.IsArray)
                    continue;

                if (!propType.IsGenericType && propType.IsClass &&
                    string.IsNullOrEmpty(GetColumnDeclaringType(propType)))
                {
                    GetPropertyStrings(propType, stringBuilder, propertyInfo);
                    continue;
                }

                if (!string.IsNullOrEmpty(name))
                    stringBuilder.AppendLine(
                        $"{name.TrimEnd()}{(enumerable.LastOrDefault() == propertyInfo ? "" : ",")}");
            }
        }

        public static string GetColumnName(PropertyInfo propertyInfo, Type parentType = null)
        {
            //Skip non-writable properties
            if (!propertyInfo.IsWriteAttribute())
                return null;

            var propertyInfoName = GetPropertyNameFromParent(parentType, propertyInfo.Name);
            var sb = new StringBuilder();

            var delcaringType = GetColumnDeclaringType(propertyInfo.PropertyType);

            var keyConstraintUnique = $"{propertyInfoName}_{delcaringType}{(parentType != null ? parentType.Name : "")}";
            if (!keyConstraint.ContainsKey(keyConstraintUnique))
                keyConstraint.Add(keyConstraintUnique, parentType);
            else
                return null;

            sb.Append($"'{propertyInfoName}'\t{GetColumnDeclaringType(propertyInfo.PropertyType)} ");

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

        private static string GetPropertyNameFromParent(Type parentType, string propertyInfoName)
        {
            if (parentType != null)
            {
                var idTableLink = parentType.Name;
                if (string.Equals(propertyInfoName, idTableLink, StringComparison.OrdinalIgnoreCase))
                    propertyInfoName = $"{idTableLink}Id";

                if (ColumnReplace.Any())
                {
                    var replaceDefault = ColumnReplace.FirstOrDefault(k => Regex.IsMatch(idTableLink, $"({k.Key})"));
                    if (replaceDefault.Key != null)
                    {
                        propertyInfoName = idTableLink.Replace(replaceDefault.Key, replaceDefault.Value) +
                                           propertyInfoName;
                    }
                }
            }

            return propertyInfoName;
        }

        public static string GetColumnDeclaringType(Type propType)
        {
            Type GetUnderlyingPropType(Type type)
            {
                if (GenericClassifier.IsISet(type))
                    type = GetUnderlyingPropType(type.GenericTypeArguments[0]);
                if (type.IsEnum)
                    return Enum.GetUnderlyingType(type);
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return Nullable.GetUnderlyingType(type);

                return type;
            }

            propType = GetUnderlyingPropType(propType);

            //INTEGER
            var integerTypes = new List<Type>
            {
                typeof(bool),
                typeof(char),
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(ushort),
                typeof(uint),
                typeof(ulong)
            };

            //REAL
            var floatTypes = new List<Type>
            {
                typeof(decimal),
                typeof(float),
                typeof(double)
            };

            if (typeof(byte[]) == propType)
                return "BLOB";
            if (integerTypes.Contains(propType))
                return "INTEGER";
            if (floatTypes.Contains(propType))
                return "REAL";
            if (propType == typeof(string))
                return "TEXT";

            return null;
        }
    }


}