using System;

namespace SpartacusUtils.AbstractFileSystem
{
    public interface IAbstractFileEntry
    {
        string FullName { get; set; }
        long FileSize { get; set; }
        long? CompressedSize { get; set; }
        DateTime? LastWriteTime { get; set; }
        DateTime? CreationTime { get; set; }
        DateTime? LastAccessedTime { get; set; }
        DateTime? ArchivedTime { get; set; }
        bool? IsDirectory { get; set; }
        bool? IsCompressed { get; set; }
        bool? IsEncrypted { get; set; }
        long? CrcChecksum { get; set; }
    }
}
