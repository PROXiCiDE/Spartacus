using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Milestone
{
    public interface IMilestoneRewardsRepository
    {
        List<MilestoneRewardsModel> SelectMilestoneRewards();
        void InsertMilestoneRewards(MilestoneRewardsModel milestonerewards);
        void UpdateMilestoneRewards(MilestoneRewardsModel milestonerewards);
        void DeleteMilestoneRewards(MilestoneRewardsModel milestonerewards);
    }
}