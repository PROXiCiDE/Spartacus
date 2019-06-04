using System.Collections.Generic;

namespace Spartacus.Database.DBModels.FileChecksum
{
    public interface IFileChecksumRepository
    {
        List<FileChecksumModel> SelectFileChecksum();
        void InsertFileChecksum(FileChecksumModel filechecksum);
        void UpdateFileChecksum(FileChecksumModel filechecksum);
        void DeleteFileChecksum(FileChecksumModel filechecksum);
    }
}