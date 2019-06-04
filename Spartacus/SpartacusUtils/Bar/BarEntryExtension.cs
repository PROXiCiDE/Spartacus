﻿using System;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public static class BarEntryExtension
    {
        public static byte[] EntryToBytes(this BarFileReader barFileReader, BarFileEntry barEntry)
        {
            if (string.IsNullOrEmpty(barFileReader.Filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(barFileReader.Filename));
            if (barEntry == null)
                throw new ArgumentNullException(nameof(barEntry));

            using (var fileStream = File.Open(barFileReader.Filename, FileMode.Open, FileAccess.Read, FileShare.Read))
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
                                final.Write(buffer, 0, (int) barEntry.FileSize);
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