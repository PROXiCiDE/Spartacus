using System;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;
using SpartacusUtils.AbstractFileSystem;

namespace SpartacusUtils.Bar
{
    public sealed class BarFileEntry : AbstractFileEntry, IBarFileEntry
    {
        public BarFileEntry(bool isArchivedFile, string fileName, string fullName, long fileSize,
            long offset, DateTime lastWriteTime)
        {
            IsArchivedFile = isArchivedFile;
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
            FullName = entry.FullName;
            FileName = entry.FileName;
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = entry.LastWriteTime;

            IsArchivedFile = entry.IsArchivedFile;
        }

        public BarFileEntry(BarEntry entry)
        {
            FileName = Path.GetFileName(entry.FileName);
            FullName = entry.FileName;
            FileSize = entry.FileSize;
            FileSize2 = entry.FileSize2;
            Offset = entry.Offset;
            LastWriteTime = entry.LastWriteTime.GetDateTime();

            //Always Local File
            IsArchivedFile = true;
        }

        public BarFileEntry(string fileName, long fileSize, long offset,
            DateTime lastWriteTime)
        {
            FullName = fileName;
            FileName = Path.GetFileName(fileName);
            FileSize = fileSize;
            FileSize2 = fileSize;
            Offset = offset;
            LastWriteTime = lastWriteTime;
        }


        public bool IsArchivedFile { get; set; }
        public string FileName { get; set; }
        public bool IsPhysicalFile { get; set; }
        public long Offset { get; set; }
        public long FileSize2 { get; set; }

        public override string FullName { get; set; }
        public override long FileSize { get; set; }

        public override DateTime? LastWriteTime { get; set; }

        #region Not Implemented

        public override long? CompressedSize
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override DateTime? CreationTime
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override DateTime? LastAccessedTime
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override DateTime? ArchivedTime
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override bool? IsDirectory
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override bool? IsCompressed
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override bool? IsEncrypted
        {
            get => null;
            set => throw new NotImplementedException();
        }

        public override long? CrcChecksum
        {
            get => null;
            set => throw new NotImplementedException();
        }

        #endregion
    }
}