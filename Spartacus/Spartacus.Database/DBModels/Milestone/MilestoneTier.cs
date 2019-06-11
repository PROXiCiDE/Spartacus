using SpartacusUtils.SQLite;

namespace Spartacus.Database.DBModels.Milestone
{
    public class MilestoneTier
    {
        public MilestoneTier()
        {
        }

        public MilestoneTier(long civId, string techName, int levelRequirement)
        {
            CivId = civId;
            TechName = techName;
            LevelRequirement = levelRequirement;
        }

        [Key, AutoIncrement]
        public long Id { get; set; }
        public long CivId { get; set; }
        public string TechName { get; set; }
        public int LevelRequirement { get; set; }
    }
}