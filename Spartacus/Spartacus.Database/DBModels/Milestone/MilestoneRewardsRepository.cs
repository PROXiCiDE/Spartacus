using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.Milestone
{
    public class MilestoneRewardsRepository : IMilestoneRewardsRepository
    {
        public MilestoneRewardsRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        private string _connstring { get; }

        public List<MilestoneRewardsModel> SelectMilestoneRewards()
        {
            // Select
            List<MilestoneRewardsModel> ret;
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"SELECT Id, CivId, TechId, DisplayId, RolloverTextId, LevelRequirement, Icon FROM [MilestoneRewards]";

                ret = db.Query<MilestoneRewardsModel>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertMilestoneRewards(MilestoneRewardsModel milestonerewards)
        {
            // Insert
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"INSERT INTO [MilestoneRewards] (Id, CivId, TechId, DisplayId, RolloverTextId, LevelRequirement, Icon) VALUES (@Id, @CivId, @TechId, @DisplayId, @RolloverTextId, @LevelRequirement, @Icon)";

                db.Execute(sql, new
                {
                    milestonerewards.Id, milestonerewards.CivId, milestonerewards.TechId, milestonerewards.DisplayId,
                    milestonerewards.RolloverTextId,
                    milestonerewards.LevelRequirement,
                    milestonerewards.Icon
                }, commandType: CommandType.Text);
            }
        }

        public void UpdateMilestoneRewards(MilestoneRewardsModel milestonerewards)
        {
            // Update
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"UPDATE [MilestoneRewards] SET Id = @Id, CivId = @CivId, TechId = @TechId, DisplayId = @DisplayId, RolloverTextId = @RolloverTextId, LevelRequirement = @LevelRequirement, Icon = @Icon WHERE Id = @Id";

                db.Execute(sql, new
                {
                    milestonerewards.Id, milestonerewards.CivId, milestonerewards.TechId, milestonerewards.DisplayId,
                    milestonerewards.RolloverTextId,
                    milestonerewards.LevelRequirement,
                    milestonerewards.Icon
                }, commandType: CommandType.Text);
            }
        }

        public void DeleteMilestoneRewards(MilestoneRewardsModel milestonerewards)
        {
            // Delete
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"DELETE FROM [MilestoneRewards] WHERE Id = @Id";

                db.Execute(sql, new {milestonerewards.Id}, commandType: CommandType.Text);
            }
        }
    }
}