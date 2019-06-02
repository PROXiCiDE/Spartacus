using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.Language
{
    public class LanguagesRepository : ILanguagesRepository
    {
        public LanguagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string _connectionString { get; }

        public List<Languages> SelectLanguages()
        {
            // Select
            List<Languages> ret;
            using (var db = new SQLiteConnection(_connectionString))
            {
                const string sql = @"SELECT Id, Locale, Filename, LocId, Symbol, Text FROM [Languages]";

                ret = db.Query<Languages>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertLanguages(Languages languages)
        {
            // Insert
            using (var db = new SQLiteConnection(_connectionString))
            {
                const string sql =
                    @"INSERT INTO [Languages] (Locale, Filename, LocId, Symbol, Text) VALUES (@Locale, @Filename, @LocId, @Symbol, @Text)";

                db.Execute(sql, new
                {
                    languages.Locale, languages.Filename, languages.LocId, languages.Symbol,
                    languages.Text
                }, commandType: CommandType.Text);
            }
        }

        public void UpdateLanguages(Languages languages)
        {
            // Update
            using (var db = new SQLiteConnection(_connectionString))
            {
                const string sql =
                    @"UPDATE [Languages] SET Locale = @Locale, Filename = @Filename, LocId = @LocId, Symbol = @Symbol, Text = @Text WHERE Id = @Id";

                db.Execute(sql, new
                {
                    languages.Id, languages.Locale, languages.Filename, languages.LocId,
                    languages.Symbol,
                    languages.Text
                }, commandType: CommandType.Text);
            }
        }

        public void DeleteLanguages(Languages languages)
        {
            // Delete
            using (var db = new SQLiteConnection(_connectionString))
            {
                const string sql = @"DELETE FROM [Languages] WHERE Id = @Id";

                db.Execute(sql, new {languages.Id}, commandType: CommandType.Text);
            }
        }
    }
}