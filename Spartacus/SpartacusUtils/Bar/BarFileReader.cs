using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;
using SpartacusUtils.Xml.Helpers;

namespace SpartacusUtils.Bar
{
    public class BarFileReader
    {
        public readonly BarFile BarFile;
        public readonly string Filename;

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
                    BarFile = new BarFile(reader);
                    Filename = sourceFile;
                }
            }
        }

        public BarFileReader(BarFile barFile)
        {
            BarFile = barFile;
        }

        /// <summary>
        /// Find Many entries of a given pattern
        /// </summary>
        /// <param name="pattern">Wildcard patterns are acceptable ? and *</param>
        /// <returns>IEnumeration of BarEntry</returns>
        public IEnumerable<BarEntry> FindEntries(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Value cannot be null or empty.", nameof(pattern));

            return BarFile.BarFileEntrys.Where(x => Regex.IsMatch(x.FileName, pattern.ToWildCard()));
        }

        /// <summary>
        /// Get a entry from the bar file
        /// Doesn't support Wildcard search
        /// </summary>
        /// <param name="sourceFile">Full path of the entry, search is case insensitive</param>
        /// <returns>BarEntry</returns>
        public BarEntry GetEntry(string sourceFile)
        {
            return BarFile.BarFileEntrys.FirstOrDefault(x =>
                x.FileName.Equals(sourceFile, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Read an bar entry contents into an object of a T
        /// </summary>
        /// <typeparam name="T">Object Type to Create</typeparam>
        /// <param name="entry">BarEntry</param>
        /// <returns>Object Type of T</returns>
        public T ReadEntry<T>(BarEntry entry) where T : class
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            T results = default(T);
            var fileExt = PathUtils.GetExtensionWithoutDot(entry.FileName).ToLower();
            if (string.IsNullOrEmpty(fileExt))
                return null;

            if (IsValidXmlExtension(fileExt))
            {
                var contents = this.EntryToBytes(entry)?.EncodeXmlToString();
                if (contents != null)
                {
                    return XmlUtils.DeserializeFromXml<T>(contents);
                }
            }
            else if (fileExt == "ddt")
            {
            }
            return null;
        }

        private bool IsValidXmlExtension(string fileExtension)
        {
            var extList = new List<string>()
            {
                "region",
                "xml",
                "xmb",
                "dataset",
                "character",
                "tactics",
                "SpawnerItem",
                "blueprint",
                "physics",
                "shp",
                "quest",
                "xsd",
                "cpn",
                "dtd"
            };
            if (fileExtension.StartsWith("."))
                fileExtension = fileExtension.Replace(".", "");
            return extList.Contains(fileExtension.ToLower());
        }
    }
}