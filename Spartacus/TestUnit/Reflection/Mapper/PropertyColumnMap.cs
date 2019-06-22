using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestUnit.Reflection.Mapper
{
    public class PropertyColumnMap
    {
        public PropertyColumnMap()
        {
        }

        public PropertyColumnMap(string columnName,
            Type parentType, ColumnType columnType, ColumnKeyAttrib keyAttrib,
            ColumKeyOptions columKeyOptions, IEnumerable<PropertyColumnMap> childrenInfos)
        {
            ParentType = parentType;
            ChildrenInfos = childrenInfos;
            ColumKeyOptions = columKeyOptions;
            ColumnName = columnName;
            ColumnType = columnType;
            KeyAttrib = keyAttrib;
        }

        public Type ParentType { get; }
        public string ColumnName { get; }
        public ColumKeyOptions ColumKeyOptions { get; }
        public ColumnType ColumnType { get; }
        public ColumnKeyAttrib KeyAttrib { get; }
        public IEnumerable<PropertyColumnMap> ChildrenInfos { get; }

        public override string ToString()
        {
            return $"{nameof(ColumnName)}: {ColumnName}, {nameof(ColumnType)}: {ColumnType}, {nameof(KeyAttrib)}: {KeyAttrib}";
        }
    }
}