using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public class BarFileEntry : IBarFileEntry
    {
        public BarFileEntry()
        {
        }

        public BarFileEntry(BarFileEntry entry)
        {
            FullName = entry.FileName;
            FileName = Path.GetFileName(entry.FileName);
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = entry.LastWriteTime;
        }

        public BarFileEntry(BarEntry entry)
        {
            FileName = Path.GetFileName(entry.FileName);
            FullName = entry.FileName;
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = new BarFileLastWriteTime(entry.LastWriteTime);
        }

        public BarFileEntry(string fileName, long fileSize, long fileSize2, long offset, BarFileLastWriteTime lastWriteTime)
        {
            FullName = fileName;
            FileName = Path.GetFileName(fileName);
            FileSize = fileSize;
            FileSize2 = fileSize2;
            Offset = offset;
            LastWriteTime = lastWriteTime;
        }

        public string FileName { get; set; }
        public string FullName { get; set; }

        public long FileSize { get; set; }
        public long FileSize2 { get; set; }
        public long Offset { get; set; }
        public BarFileLastWriteTime LastWriteTime { get; set; }
    }
}