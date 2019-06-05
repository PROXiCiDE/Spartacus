using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public class BarFileEntry : IBarFileEntry
    {
        public BarFileEntry(bool isLocalFile, string fileName, string fullName, long fileSize,
            long offset, BarFileLastWriteTime lastWriteTime)
        {
            IsLocalFile = isLocalFile;
            FileName = fileName;
            FullName = fullName;
            FileSize = fileSize;
            FileSize2 = FileSize;
            Offset = offset;
            LastWriteTime = lastWriteTime;
        }

        public BarFileEntry()
        {
        }

        public BarFileEntry(BarFileEntry entry)
        {
            IsLocalFile = entry.IsLocalFile;
            FullName = entry.FileName;
            FileName = Path.GetFileName(entry.FileName);
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = entry.LastWriteTime;
        }

        public BarFileEntry(BarEntry entry)
        {
            IsLocalFile = false;
            FileName = Path.GetFileName(entry.FileName);
            FullName = entry.FileName;
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = new BarFileLastWriteTime(entry.LastWriteTime);
        }

        public BarFileEntry(string fileName, long fileSize, long offset,
            BarFileLastWriteTime lastWriteTime)
        {
            IsLocalFile = false;
            FullName = fileName;
            FileName = Path.GetFileName(fileName);
            FileSize = fileSize;
            FileSize2 = fileSize;
            Offset = offset;
            LastWriteTime = lastWriteTime;
        }

        public bool IsLocalFile { get; set; }
        public string FileName { get; set; }
        public string FullName { get; set; }

        public long FileSize { get; set; }
        public long FileSize2 { get; set; }
        public long Offset { get; set; }
        public BarFileLastWriteTime LastWriteTime { get; set; }
    }
}