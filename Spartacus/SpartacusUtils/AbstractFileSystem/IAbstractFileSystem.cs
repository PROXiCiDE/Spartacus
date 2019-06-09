using System.Collections;
using System.Collections.Generic;

namespace SpartacusUtils.AbstractFileSystem
{
    public interface IAbstractFileSystem<TEntry>
    {
        IList<TEntry> ArchiveEntries { get; set; }
        IList<TEntry> LocalEntries { get; set; }

        TEntry GetEntry(string sourceFile);

        T ReadEntry<T>(TEntry entry);
        IEnumerable<TEntry> FindEntries(string searchPattern);
    }
}