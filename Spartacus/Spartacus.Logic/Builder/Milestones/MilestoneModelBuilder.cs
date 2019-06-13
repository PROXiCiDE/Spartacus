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

namespace Spartacus.Logic.Builder.Milestones
{

    public class MilestoneBuilder : IRepositoryBuilder<Milestone>
    {
        public IEnumerable<Milestone> FromRepository(IDbConnection connection)
        {
            return connection.GetAll<Milestone>();
        }

        public IEnumerable<Milestone> FromXml(BarFileSystem barFile, IDbConnection connection)
        {
            var entry = barFile.GetEntry(StringResource.XmlFile_MilestoneRewards);
            if (entry == null) return null;

            var xmlFile = barFile.ReadEntry<MilestoneRewardDataXml>(entry);

            var toInsert = new List<Milestone>();
            xmlFile.Tiers.Tier.ForEach(tier =>
            {
                foreach (var id in tier.RewardIds.Id)
                {
                    var reward = xmlFile.Rewards[id];
                    if (reward != null)
                        toInsert.Add(new Milestone((long)tier.CivId, reward.Tech, tier.Level, reward.LargeIcon));
                }
            });

            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                connection.Insert(toInsert);
                trans.Commit();
            }
            connection.Close();

            return toInsert;
        }
    }
}