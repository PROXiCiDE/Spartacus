using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.SQLite.Annotations;
using TestUnit.Reflection.Mapper;

namespace TestUnit.Reflection
{
    public class MapperTest
    {
        public class ProtoUnit
        {
            //[PrimaryKey, AutoIncrement]
            //public int Id { get; set; }
            //public ProtoAge4XmlUnit Unit { get; set; }
            public ProtoAge4XmlUnitInitialResource InitialResource { get; set; }

            public ProtoAge4XmlUnitCost UnitCost { get; set; }

            public List<ProtoAge4XmlUnit> Units { get; set; }
            public List<string> EnumStrings { get; set; }
            public IEnumerable<int> EnumerableInt { get; set; }

            //public HashSet<string> Strings { get; set; }
            //public ICollection<string> Enumerable { get; set; }
            //public ColumnKeyAttrib? ColumnKeyAttrib { get; set; }
            ////public decimal DecimalTest { get; set; }
            //public DateTime? DateTime { get; set; }
            //public Guid? Guid { get; set; }
        }

        [Test]
        public void TestMapper()
        {
            var sb = new StringBuilder();

            var unit = new PropertyMap<ProtoUnit>().GetColumns();
            var sqlWriter = new SqlTableWriter(unit);
        }

        public class SqlTableWriter
        {
            public IEnumerable<PropertyColumnMap> Map { get; }
            public List<string> TableList { get; }

            public SqlTableWriter(IEnumerable<PropertyColumnMap> map)
            {
                var propertyColumnMaps = map as PropertyColumnMap[] ?? map.ToArray();

                Map = propertyColumnMaps;
                InitMap(propertyColumnMaps);
            }

            private void InitMap(IEnumerable<PropertyColumnMap> map)
            {
                var children = map.Where(x=> x.ColumKeyOptions.HasOption(ColumKeyOptions.Class) && x.ChildrenInfos.Any())
                    .Select(p => new { Key = p.ChildrenInfos.FirstOrDefault()?.ParentType.Name, Value = p })
                    .Distinct()
                    .ToDictionary(p => p.Key, p => p.Value);

                foreach (var child in children)
                {
                    Debug.WriteLine($"{child.Key}");
                }
                //foreach (var item in map)
                //{
                //    var tableName = item.ParentType.Name;
                //    if(item.ChildrenInfos.Any())
                //        TableList.Add( CreateTableFromItem(tableName, item.ChildrenInfos));
                //    TableList.Add( CreateTableFromItem (tableName, map) );
                //}
            }

            private string CreateTableFromItem(string tableName, IEnumerable<PropertyColumnMap> child)
            {
                var sb = new StringBuilder();


                return sb.ToString();
            }
        }
    }
}