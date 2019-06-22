﻿using System;

namespace TestUnit.Reflection.Mapper
{
    [Flags]
    public enum ColumKeyOptions : ushort
    {
        None = 0,
        Enumerable = 1 << 0,
        Enum = 1 << 1,
        Array = 1 << 2,
        Class = 1 << 3,
        Nullable = 1 << 4,
    }

    [Flags]
    public enum ColumnKeyAttrib : ushort
    {
        None = 0,
        Primary = 1 << 0,
        AutoIncrecment = 1 << 1,
        Explicit = 1 << 2,
        NotNull = 1 << 3,
        Unique = 1 << 4,
        Table = 1 << 5,
        Foreign = 1 << 6,
        Write = 1 << 7,
    }
}