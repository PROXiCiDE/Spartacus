using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Milestone;
using SpartacusUtils.Bar;
using SpartacusUtils.SQLite;
using Xunit;

namespace DapperTestUnit
{
    public class UnitTest1
    {
        [Fact]
        public void InsertTest()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=.\\Testdb.db;Version=3;"))
            {
                conn.DropTableIfExists<Milestone>();
                Debug.WriteLine(conn.CreateTable<Milestone>());

                BuildMilestoneData(conn);
            }
        }

        public void BuildMilestoneData(SQLiteConnection conn)
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

            conn.BulkInsert(dataTiers);
        }
    }
}
