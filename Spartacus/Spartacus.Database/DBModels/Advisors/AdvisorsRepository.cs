using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.Advisors
{
    public class AdvisorsRepository : IAdvisorsRepository
    {
        public AdvisorsRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        public string _connstring { get; set; }

        public List<AdvisorsModel> SelectAdvisors()
        {
            // Select
            List<AdvisorsModel> ret;
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"SELECT Name, Civid, Age, Icon, Rarirty, RollverTextId, DisplayDescriptionId, DisplayNameId, MinLevel, ItemLevel, TechId FROM [Advisors]";

                ret = db.Query<AdvisorsModel>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertAdvisors(AdvisorsModel advisors)
        {
            // Insert
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"INSERT INTO [Advisors] (Name, Civid, Age, Icon, Rarirty, RollverTextId, DisplayDescriptionId, DisplayNameId, MinLevel, ItemLevel, TechId) VALUES (@Name, @Civid, @Age, @Icon, @Rarirty, @RollverTextId, @DisplayDescriptionId, @DisplayNameId, @MinLevel, @ItemLevel, @TechId)";

                db.Execute(sql, new
                {
                    advisors.Name, advisors.Civid, advisors.Age, advisors.Icon,
                    advisors.Rarirty,
                    advisors.RollverTextId,
                    advisors.DisplayDescriptionId,
                    advisors.DisplayNameId,
                    advisors.MinLevel,
                    advisors.ItemLevel,
                    advisors.TechId
                }, commandType: CommandType.Text);
            }
        }

        public void UpdateAdvisors(AdvisorsModel advisors)
        {
            // Update
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"UPDATE [Advisors] SET Name = @Name, Civid = @Civid, Age = @Age, Icon = @Icon, Rarirty = @Rarirty, RollverTextId = @RollverTextId, DisplayDescriptionId = @DisplayDescriptionId, DisplayNameId = @DisplayNameId, MinLevel = @MinLevel, ItemLevel = @ItemLevel, TechId = @TechId WHERE Name = @Name";

                db.Execute(sql, new
                {
                    advisors.Name, advisors.Civid, advisors.Age, advisors.Icon,
                    advisors.Rarirty,
                    advisors.RollverTextId,
                    advisors.DisplayDescriptionId,
                    advisors.DisplayNameId,
                    advisors.MinLevel,
                    advisors.ItemLevel,
                    advisors.TechId
                }, commandType: CommandType.Text);
            }
        }

        public void DeleteAdvisors(AdvisorsModel advisors)
        {
            // Delete
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"DELETE FROM [Advisors] WHERE Name = @Name";

                db.Execute(sql, new {advisors.Name}, commandType: CommandType.Text);
            }
        }
    }
}