using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.AbstractFileSystem;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;
using SpartacusUtils.Xml.Helpers;

namespace SpartacusUtils.Bar
{
    public class BarFileSystem : AbstractFileSystem<BarFileEntry>
    {
        public BarFileSystem(string sourceFile) : this()
        {
            Open(sourceFile);
        }

        public BarFileSystem()
        {
        }

        public bool UsePhysicalFileFirst { get; set; } = false;

        public BarFile ArchiveReader { get; set; }

        public string Root { get; set; }

        public uint NumberOfFiles { get; set; }

        public string FileName { get; set; }

        public new bool Open(string sourceFile)
        {
            if (!File.Exists(sourceFile))
                return false;

            if (string.IsNullOrEmpty(sourceFile))
                throw new ArgumentException("Value cannot be null or empty.", nameof(sourceFile));
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException($"File '{sourceFile}' does not exist.", sourceFile);

            using (var stream = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    ArchiveEntries = new List<BarFileEntry>();

                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    var barFileHeader = new BarFileHeader(reader);
                    reader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin);
                    ArchiveReader = new BarFile(reader);

                    ArchiveReader.BarFileEntrys.ForEach(x => { ArchiveEntries.Add(new BarFileEntry(x)); });

                    FileName = sourceFile;
                    Root = ArchiveReader.RootPath;
                    NumberOfFiles = ArchiveReader.NumberOfRootFiles;
                }
            }

            return ArchiveReader != null;
        }

        /// <summary>
        /// Get an file entry inside of the Archive
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns>BarFileEntry</returns>
        public new BarFileEntry GetEntry(string sourceFile)
        {
            var entries = ArchiveEntries;

            //Prevent multiple enumerations
            var barFileEntries = entries as BarFileEntry[] ?? entries.ToArray();

            var result = barFileEntries.FirstOrDefault(x =>
                CompareFileNames(sourceFile, x));

            //User may have checked for a valid XML file, Non XMB Format
            //Recheck to see if XMB format exists
            if (IsValidXmlExtension(sourceFile) && result == null)
                return barFileEntries.FirstOrDefault(x =>
                    CompareFileNames($"{sourceFile}.xmb", x));

            return result;
        }


        /// <summary>
        /// Read the file contents into an object of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <returns>Object</returns>
        public new T ReadEntry<T>(BarFileEntry entry) where T : class
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            var fileExt = PathUtils.GetExtensionWithoutDot(entry.FullName).ToLower();

            if (string.IsNullOrEmpty(fileExt)) return default;
            if (IsValidXmlExtension(fileExt))
            {
                var contents = this.EntryToBytes(entry)?.EncodeXmlToString();
                if (contents != null) return XmlUtils.DeserializeFromXml<T>(contents);
            }

            return this.EntryToBytes(entry) as T;
        }

        /// <summary>
        /// Find many entries that resides in the archive from a given search pattern
        /// </summary>
        /// <remarks>Wildcard supported (*?)</remarks>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public new IEnumerable<BarFileEntry> FindEntries(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern))
                throw new ArgumentException("Value cannot be null or empty.", nameof(searchPattern));
            return ArchiveEntries.Where(x => Regex.IsMatch(x.FullName, searchPattern.ToWildCard()));
        }


        #region Private Methods

        
      
        private static bool CompareFileNames(string sourceFile, BarFileEntry entry)
        {
            return entry.FullName.Equals(sourceFile, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsValidImageExtension(string fileExtension)
        {
            var extList = new List<string>
            {
                "tga",
                "jpeg",
                "jpg",
                "png",
                "ddt"
            };
            return PathUtils.ContainsExtension(fileExtension, extList);
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

            return PathUtils.ContainsExtension(fileExtension, extList);
        }

        #endregion
    }
}