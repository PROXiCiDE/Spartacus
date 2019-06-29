using System.Collections.Generic;

namespace TestUnit.Reflection.Mapper
{
    public sealed class PropertyColumnMapDistinctComparer : IEqualityComparer<PropertyColumnMap>
    {
        public bool Equals(PropertyColumnMap x, PropertyColumnMap y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.ThisType == y.ThisType && x.ColumnKeyOptions == y.ColumnKeyOptions && x.ColumnType == y.ColumnType && x.KeyAttrib == y.KeyAttrib;
        }

        public int GetHashCode(PropertyColumnMap obj)
        {
            unchecked
            {
                var hashCode = (obj.ThisType != null ? obj.ThisType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)obj.ColumnKeyOptions;
                hashCode = (hashCode * 397) ^ (int)obj.ColumnType;
                hashCode = (hashCode * 397) ^ (int)obj.KeyAttrib;
                return hashCode;
            }
        }
    }
}