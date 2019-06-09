using System;
using System.Collections.Generic;
using System.IO;

namespace SpartacusUtils.AbstractFileSystem
{
    public abstract class AbstractFileSystem<TEntry> : IAbstractFileSystem<TEntry>
    {
        public AbstractFileSystem()
        {
        }

        public string HomePath { get; set; }
        public string CurrentResourceFile { get; set; }
        public IList<TEntry> ArchiveEntries { get; set; }
        public IList<TEntry> LocalEntries { get; set; }


        /// <summary>
        /// Get a file entry inside the archive
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns>Archive Entry information</returns>
        public TEntry GetEntry(string sourceFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read the contents of the file entry into an Object type of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <returns>Object Template</returns>
        public T ReadEntry<T>(TEntry entry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntry> FindEntries(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public void Open(string sourceFile)
        {
            throw new NotImplementedException();
        }

        private void SetupPaths()
        {
            HomePath = Path.GetDirectoryName(CurrentResourceFile);

            throw new NotImplementedException();
        }

        private void ThrowIfNotLoaded()
        {
            throw new NotImplementedException();
        }
    }
}