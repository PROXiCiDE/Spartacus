using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Diagnostics;
using Dapper;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace DapperCrud
{
    public class TestMemoryModel
    {
        [AutoIncrement, UniqueKey, NotNull, SpartacusUtils.SQLite.Key]
        public int Id { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
    }

    internal class Program
    {

        private static void Main(string[] args)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=.\\Test.db"))
            {
                conn.DropTableIfExists<TestMemoryModel>();
                var sqlSchema = CreateTableReflection.CreateTableSchema(typeof(TestMemoryModel));

                Debug.WriteLine(sqlSchema);
                Debug.WriteLine(conn.Execute(sqlSchema));

                var test = new TestMemoryModel();
                test.Int = 100;
                test.Long = 0xff00ff;

                conn.Insert(test);

                var all = conn.GetList<TestMemoryModel>();
                all.ForEach(model =>
                {
                    Debug.WriteLine($"{model.Id}, {model.Int}, {model.Long.ToString("X")}");
                });
            }
        }

     

        //public static IEnumerable<ProtoAge> getProtoAges()
        //{
        //    var results = new List<ProtoAge>();
        //    using (var connection = new SQLiteConnection(@"Data Source=G:\_DB Dev\spartacus.db;Version=3;"))
        //    {
        //        var protoAges = connection.GetList<ProtoAge>();
        //        foreach (var protoAge in protoAges)
        //        {
        //            var newAge = new ProtoAge(protoAge);

        //            var resourceCost = connection.GetList<ProtoAgeResourceCost>(new {protoAge.Name});
        //            newAge.ResourceCost = new ProtoAgeResourceCost(resourceCost.First());

        //            var unitTypes = connection.GetList<ProtoAgeUnitType>(new {protoAge.Name});
        //            newAge.UnitTypes = new List<ProtoAgeUnitType>();
        //            unitTypes.ForEach(type => { newAge.UnitTypes.Add(new ProtoAgeUnitType(type)); });

        //            var unitFlags = connection.GetList<ProtoAgeUnitFlag>(new {protoAge.Name});

        //            newAge.UnitFlags = new List<ProtoAgeUnitFlag>();
        //            unitFlags.ForEach(flag => { newAge.UnitFlags.Add(new ProtoAgeUnitFlag(flag)); });
        //            results.Add(newAge);
        //        }
        //    }

        //    return results;
        //}

        //public static void InsertProtoAges()
        //{
        //    var protoAges = new List<ProtoAge>();
        //    using (var connection = new SQLiteConnection(@"Data Source=G:\_DB Dev\spartacus.db;Version=3;"))
        //    {
        //        connection.Execute("Delete from ProtoAge");
        //        connection.Execute("Delete from ProtoAgeResourceCost");
        //        connection.Execute("Delete from ProtoAgeUnitFlag");
        //        connection.Execute("Delete from ProtoAgeUnitType");
        //        connection.Execute("update sqlite_sequence set seq = 0 where name='ProtoAge'");

        //        var unitFlags = new List<ProtoAgeUnitFlag>();
        //        var unitTypes = new List<ProtoAgeUnitType>();
        //        var resourceCosts = new List<ProtoAgeResourceCost>();

        //        for (var i = 0; i < 5; i++)
        //        {
        //            var name = $"UnitName{i.ToString()}";
        //            var anim = $"AnimFile{i.ToString()}";

        //            unitFlags.Add(new ProtoAgeUnitFlag(name, i + 0x55));
        //            unitTypes.Add(new ProtoAgeUnitType(name, $"TechName{i.ToString()}"));
        //            resourceCosts.Add(new ProtoAgeResourceCost(name, i + 10, i + 5, 0, 0));

        //            var newAge = new ProtoAge(name, i * 200, i * 400, i + 20, anim, "IconFile",
        //                "PortraitFile", i * 0x400, i + 0x400, 0x2000, 0x3000, 10, i);

        //            connection.Insert(newAge);
        //        }

        //        foreach (var unitFlag in unitFlags)
        //        {
        //            var i = connection.Insert<long, ProtoAgeUnitFlag>(unitFlag);
        //        }

        //        foreach (var cost in resourceCosts) connection.Insert(cost);

        //        foreach (var type in unitTypes) connection.Insert(type);

        //        //for (int i = 0; i < 5; i++)
        //        //{
        //        //    var name = "UnitName" + i;
        //        //    var anim = "AnimFile" + i;
        //        //    ProtoAgeResourceCost resourceCost = new ProtoAgeResourceCost(name, i + 10, i + 5, 0, 0);
        //        //    resourceCost.Id = i;
        //        //    List<ProtoAgeUnitFlag> unitFlags = new List<ProtoAgeUnitFlag>();

        //        //    for (int j = 0; j < 3; j++)
        //        //    {
        //        //        var flag = new ProtoAgeUnitFlag(j, name, i + 0x55);
        //        //        unitFlags.Add(flag);
        //        //    }


        //        //    List<ProtoAgeUnitType> unitTypes = new List<ProtoAgeUnitType>();

        //        //    for (int j = 0; j < 3; j++)
        //        //    {
        //        //        var unitType = new ProtoAgeUnitType(j, name, anim);
        //        //        unitTypes.Add(unitType);
        //        //    }

        //        //    var newAge = new ProtoAge(name, i, i * 200, i * 400, i + 20, anim, "IconFile", "PortraitFile",
        //        //        i * 0x400, i + 0x400, 0x2000, 0x3000, 10, i - 5, resourceCost, unitFlags, unitTypes);

        //        //    protoAges.Add(newAge);
        //        //}
        //    }
        //}
    }
}