using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Helpers;
using TestUnit.Reflection.Mapper;
using static TestUnit.Reflection.Mapper.PropertyColumnMap;

namespace TestUnit.Reflection
{
    public class MapperTest
    {
        [Test]
        public void TestMapper()
        {
            var sb = new StringBuilder();

            var unit = new PropertyMap<ProtoAge4XmlUnit>().GetColumns();
            var sqlWriter = new SqlTableWriter(unit);
        }

        public class EmptyClassTest
        {
            
        }

        public class ProtoUnitDup
        {
            public int Integer { get; set; }
            public string String { get; set; }
            public ProtoUnitDup UnitDup { get; set; }
            public ProtoUnitDup UnitDup2 { get; set; }
            public EmptyClassTest ClassTest { get; set; }
        }

        public class ProtoUnitChild
        {
            public List<string> EnumStrings { get; set; }
            public IEnumerable<int> EnumerableInt { get; set; }

            public HashSet<string> Strings { get; set; }

            public ProtoUnitDup ProtoUnitDup { get; set; }
        }

        public class ProtoUnit
        {
            //[PrimaryKey, AutoIncrement]
            //public int Id { get; set; }
            //public ProtoAge4XmlUnit Unit { get; set; }
            //public ProtoAge4XmlUnitInitialResource InitialResource { get; set; }

            public int IntegerTest { get; set; }

            //public ProtoAge4XmlUnitCost UnitCost { get; set; }
            public ProtoUnitChild Child { get; set; }
            public ProtoUnitChild ChildSecond { get; set; }

            public IEnumerable<int> EnumerableInt { get; set; }

            //public List<string> EnumStrings { get; set; }
            //public HashSet<string> Strings { get; set; }
            //public ICollection<string> Enumerable { get; set; }

            //public List<ProtoAge4XmlUnit> Units { get; set; }
            //public ColumnKeyAttrib? ColumnKeyAttrib { get; set; }
            //public decimal DecimalTest { get; set; }
            //public DateTime? DateTime { get; set; }
            //public Guid? Guid { get; set; }
        }

        public class SqlTableWriter
        {
            public Dictionary<ColumnType, string> ColumnTypeDictionary;
            public HashSet<PropertyColumnMap> UniqueMaps = new HashSet<PropertyColumnMap>();

            public SqlTableWriter(IEnumerable<PropertyColumnMap> map)
            {
                ColumnTypeDictionary = new Dictionary<ColumnType, string>
                {
                    {ColumnType.Blob, "BLOB"},
                    {ColumnType.DateTime, "TEXT"},
                    {ColumnType.DateTimeOffset, "TEXT"},
                    {ColumnType.Guid, "TEXT"},
                    {ColumnType.Integer, "INTEGER"},
                    {ColumnType.Real, "REAL"},
                    {ColumnType.Text, "TEXT"},
                    {ColumnType.TimeSpan, "TEXT"},
                    {ColumnType.Unknown, "TEXT"}
                };

                var propertyColumnMaps = map as PropertyColumnMap[] ?? map.ToArray();

                Map = propertyColumnMaps;
                InitMap(propertyColumnMaps);
            }

            public IEnumerable<PropertyColumnMap> Map { get; }
            public List<string> TableList { get; }

            private void InitMap(IEnumerable<PropertyColumnMap> map)
            {
                var maps = map as PropertyColumnMap[] ?? map.ToArray();
                var classMaps = maps.Where(x =>
                        x.ColumnKeyOptions.HasOption(ColumnKeyOptions.Class) && x.ColumnType == ColumnType.Unknown &&
                        x.ChildrenInfos.Any())
                    .Select(p => p).Distinct();

                var enumerableMaps = maps.Where(x =>
                        x.ColumnKeyOptions.HasOption(ColumnKeyOptions.Enumerable) && x.ColumnType != ColumnType.Unknown)
                    .Select(p => p).Distinct();

                GetUniqueTableMaps(classMaps);
               var tables = CreateUniqueTables(UniqueMaps);
               Debug.WriteLine(tables);
            }

            private void GetUniqueTableMaps(IEnumerable<PropertyColumnMap> classMaps)
            {
                foreach (var item in classMaps)
                    if (!UniqueMaps.Contains(item))
                        if (item.ColumnKeyOptions.HasOption(ColumnKeyOptions.Class) &&
                            item.ColumnType == ColumnType.Unknown)
                        {
                            UniqueMaps.Add(item);
                            GetUniqueTableMaps(item.ChildrenInfos);
                        }
            }

            private string CreateTableForMappedColumn(PropertyColumnMap map)
            {
                var sb = new StringBuilder();

                var childInfos = map.ChildrenInfos.OrderBy(x => ColumnTypeDictionary[x.ColumnType]).ToArray();

                var primaryKeyMaps = childInfos.FirstOrDefault(x => x.KeyAttrib.HasKey(ColumnKeyAttrib.Primary));
                var idName = childInfos.FirstOrDefault(x => x.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase));

                var primaryKeyName = primaryKeyMaps?.ColumnName;
                if (string.IsNullOrEmpty(primaryKeyName))
                    primaryKeyName = idName?.ColumnName ?? "Id";

                var parent = map.ThisType;
                var lastChild = childInfos.Last();

                sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{parent.Name}' (");
                sb.AppendLine($"'{primaryKeyName}' INTEGER{(lastChild != null ? "," : "")}");

                foreach (var column in childInfos)
                {
                    var comaSep = (lastChild != null && lastChild.Equals(column)) ? "" : ",";
                    sb.AppendLine(
                        $"'{column.ColumnName}' {ColumnTypeDictionary[column.ColumnType]}{comaSep}");
                }
                sb.AppendLine(")");

                return sb.ToString();
            }

            //TODO: Combine non Lists into Parent, create seperate tables for Lists
            private string CreateUniqueTables(IEnumerable<PropertyColumnMap> maps)
            {
                var list = new List<string>();

                foreach (var child in maps.Distinct(new PropertyColumnMapDistinctComparer()))
                    list.Add(CreateTableForMappedColumn(child));

                return string.Join(Environment.NewLine, list);
            }
        }
    }
}