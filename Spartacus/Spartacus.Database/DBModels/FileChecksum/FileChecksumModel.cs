using System;

namespace Spartacus.Database.DBModels.FileChecksum
{
    public class FileChecksumModel
    {
        public FileChecksumModel(string fullname, string filename, string checksum, long lastWriteTime)
        {
            Fullname = fullname;
            Filename = filename;
            Checksum = checksum;
            LastWriteTime = lastWriteTime;
        }

        public string Filename { get; set; }
        public string Fullname { get; set; }
        public string Checksum { get; set; }
        public long LastWriteTime { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Filename)}: {Filename}, {nameof(Fullname)}: {Fullname}, {nameof(Checksum)}: {Checksum}, {nameof(LastWriteTime)}: {new DateTime(LastWriteTime, DateTimeKind.Utc)}";
        }
    }
}