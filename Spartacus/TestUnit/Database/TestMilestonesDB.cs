using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Milestone;
using Spartacus.Database.Models.Milestone;
using Spartacus.Logic.Builder.Milestones;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace TestUnit.Database
{
    public class TestMilestonesDb : SqliteTestBase
    {
        [Test]
        public void InsertTest()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.DropTableIfExists<Milestone>();
                Debug.WriteLine(conn.CreateTable<Milestone>());

                MilestoneBuilder milestoneBuilder = new MilestoneBuilder();
                milestoneBuilder.DropTables(conn);
                milestoneBuilder.CreateTables(conn);
                var milestones = milestoneBuilder.FromBar(new BarFileSystem(DataBar));
                milestoneBuilder.InsertRepository(conn, milestones);

                var data = milestoneBuilder.FromRepository(conn);
                data.ForEach(item =>
                {
                    Debug.WriteLine(item);
                });
            }
        }
    }
}