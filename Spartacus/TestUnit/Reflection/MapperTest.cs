using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            public ProtoAge4XmlUnit Unit { get; set; }
            //public ProtoAge4XmlUnitCost UnitCost { get; set; }

            //public List<string> EnumStrings { get; set; }
            //public IEnumerable<int> EnumerableInt { get; set; }

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
            var unit = new PropertyMap<ProtoUnit>().GetColumns();
            foreach (var propertyColumnMap in unit)
            {

                Debug.WriteLine($"{propertyColumnMap}");
                foreach (var ChildrenInfos in propertyColumnMap.ChildrenInfos)
                {
                    Debug.WriteLine(ChildrenInfos);
                }
            }
        }
    }
}