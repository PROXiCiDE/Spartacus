namespace Spartacus.Database.DBModels.Milestone
{
    public class MilestoneRewardsModel
    {
        public string Id { get; set; }
        public long CivId { get; set; }
        public string TechId { get; set; }
        public long DisplayId { get; set; }
        public long RolloverTextId { get; set; }
        public long LevelRequirement { get; set; }
        public string Icon { get; set; }
    }
}