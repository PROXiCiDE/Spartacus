using System;

namespace TestUnit.Reflection.Mapper
{
    [Flags]
    public enum ColumnKeyOptions : ushort
    {
        None = 0,
        Enumerable = 1 << 0,
        Enum = 1 << 1,
        Array = 1 << 2,
        Class = 1 << 3,
        Nullable = 1 << 4,
        Primitive = 1 << 5,
    }
}