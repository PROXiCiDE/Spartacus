using System;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public static class BarEntryExtension
    {
        public static byte[] ToBytes(this BarFileReader barFileReader, BarEntry barEntry)
        {
            if (string.IsNullOrEmpty(barFileReader._filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(barFileReader._filename));
            if (barEntry == null)
                throw new ArgumentNullException(nameof(barEntry));

            using (var fileStream = File.Open(barFileReader._filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var binReader = new BinaryReader(fileStream))
                {
                    binReader.BaseStream.Seek(barEntry.Offset, SeekOrigin.Begin);

                    using (var final = new MemoryStream())
                    {
                        var buffer = new byte[4096];
                        int read;
                        var totalread = 0L;
                        while ((read = binReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (read > barEntry.FileSize)
                            {
                                totalread = barEntry.FileSize;
                                final.Write(buffer, 0, barEntry.FileSize);
                            }
                            else if (totalread + read <= barEntry.FileSize)
                            {
                                totalread += read;
                                final.Write(buffer, 0, read);
                            }
                            else if (totalread + read > barEntry.FileSize)
                            {
                                var leftToRead = barEntry.FileSize - totalread;
                                totalread = barEntry.FileSize;
                                final.Write(buffer, 0, Convert.ToInt32(leftToRead));
                            }

                            if (totalread >= barEntry.FileSize)
                                break;
                        }

                        return final.ToArray();
                    }
                }
            }
        }
    }
}