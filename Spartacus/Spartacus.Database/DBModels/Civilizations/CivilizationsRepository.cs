using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.Civilizations
{
    public class CivilizationsRepository : ICivilizationsRepository
    {
        private readonly string _connstring;

        public CivilizationsRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        public List<CivilizationsModel> SelectCivilizations()
        {
            // Select
            List<CivilizationsModel> ret;
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"SELECT CivilizationId, DisplayNameId, RolloverNameId, ShieldTexture, ShieldGreyTexture, Age0, Age1, Age2, Age3, StorehouseTechId FROM [Civilizations]";

                ret = db.Query<CivilizationsModel>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertCivilizations(CivilizationsModel civilizations)
        {
            // Insert
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"INSERT INTO [Civilizations] (CivilizationId, DisplayNameId, RolloverNameId, ShieldTexture, ShieldGreyTexture, Age0, Age1, Age2, Age3, StorehouseTechId) VALUES (@CivilizationId, @DisplayNameId, @RolloverNameId, @ShieldTexture, @ShieldGreyTexture, @Age0, @Age1, @Age2, @Age3, @StorehouseTechId)";

                db.Execute(sql, new
                {
                    civilizations.CivilizationId, civilizations.DisplayNameId, civilizations.RolloverNameId,
                    civilizations.ShieldTexture,
                    civilizations.ShieldGreyTexture,
                    civilizations.Age0,
                    civilizations.Age1,
                    civilizations.Age2,
                    civilizations.Age3,
                    civilizations.StorehouseTechId
                }, commandType: CommandType.Text);
            }
        }

        public void UpdateCivilizations(CivilizationsModel civilizations)
        {
            // Update
            using (var db = new SqlConnection(_connstring))
            {
                const string sql =
                    @"UPDATE [Civilizations] SET CivilizationId = @CivilizationId, DisplayNameId = @DisplayNameId, RolloverNameId = @RolloverNameId, ShieldTexture = @ShieldTexture, ShieldGreyTexture = @ShieldGreyTexture, Age0 = @Age0, Age1 = @Age1, Age2 = @Age2, Age3 = @Age3, StorehouseTechId = @StorehouseTechId WHERE CivilizationId = @CivilizationId";

                db.Execute(sql, new
                {
                    civilizations.CivilizationId, civilizations.DisplayNameId, civilizations.RolloverNameId,
                    civilizations.ShieldTexture,
                    civilizations.ShieldGreyTexture,
                    civilizations.Age0,
                    civilizations.Age1,
                    civilizations.Age2,
                    civilizations.Age3,
                    civilizations.StorehouseTechId
                }, commandType: CommandType.Text);
            }
        }

        public void DeleteCivilizations(CivilizationsModel civilizations)
        {
            // Delete
            using (var db = new SqlConnection(_connstring))
            {
                const string sql = @"DELETE FROM [Civilizations] WHERE CivilizationId = @CivilizationId";

                db.Execute(sql, new {civilizations.CivilizationId}, commandType: CommandType.Text);
            }
        }
    }
}