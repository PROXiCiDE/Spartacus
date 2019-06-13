using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Milestone;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;
using TestUnit.Database;

namespace TestUnit.Bar
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

                BuildMilestoneData(conn);
            }
        }

        public void BuildMilestoneData(IDbConnection conn)
        {
            string MilestoneFilename = "milestonerewards.xml";

            var dataBar = new BarFileSystem(@"G:\Age of Empires Online\Data2\data.bar");
            var entry = dataBar.GetEntry(MilestoneFilename);
            if (entry == null) return;

            var dataTiers = new List<Milestone>();
            var xmlFile = dataBar.ReadEntry<MilestoneRewardDataXml>(entry);

            xmlFile.Tiers.Tier.ForEach(tier =>
            {
                foreach (var id in tier.RewardIds.Id)
                {
                    var reward = xmlFile.Rewards[id];
                    if (reward != null)
                        dataTiers.Add(new Milestone((long)tier.CivId, reward.Tech, tier.Level, reward.LargeIcon));
                }
            });

            Debug.WriteLine($"Count: {dataTiers.Count}");

            conn.Open();
            using (var trans = conn.BeginTransaction())
            {
                Debug.WriteLine($"Written: {conn.Insert(dataTiers)}");
                trans.Commit();
            }
            conn.Close();

            Debug.WriteLine("Before Update");

            var milestones2 = new List<Milestone>();
            conn.GetAll<Milestone>().ForEach(tier =>
            {
                var milestone = new Milestone(tier.Id, tier.CivId, tier.TechId + "_Test", tier.LevelRequirement + 10, tier.Icon);
                milestones2.Add(milestone);
                Debug.WriteLine(tier);
            });

            Debug.WriteLine("After Update");
            conn.Update(milestones2);
            conn.GetAll<Milestone>().Take(5).ForEach(tier =>
            {
                Debug.WriteLine(tier);
            });

        }
    }
}