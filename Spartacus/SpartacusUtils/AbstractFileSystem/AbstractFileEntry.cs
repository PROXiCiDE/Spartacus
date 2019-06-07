using System;

namespace SpartacusUtils.AbstractFileSystem
{
    public abstract class AbstractFileEntry : IAbstractFileEntry
    {
        public abstract string FullName { get; set; }
        public abstract long FileSize { get; set; }
        public abstract long? CompressedSize { get; set; }
        public abstract DateTime? LastWriteTime { get; set; }
        public abstract DateTime? CreationTime { get; set; }
        public abstract DateTime? LastAccessedTime { get; set; }
        public abstract DateTime? ArchivedTime { get; set; }
        public abstract bool? IsDirectory { get; set; }
        public abstract bool? IsCompressed { get; set; }
        public abstract bool? IsEncrypted { get; set; }
        public abstract long? CrcChecksum { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}