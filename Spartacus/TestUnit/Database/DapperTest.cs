using System.Data.SQLite;
using System.Reflection;
using NUnit.Framework;
using SpartacusUtils.SQLite;

namespace TestUnit.Database
{
    public class TestMemoryModel
    {
        [PrimaryKey, AutoIncrement, UniqueKey, NotNull]
        public int Key { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
    }

    internal class DapperTest
    {
        [Test]
        public void TestMemoryDb()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=:memory:"))
            {
                //conn.Drop

                var sqlSchema = conn.CreateTable<TestMemoryModel>();

                var test = new TestMemoryModel();
                test.Int = 100;
                test.Long = 0xff00ff;

                //conn.Insert(test);
            }
        }

        public PropertyInfo[] GetProperties<T>()
        {
            var obj = default(T);
            return (obj?.GetType())?.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        [Test]
        public void GetProperties()
        {
            //var currenttype = typeof(TestMemoryModel);
            //var idProps = GetIdProperties(currenttype).ToList();

            //if (!idProps.Any())
            //    throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");

            //foreach (var prop in GetAllProperties<TestMemoryModel>())
            //{
            //    //foreach (var attribute in prop.CustomAttributes)
            //    //{
            //    //    Debug.WriteLine($"{attribute.AttributeType.Name}");
            //    //}
            //    Debug.WriteLine($"{prop.DeclaringType.Name} {prop.Name} {idProps.Contains(prop)}");
            //}
        }
    }
}