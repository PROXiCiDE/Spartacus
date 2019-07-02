using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using SpartacusUtils.SQLite.Annotations;

namespace TestUnit.Reflection.Mapper
{
    public class PropertyMap<T>
    {
        public PropertyMap()
        {
            KeyAttributes = new Dictionary<Type, ColumnKeyAttrib>
            {
                {typeof(KeyAttribute), ColumnKeyAttrib.Primary},
                {typeof(PrimaryKeyAttribute), ColumnKeyAttrib.Primary},
                {typeof(AutoIncrementAttribute), ColumnKeyAttrib.AutoIncrecment},
                {typeof(ExplicitKeyAttribute), ColumnKeyAttrib.Explicit},
                {typeof(UniqueKeyAttribute), ColumnKeyAttrib.Unique},
                {typeof(NotNullAttribute), ColumnKeyAttrib.NotNull},
                {typeof(TableAttribute), ColumnKeyAttrib.Table},
                {typeof(WriteAttribute), ColumnKeyAttrib.Write }
            };
        }

        public Dictionary<Type, ColumnKeyAttrib> KeyAttributes { get; set; }

        public IEnumerable<PropertyColumnMap> GetColumns()
        {
            var parentType = typeof(T);
            var properties = parentType.GetProperties();
            return GetColumns(parentType, properties);
        }

        public IEnumerable<PropertyColumnMap> GetColumns(Type parentType, PropertyInfo[] propertyInfos)
        {

            foreach (var propertyInfo in propertyInfos)
            {
                var propType = propertyInfo.PropertyType;
                var attribute = GetColumnKeyAttribute(propertyInfo);
                var columnType = GetColumnType(propType);
                var keyOptions = GetColumnKeyOptions(propertyInfo);

                if ((propType.IsGenericType && keyOptions.HasOption(ColumnKeyOptions.Enumerable)))
                    propType = propType.GenericTypeArguments.FirstOrDefault();

                if (propType.IsArray)
                    propType = propType.GetElementType();

                yield return
                    new PropertyColumnMap(
                        propertyInfo.Name,
                        parentType,
                        propType,
                        columnType,
                        attribute,
                        keyOptions,
                        GetColumns(propType, propType?.GetProperties()));
            }
        }

        public ColumnKeyOptions GetColumnKeyOptions(PropertyInfo propertyInfo)
        {
            ColumnKeyOptions keyOptions = ColumnKeyOptions.None;

            var typeHashSet = new List<Type>()
            {
                typeof(IEnumerable<>),
                typeof(ICollection<>),
                typeof(ISet<>),
            };

            Type propType = propertyInfo.PropertyType;
            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propType = Nullable.GetUnderlyingType(propType);
                keyOptions |= ColumnKeyOptions.Nullable;
            }

            if (propType.IsInterface)
            {
                if (propType.GetInterface(nameof(IEnumerable)) != null)
                    keyOptions |= ColumnKeyOptions.Enumerable;
                else
                    foreach (var hashType in typeHashSet)
                        if (IsGenericTypeOf(propType, hashType))
                        {
                            keyOptions |= ColumnKeyOptions.Enumerable;
                            break;
                        }
            }

            if (propType.IsClass)
                keyOptions |= ColumnKeyOptions.Class;

            if (propType.IsEnum)
                keyOptions |= ColumnKeyOptions.Enum;

            if (propType.IsPrimitive)
                keyOptions |= ColumnKeyOptions.Primitive;

            return keyOptions;
        }

        public ColumnKeyAttrib GetColumnKeyAttribute(PropertyInfo propertyInfo)
        {
            ColumnKeyAttrib keyAttrib = 0;
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();
            foreach (var (key, value) in KeyAttributes)
                if (attributes.Any(attr => key != null && attr.GetType() == key))
                    keyAttrib |= value;


            return keyAttrib;
        }

        public bool IsGenericTypeOf(Type sourceType, Type destinationType)
        {
            if (!sourceType.IsGenericType)
                return false;

            return sourceType.GetInterfaces()
                       .Where(t => t.IsGenericType)
                       .Any(delegate (Type p)
                       {
                           return p.GetGenericTypeDefinition() == destinationType;
                       });
        }

        public ColumnType GetColumnType(Type propType)
        {

            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propType = Nullable.GetUnderlyingType(propType);
            if (propType.IsGenericType)
                propType = propType.GetGenericArguments().FirstOrDefault();
            if (propType != null && propType.IsEnum)
                propType = Enum.GetUnderlyingType(propType);


            //INTEGER
            var integerTypes = new List<Type>
            {
                typeof(bool),
                typeof(char),
                typeof(byte), typeof(sbyte),
                typeof(short), typeof(ushort),
                typeof(int), typeof(uint),
                typeof(long), typeof(ulong)
            };

            //REAL
            var floatTypes = new List<Type>
            {
                typeof(decimal),
                typeof(float),
                typeof(double)
            };

            if (typeof(byte[]) == propType)
                return ColumnType.Blob;
            else if (integerTypes.Contains(propType))
                return ColumnType.Integer;
            else if (floatTypes.Contains(propType))
                return ColumnType.Real;
            else if (propType == typeof(string))
                return ColumnType.Text;
            else if (propType == typeof(Guid))
                return ColumnType.Guid;
            else if (propType == typeof(DateTime))
                return ColumnType.DateTime;
            else if (propType == typeof(DateTimeOffset))
                return ColumnType.DateTimeOffset;
            else if (propType == typeof(TimeSpan))
                return ColumnType.TimeSpan;

            return ColumnType.Unknown;
        }
    }
}