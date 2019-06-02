using System.Collections.Generic;

namespace Spartacus.Database.DBModels.FileChecksum
{
    public interface IFileChecksumRepository
    {
        List<FileChecksum> SelectFileChecksum();
        void InsertFileChecksum(FileChecksum filechecksum);
        void UpdateFileChecksum(FileChecksum filechecksum);
        void DeleteFileChecksum(FileChecksum filechecksum);
    }
}