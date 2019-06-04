using System;
using System.Collections.Generic;
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
        private readonly BarFile _barFile;
        public readonly string Filename;
        private bool _readEntries;
        private List<BarFileEntry> _barFileEntries;

        public BarFileReader()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="sourceFile"></param>
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
                    BarFileEntries = new List<BarFileEntry>();

                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    var barFileHeader = new BarFileHeader(reader);
                    reader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin);
                    _barFile = new BarFile(reader);
                    Filename = sourceFile;

                    EnsureDirectoryRead();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="barFile"></param>
        public BarFileReader(BarFile barFile)
        {
            _barFile = barFile;
        }

        public List<BarFileEntry> BarFileEntries
        {
            get => _barFileEntries;
            set => _barFileEntries = value;
        }

        private void EnsureDirectoryRead()
        {
            if (_readEntries)
                return;
            _barFile.BarFileEntrys.ForEach(x => { BarFileEntries.Add(new BarFileEntry(x)); });
            _readEntries = true;
        }

        /// <summary>
        ///     Get all entries inside a bar file
        /// </summary>
        /// <returns>IEnumeration of BarFileEntry</returns>
        public IEnumerable<BarFileEntry> GetEntries()
        {
            return BarFileEntries;
        }

        /// <summary>
        ///     Find Many entries of a given pattern
        /// </summary>
        /// <param name="pattern">Wildcard patterns are acceptable ? and *</param>
        /// <returns>IEnumeration of BarFileEntry</returns>
        public IEnumerable<BarFileEntry> FindEntries(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Value cannot be null or empty.", nameof(pattern));

            return GetEntries().Where(x => Regex.IsMatch(x.FullName, pattern.ToWildCard()));
        }

        /// <summary>
        ///     Get a entry from the bar file
        ///     Doesn't support Wildcard search
        /// </summary>
        /// <param name="sourceFile">Full path of the entry, search is case insensitive</param>
        /// <returns>BarFileEntry</returns>
        public BarFileEntry GetEntry(string sourceFile)
        {
            var entries = GetEntries();
            var barFileEntries = entries as BarFileEntry[] ?? entries.ToArray();

            var result = barFileEntries.FirstOrDefault(x =>
                x.FullName.Equals(sourceFile, StringComparison.OrdinalIgnoreCase));

            //User may have checked for a valid XML file, Non XMB Format
            //Recheck to see if XMB format exists
            if (IsValidXmlExtension(sourceFile) && result == null)
                return barFileEntries.FirstOrDefault(x =>
                    x.FullName.Equals($"{sourceFile}.xmb", StringComparison.OrdinalIgnoreCase));

            return result;
        }

        /// <summary>
        ///     Read an bar entry contents into an object of a T
        /// </summary>
        /// <typeparam name="T">Object Type to Create</typeparam>
        /// <param name="entry">BarEntry</param>
        /// <returns>Object Type of T</returns>
        public T ReadEntry<T>(BarFileEntry entry) where T : class
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            var fileExt = PathUtils.GetExtensionWithoutDot(entry.FullName).ToLower();
            if (string.IsNullOrEmpty(fileExt))
                return null;

            if (IsValidXmlExtension(fileExt))
            {
                var contents = this.EntryToBytes(entry)?.EncodeXmlToString();
                if (contents != null) return XmlUtils.DeserializeFromXml<T>(contents);
            }
            else if (fileExt == "ddt")
            {
            }

            return default;
        }

        private bool IsValidXmlExtension(string fileExtension)
        {
            var extList = new List<string>
            {
                "region",
                "xml",
                "xmb",
                "dataset",
                "character",
                "tactics",
                "spawneritem",
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