using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace Spartacus.Database.DBModels.FileChecksum
{
    public class FileChecksumRepository : IFileChecksumRepository
    {
        private readonly string _connstring;

        public FileChecksumRepository(string connectionString)
        {
            _connstring = connectionString;
        }

        public List<FileChecksumModel> SelectFileChecksum()
        {
            // Select
            List<FileChecksumModel> ret;
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql =
                    @"SELECT Fullname, Filename, Checksum, LastWriteTime FROM [FileChecksum]";

                ret = db.Query<FileChecksumModel>(sql, commandType: CommandType.Text).ToList();
            }

            return ret;
        }

        public void InsertFileChecksum(FileChecksumModel filechecksum)
        {
            // Insert
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql =
                    @"INSERT INTO [FileChecksum] (Fullname, Filename, Checksum, LastWriteTime) VALUES (@Fullname, @Filename, @Checksum, @LastWriteTime)";

                db.Execute(sql, new
                {
                    filechecksum.Fullname,
                    filechecksum.Filename, filechecksum.Checksum,
                    filechecksum.LastWriteTime
                }, commandType: CommandType.Text);
            }
        }

        public void UpdateFileChecksum(FileChecksumModel filechecksum)
        {
            // Update
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql =
                    @"UPDATE [FileChecksum] SET Filename = @Filename, FullFilename = @FullFilename, Checksum = @Checksum, Location = @Location, LastWriteTime = @LastWriteTime WHERE Filename = @Filename";

                db.Execute(sql, new
                {
                    filechecksum.Filename, filechecksum.Fullname, filechecksum.Checksum,
                    filechecksum.LastWriteTime
                }, commandType: CommandType.Text);
            }
        }

        public void DeleteFileChecksum(FileChecksumModel filechecksum)
        {
            // Delete
            using (var db = new SQLiteConnection(_connstring))
            {
                const string sql = @"DELETE FROM [FileChecksum] WHERE Filename = @Filename";

                db.Execute(sql, new {filechecksum.Filename}, commandType: CommandType.Text);
            }
        }
    }
}