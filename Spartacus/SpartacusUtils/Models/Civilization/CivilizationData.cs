using ProjectCeleste.GameFiles.XMLParser.Enum;
using SpartacusUtils.Models.Milestone;

namespace SpartacusUtils.Models.Civilization
{
    public class CivilizationData
    {
        public CivilizationData(
            string name,
            ECivilizationEnum civMatchingId,
            int displayNameId,
            CivilizationShieldData shieldData,
            StartingResourcesData startingResourcesData, MilestoneRewardsModel milestoneRewardsModel)
        {
            Name = name;
            CivMatchingId = civMatchingId;
            DisplayNameId = displayNameId;
            ShieldData = shieldData;
            StartingResourcesData = startingResourcesData;
            MilestoneRewardsModel = milestoneRewardsModel;
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name { get; private set; }
        public ECivilizationEnum CivMatchingId { get; private set; }
        public int DisplayNameId { get; private set; }
        public CivilizationShieldData ShieldData { get; private set; }
        public StartingResourcesData StartingResourcesData { get; private set; }
        public MilestoneRewardsModel MilestoneRewardsModel { get; }
    }
}
