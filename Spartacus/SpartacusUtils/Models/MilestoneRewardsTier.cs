using System.Collections.Generic;
using ProjectCeleste.GameFiles.XMLParser.Enum;

namespace SpartacusUtils.Models
{
    public class MilestoneRewardsTier
    {
        public CivilizationEnum CivId;
        public int LevelRequirement;
        public List<MilestoneRewardsData> RewardDatas;
    }
}