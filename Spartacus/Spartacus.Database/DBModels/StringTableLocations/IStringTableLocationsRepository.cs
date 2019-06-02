using System.Collections.Generic;

namespace Spartacus.Database.DBModels.StringTableLocations
{
    public interface IStringTableLocationsRepository
    {
        List<StringTableLocations> SelectStringTableLocations();
        void InsertStringTableLocations(StringTableLocations stringtablelocations);
        void UpdateStringTableLocations(StringTableLocations stringtablelocations);
        void DeleteStringTableLocations(StringTableLocations stringtablelocations);
    }
}