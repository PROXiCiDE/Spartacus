using System;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public static class BarEntryExtension
    {
        public static DateTime GetDateTime(this BarEntryLastWriteTime lastWriteTime)
        {
            return new DateTime(lastWriteTime.Year,
                lastWriteTime.Month,
                lastWriteTime.Day,
                lastWriteTime.Hour,
                lastWriteTime.Minute,
                lastWriteTime.Second,
                lastWriteTime.Milliseconds);
        }

        public static byte[] EntryToBytes(this BarFileSystem barFileReader, BarFileEntry barEntry, bool useLocalFile = true)
        {
            if (string.IsNullOrEmpty(barFileReader.FileName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(barFileReader.FileName));
            if (barEntry == null)
                throw new ArgumentNullException(nameof(barEntry));

            if (barEntry.IsArchivedFile && useLocalFile)
                using (var fileStream = File.Open(barFileReader.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return barEntry.StreamToBytes(fileStream);
                }
            else
            {
                using (var fileStream = File.Open(barEntry.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return barEntry.StreamToBytes(fileStream);
                }
            }
        }

        private static byte[] StreamToBytes(this BarFileEntry barEntry, Stream stream, bool useLocalFile = true)
        {
            using (var binReader = new BinaryReader(stream))
            {

                //Determine if we'll be using the bar file or a physical file
                if (barEntry.IsArchivedFile && useLocalFile)
                    binReader.BaseStream.Seek(barEntry.Offset, SeekOrigin.Begin);

                using (var memoryStream = new MemoryStream())
                {
                    var buffer = new byte[4096];
                    int read;
                    var totalRead = 0L;
                    while ((read = binReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (read > barEntry.FileSize)
                        {
                            totalRead = barEntry.FileSize;
                            memoryStream.Write(buffer, 0, (int)barEntry.FileSize);
                        }
                        else if (totalRead + read <= barEntry.FileSize)
                        {
                            totalRead += read;
                            memoryStream.Write(buffer, 0, read);
                        }
                        else if (totalRead + read > barEntry.FileSize)
                        {
                            var leftToRead = barEntry.FileSize - totalRead;
                            totalRead = barEntry.FileSize;
                            memoryStream.Write(buffer, 0, Convert.ToInt32(leftToRead));
                        }

                        if (totalRead >= barEntry.FileSize)
                            break;
                    }

                    return memoryStream.ToArray();
                }
            }
        }
    }
}