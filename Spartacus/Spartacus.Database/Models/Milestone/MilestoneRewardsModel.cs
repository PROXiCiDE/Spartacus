    namespace Spartacus.Database.Models.Milestone
{
    public class MilestoneRewardsModel
    {
        //private readonly ConfigInfo _configInfo;
        //private readonly CivilizationEnum _civId;
        //private string MilestoneFilename = "milestonerewards.xml.xmb";
        //public List<MilestoneRewardsTier> DataTiers { get; set; }
        //public MilestoneRewardsModel(ConfigInfo configInfo, CivilizationEnum civId)
        //{
        //    _configInfo = configInfo;
        //    _civId = civId;

        //    LoadModel();
        //}

        //public void LoadModel()
        //{
        //    var dataBar = _configInfo.BarFileReaders[BarFileEnum.Data];
        //    var entry = dataBar.GetEntry(MilestoneFilename);
        //    if (entry != null)
        //    {
        //        DataTiers = new List<MilestoneRewardsTier>();

        //        var xmlFile = dataBar.EntryToBytes(entry).EncodeXmlToString();
        //        MilestoneRewardDataXml milestoneXml =
        //            XmlUtils.DeserializeFromXml<MilestoneRewardDataXml>(xmlFile);

        //        foreach (var tier in milestoneXml.Tiers.Tier)
        //        {
        //            var newTier = new MilestoneRewardsTier
        //            {
        //                CivId = tier.CivId,
        //                LevelRequirement = tier.Level,
        //                RewardDatas = new List<MilestoneRewardsData>()
        //            };

        //            foreach (var rewardId in tier.RewardIds.Id)
        //            {
        //                var reward = milestoneXml.Rewards[rewardId];
        //                var rewardData = new MilestoneRewardsData
        //                {
        //                    Name = rewardId, Icon = reward.LargeIcon
        //                };

        //                var tech = _configInfo.TechTree.TechArray
        //                    .FirstOrDefault(x => string.Equals(x.Name, rewardId));

        //                if (tech != null)
        //                {
        //                    rewardData.Id = tech.DisplayNameId;
        //                    rewardData.RolloverTextId = tech.RolloverTextId;
        //                }

        //                newTier.RewardDatas.Add(rewardData);
        //            }
        //            DataTiers.Add(newTier);
        //        }
        //    }
        //}
    }
}