using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectCeleste.GameFiles.Tools.Bar;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.Bar
{
    public class BarFileReader
    {
        public readonly BarFile _barFilesInfo;
        public readonly string _filename;

        public BarFileReader(string sourceFile)
        {
            if (string.IsNullOrEmpty(sourceFile))
                throw new ArgumentException("Value cannot be null or empty.", nameof(sourceFile));
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException($"File '{sourceFile}' does not exist.", sourceFile);

            using (var stream = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    var barFileHeader = new BarFileHeader(reader);
                    reader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin);
                    _barFilesInfo = new BarFile(reader);
                    _filename = sourceFile;
                }
            }
        }

        public BarFileReader(BarFile barFile)
        {
            _barFilesInfo = barFile;
        }

        public IEnumerable<BarEntry> FindEntries(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Value cannot be null or empty.", nameof(pattern));

            return _barFilesInfo.BarFileEntrys.Where(x => Regex.IsMatch(x.FileName, pattern.ToWildCard()));
        }

        public BarEntry GetEntry(string sourceFile)
        {
            return _barFilesInfo.BarFileEntrys.FirstOrDefault(x =>
                x.FileName.Equals(sourceFile, StringComparison.OrdinalIgnoreCase));
        }
    }
}