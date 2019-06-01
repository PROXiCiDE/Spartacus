using System.Collections.Generic;
using ProjectCeleste.GameFiles.XMLParser.Enum;

namespace SpartacusUtils.Models.Milestone
{
    public class MilestoneRewardsTier
    {
        public CivilizationEnum CivId { get; set; }
        public int LevelRequirement { get; set; }
        public List<MilestoneRewardsData> RewardDatas { get; set; }
    }
}