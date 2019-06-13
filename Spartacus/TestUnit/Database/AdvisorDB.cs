using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using Spartacus.Database.DBModels.Advisors;
using Spartacus.Logic.Builder.Advisors;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace TestUnit.Database
{
    public class AdvisorDB : SqliteTestBase
    {
        [Test]
        public void TestAdvisor()
        {
            AdvisorBuilder advisorBuilder = new AdvisorBuilder();
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                advisorBuilder.DropTables(conn);
                advisorBuilder.CreateTables(conn);

                var advisors = advisorBuilder.FromBar(new BarFileSystem(DataBar));

                advisors.Take(5).ForEach(advisor =>
                {
                    Debug.WriteLine(advisor);
                });

                advisorBuilder.InsertRepository(conn, advisors);

                advisorBuilder.FromRepository(conn).Take(5).ForEach(advisor =>
                {
                    Debug.WriteLine(advisor);
                });
            }
        }
    }
}