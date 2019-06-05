using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using Spartacus.Database.DBModels.Milestone;
using SpartacusUtils.Bar;
using MilestoneRewardsModel = Spartacus.Database.Models.Milestone.MilestoneRewardsModel;

namespace Spartacus.Logic.Builder.Milestones
{
    public class MilestoneModelBuilder : IModelBuilder<MilestoneRewardsModel, MilestoneRewardsRepository>
    {
        public List<MilestoneRewardsModel> FromBar(BarFileSystem barFileReader)
        {
            var models = new List<MilestoneRewardsModel>();

            var entry = barFileReader.GetEntry(StringResource.XmlFile_MilestoneRewards);
            if (entry != null)
            {
                var xmlClass = barFileReader.ReadEntry<MilestoneRewardDataXml>(entry);

                xmlClass?.Tiers?.Tier.ForEach(tier =>
                {
                    var model = new MilestoneRewardsModel();

                    //foreach (var rewardId in tier.RewardIds.Id)
                    //{
                    //    var reward = xmlClass.Rewards[rewardId];

                    //    var tech = _configInfo.TechTree.TechArray
                    //        .FirstOrDefault(x => string.Equals(x.Name, rewardId));

                    //    if (tech != null)
                    //    {
                    //        rewardData.Id = tech.DisplayNameId;
                    //        rewardData.RolloverTextId = tech.RolloverTextId;
                    //    }
                    //}

                    models.Add(model);
                });
            }
            return models;
        }

        public List<MilestoneRewardsModel> FromRepository(MilestoneRewardsRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}
