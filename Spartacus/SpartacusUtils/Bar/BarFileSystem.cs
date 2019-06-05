using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.AbstractFileSystem;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;
using SpartacusUtils.Xml.Helpers;

namespace SpartacusUtils.Bar
{
    public class BarFileSystem : AbstractFileSystem<BarFileReader, BarFileEntry>
    {
        public BarFileSystem(BarFileReader archiveReader) : base(archiveReader)
        {
        }

        public BarFileSystem(string sourceFile) : this()
        {
            Open(sourceFile);
        }

        public BarFileSystem()
        {
        }

        public new bool Open(string sourceFile)
        {
            if (!File.Exists(sourceFile))
                return false;

            ArchiveReader = new BarFileReader(sourceFile);
            ArchiveEntries = ArchiveReader.BarFileEntries;
            return ArchiveReader != null;
        }

        /// <summary>
        ///     Single file search doesn't support wildcard patterns
        /// </summary>
        /// <param name="sourceFile">
        ///     Case insensitive, If sourceFile == file.xml then it will also search for file.xml.xmb the first result isn't found.
        ///     Must be full path as it is in the entry table
        /// </param>
        /// <returns>BarFileEntry</returns>
        public new BarFileEntry GetEntry(string sourceFile)
        {
            var entries = ArchiveEntries;
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
        public new T ReadEntry<T>(BarFileEntry entry) where T : class
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            var fileExt = PathUtils.GetExtensionWithoutDot(entry.FullName).ToLower();
            if (string.IsNullOrEmpty(fileExt))
                return null;

            if (IsValidXmlExtension(fileExt))
            {
                var contents = ArchiveReader.EntryToBytes(entry)?.EncodeXmlToString();
                if (contents != null) return XmlUtils.DeserializeFromXml<T>(contents);
            }
            else if (fileExt == "ddt")
            {
            }

            return default;
        }

        /// <summary>
        ///     Find Many entries of a given pattern
        /// </summary>
        /// <param name="pattern">Wildcard patterns are acceptable ? and *</param>
        /// <returns>IEnumeration of BarFileEntry</returns>
        public new IEnumerable<BarFileEntry> FindEntries(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern))
                throw new ArgumentException("Value cannot be null or empty.", nameof(searchPattern));
            return ArchiveEntries.Where(x => Regex.IsMatch(x.FullName, searchPattern.ToWildCard()));
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