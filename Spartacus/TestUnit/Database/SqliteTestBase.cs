namespace TestUnit.Database
{
    public class SqliteTestBase
    {
        public string ConnectionString { get; }
        public string DataBar = @"G:\Age of Empires Online\Data2\data.bar";

        public SqliteTestBase(string connectionString = null)
        {
            ConnectionString = connectionString ?? @"Data Source=.\Test.db;Version=3;";
        }
    }
}