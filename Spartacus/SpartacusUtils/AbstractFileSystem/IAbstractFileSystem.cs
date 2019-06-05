using System.Collections.Generic;

namespace SpartacusUtils.AbstractFileSystem
{
    public interface IAbstractFileSystem<TArchiveReader, TEntry>
    {
        TArchiveReader ArchiveReader { get; set; }
        IEnumerable<TEntry> ArchiveEntries { get; set; }
        IEnumerable<TEntry> LocalEntries { get; set; }

        TEntry GetEntry(string sourceFile);
        T ReadEntry<T>(TEntry entry);
        IEnumerable<TEntry> FindEntries(string searchPattern);
    }
}