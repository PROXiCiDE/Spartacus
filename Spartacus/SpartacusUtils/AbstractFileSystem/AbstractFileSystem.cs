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

        public TEntry GetEntry(string sourceFile)
        {
            throw new NotImplementedException();
        }

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