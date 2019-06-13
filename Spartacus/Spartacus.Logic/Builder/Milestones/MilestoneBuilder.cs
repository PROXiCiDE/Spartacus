using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Dapper.Contrib.Extensions;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Milestone;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace Spartacus.Logic.Builder.Milestones
{

    public class MilestoneBuilder : IRepositoryBuilder<Milestone>
    {
        public IEnumerable<Milestone> FromBar(BarFileSystem barFile)
        {
            var entry = barFile.GetEntry(StringResource.XmlFile_MilestoneRewards);
            if (entry == null) return null;

            var xmlFile = barFile.ReadEntry<MilestoneRewardDataXml>(entry);

            var milestones = new List<Milestone>();
            xmlFile.Tiers.Tier.ForEach(tier =>
            {
                foreach (var id in tier.RewardIds.Id)
                {
                    var reward = xmlFile.Rewards[id];
                    if (reward != null)
                        milestones.Add(new Milestone((long)tier.CivId, reward.Tech, tier.Level, reward.LargeIcon));
                }
            });
            return milestones;
        }

        public IEnumerable<Milestone> FromRepository(IDbConnection connection)
        {
            return connection.GetAll<Milestone>();
        }

        public long InsertRepository(IDbConnection connection, IEnumerable<Milestone> milestones)
        {
            connection.Open();
            long writtenCount = 0;
            using (var trans = connection.BeginTransaction())
            {
                writtenCount = connection.Insert(milestones);
                trans.Commit();
            }
            connection.Close();
            return writtenCount;
        }

        public void DropTables(IDbConnection connection)
        {
            connection.DropTableIfExists<Milestone>();
        }

        public void CreateTables(IDbConnection connection)
        {
            connection.CreateTable<Milestone>();
        }
    }
}