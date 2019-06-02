using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.StringTableLocations
{
    public class StringTableLocationsRepository : IStringTableLocationsRepository
    {
        private readonly string _connstring;

        public StringTableLocationsRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        public List<StringTableLocations> SelectStringTableLocations()
        {
            // Select
            List<StringTableLocations> ret;
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql = @"SELECT TableName, LocStart, LocEnd FROM [StringTableLocations]";

                ret = db.Query<StringTableLocations>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertStringTableLocations(StringTableLocations stringtablelocations)
        {
            // Insert
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql =
                    @"INSERT INTO [StringTableLocations] (TableName, LocStart, LocEnd) VALUES (@TableName, @LocStart, @LocEnd)";

                db.Execute(sql,
                    new {stringtablelocations.TableName, stringtablelocations.LocStart, stringtablelocations.LocEnd},
                    commandType: CommandType.Text);
            }
        }

        public void UpdateStringTableLocations(StringTableLocations stringtablelocations)
        {
            // Update
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql =
                    @"UPDATE [StringTableLocations] SET TableName = @TableName, LocStart = @LocStart, LocEnd = @LocEnd WHERE TableName = @TableName";

                db.Execute(sql,
                    new {stringtablelocations.TableName, stringtablelocations.LocStart, stringtablelocations.LocEnd},
                    commandType: CommandType.Text);
            }
        }

        public void DeleteStringTableLocations(StringTableLocations stringtablelocations)
        {
            // Delete
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql = @"DELETE FROM [StringTableLocations] WHERE TableName = @TableName";

                db.Execute(sql, new {stringtablelocations.TableName}, commandType: CommandType.Text);
            }
        }
    }
}