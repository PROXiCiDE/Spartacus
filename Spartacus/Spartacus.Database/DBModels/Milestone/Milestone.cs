using System;
using Dapper.Contrib.Extensions;
using SpartacusUtils.SQLite;
using SpartacusUtils.SQLite.Annotations;

namespace Spartacus.Database.DBModels.Milestone
{
    [Table("Milestone")]
    public class Milestone
    {
        /// <summary>
        /// Used for specific SQLite Query / Commands such as Update/Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="civId"></param>
        /// <param name="techId"></param>
        /// <param name="levelRequirement"></param>
        /// <param name="icon"></param>
        public Milestone(long id, long civId, string techId, long levelRequirement, string icon)
        {
            Id = id;
            CivId = civId;
            TechId = techId;
            LevelRequirement = levelRequirement;
            Icon = icon;
        }

        public Milestone()
        {
        }

        /// <summary>
        /// Create a new Milestone
        /// </summary>
        /// <param name="civId"></param>
        /// <param name="techId"></param>
        /// <param name="levelRequirement"></param>
        /// <param name="icon"></param>
        public Milestone(long civId, string techId, long levelRequirement, string icon)
        {
            CivId = civId;
            TechId = techId;
            LevelRequirement = levelRequirement;
            Icon = icon;
        }

        [Key]
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