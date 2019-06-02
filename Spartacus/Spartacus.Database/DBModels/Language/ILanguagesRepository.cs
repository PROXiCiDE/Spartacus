using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Language
{
    public interface ILanguagesRepository
    {
        List<Languages> SelectLanguages();
        void InsertLanguages(Languages languages);
        void UpdateLanguages(Languages languages);
        void DeleteLanguages(Languages languages);
    }
}