using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using Spartacus.Database.DBModels.Milestone;
using SpartacusUtils.Bar;
using SpartacusUtils.ConfigManager;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;
using SpartacusUtils.Xml.Helpers;

namespace TestUnit
{
    public class TestMilestonesDb
    {
        [Test]
        public void InsertTest()
        {
            using (IDbConnection conn = new SQLiteConnection("Data Source=.\\Testdb.db;Version=3;"))
            {
                conn.DropTableIfExists<Milestone>();
                Debug.WriteLine(conn.CreateTable<Milestone>());
            }
        }

        [Test]
        public void BuildMilestoneData()
        {
            string MilestoneFilename = "milestonerewards.xml.xmb";

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
                    dataTiers.Add(new Milestone((long) tier.CivId, reward.Tech, tier.Level, reward.LargeIcon));
                }
            });

            dataTiers.Take(5).ForEach(tier =>
            {
                Debug.WriteLine(tier);
            });
        }
    }
}