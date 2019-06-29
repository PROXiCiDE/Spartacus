using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestUnit.Reflection.Mapper
{
    public class PropertyColumnMap
    {
        protected bool Equals(PropertyColumnMap other)
        {
            return ThisType == other.ThisType && string.Equals(ColumnName, other.ColumnName) && ColumnKeyOptions == other.ColumnKeyOptions && ColumnType == other.ColumnType && KeyAttrib == other.KeyAttrib;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyColumnMap) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ThisType != null ? ThisType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ColumnName != null ? ColumnName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ColumnKeyOptions;
                hashCode = (hashCode * 397) ^ (int) ColumnType;
                hashCode = (hashCode * 397) ^ (int) KeyAttrib;
                return hashCode;
            }
        }

        public PropertyColumnMap(Type thisType)
        {
            ThisType = thisType;
        }

        public PropertyColumnMap(string columnName,
            Type parentType, Type thisType, ColumnType columnType, ColumnKeyAttrib keyAttrib,
            ColumnKeyOptions columnKeyOptions, IEnumerable<PropertyColumnMap> childrenInfos)
        {
            ParentType = parentType;
            ChildrenInfos = childrenInfos;
            ThisType = thisType;
            ColumnKeyOptions = columnKeyOptions;
            ColumnName = columnName;
            ColumnType = columnType;
            KeyAttrib = keyAttrib;
        }

        public Type ParentType { get; }
        public Type ThisType { get; }
        public string ColumnName { get; }
        public ColumnKeyOptions ColumnKeyOptions { get; }
        public ColumnType ColumnType { get; }
        public ColumnKeyAttrib KeyAttrib { get; }
        public IEnumerable<PropertyColumnMap> ChildrenInfos { get; }

        public override string ToString()
        {
            return $"{nameof(ColumnName)}: {ColumnName}, {nameof(ColumnType)}: {ColumnType}, {nameof(ColumnKeyOptions)}: {ColumnKeyOptions}, {nameof(KeyAttrib)}: {KeyAttrib}";
        }
    }
}