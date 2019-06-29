using System;

namespace TestUnit.Reflection.Mapper
{
    public class PropertyColumnName
    {
        protected bool Equals(PropertyColumnName other)
        {
            return ThisType == other.ThisType && string.Equals(ColumnName, other.ColumnName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyColumnName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ThisType != null ? ThisType.GetHashCode() : 0) * 397) ^ (ColumnName != null ? ColumnName.GetHashCode() : 0);
            }
        }

        public PropertyColumnName(Type thisType, string columnName, ColumnType columnType)
        {
            ThisType = thisType;
            ColumnName = columnName;
            ColumnType = columnType;
        }

        public Type ThisType { get; }
        public string ColumnName { get; }
        public ColumnType ColumnType { get; }
    }
}