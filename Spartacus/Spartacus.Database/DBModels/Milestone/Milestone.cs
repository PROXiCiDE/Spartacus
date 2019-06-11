using System;

namespace Spartacus.Database.DBModels.Milestone
{
    public class Milestone
    {
        public Milestone()
        {
        }

        public Milestone(long civId, string techId, long levelRequirement, string icon)
        {
            CivId = civId;
            TechId = techId;
            LevelRequirement = levelRequirement;
            Icon = icon;
        }

        public long Id { get; set; }
        public long CivId { get; set; }
        public string TechId { get; set; }
        public long LevelRequirement { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(CivId)}: {CivId}, {nameof(TechId)}: {TechId}, {nameof(LevelRequirement)}: {LevelRequirement}, {nameof(Icon)}: {Icon}";
        }
    }
}