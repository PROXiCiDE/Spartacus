using System.Collections.Generic;
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

            var unit = new PropertyMap<ProtoUnit>().GetColumns();
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
    }
}