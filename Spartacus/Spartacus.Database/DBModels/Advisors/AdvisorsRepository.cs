using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Spartacus.Database.DBModels.Advisors
{
    public class AdvisorsRepository : IAdvisorsRepository
    {
        public string _connstring { get; set; }

        public AdvisorsRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        public List<Advisors> SelectAdvisors()
        {
            // Select
            List<Advisors> ret;
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"SELECT Name, Civid, Age, Icon, Rarirty, RollverTextId, DisplayDescriptionId, DisplayNameId, MinLevel, ItemLevel, TechId FROM [Advisors]";

                ret = db.Query<Advisors>(sql, commandType: CommandType.Text).ToList();
            }
            return ret;
        }
        public void InsertAdvisors(Advisors advisors)
        {
            // Insert
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"INSERT INTO [Advisors] (Name, Civid, Age, Icon, Rarirty, RollverTextId, DisplayDescriptionId, DisplayNameId, MinLevel, ItemLevel, TechId) VALUES (@Name, @Civid, @Age, @Icon, @Rarirty, @RollverTextId, @DisplayDescriptionId, @DisplayNameId, @MinLevel, @ItemLevel, @TechId)";

                db.Execute(sql, new { Name = advisors.Name, Civid = advisors.Civid, Age = advisors.Age, Icon = advisors.Icon, Rarirty = advisors.Rarirty, RollverTextId = advisors.RollverTextId, DisplayDescriptionId = advisors.DisplayDescriptionId, DisplayNameId = advisors.DisplayNameId, MinLevel = advisors.MinLevel, ItemLevel = advisors.ItemLevel, TechId = advisors.TechId }, commandType: CommandType.Text);
            }
        }
        public void UpdateAdvisors(Advisors advisors)
        {
            // Update
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"UPDATE [Advisors] SET Name = @Name, Civid = @Civid, Age = @Age, Icon = @Icon, Rarirty = @Rarirty, RollverTextId = @RollverTextId, DisplayDescriptionId = @DisplayDescriptionId, DisplayNameId = @DisplayNameId, MinLevel = @MinLevel, ItemLevel = @ItemLevel, TechId = @TechId WHERE Name = @Name";

                db.Execute(sql, new { Name = advisors.Name, Civid = advisors.Civid, Age = advisors.Age, Icon = advisors.Icon, Rarirty = advisors.Rarirty, RollverTextId = advisors.RollverTextId, DisplayDescriptionId = advisors.DisplayDescriptionId, DisplayNameId = advisors.DisplayNameId, MinLevel = advisors.MinLevel, ItemLevel = advisors.ItemLevel, TechId = advisors.TechId }, commandType: CommandType.Text);
            }
        }
        public void DeleteAdvisors(Advisors advisors)
        {
            // Delete
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"DELETE FROM [Advisors] WHERE Name = @Name";

                db.Execute(sql, new { advisors.Name }, commandType: CommandType.Text);
            }
        }
    }
}
