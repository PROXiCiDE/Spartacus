using System;
using System.Collections.Generic;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.Bar
{
    public class BarFileReader
    {
        private readonly BarFile _barFile;
        public readonly string Filename;
        private bool _readEntries;

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

        public List<BarFileEntry> BarFileEntries { get; set; }

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
    }
}