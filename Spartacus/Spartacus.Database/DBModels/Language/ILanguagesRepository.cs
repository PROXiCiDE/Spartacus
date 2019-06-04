using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Language
{
    public interface ILanguagesRepository
    {
        List<LanguagesModel> SelectLanguages();
        void InsertLanguages(LanguagesModel languages);
        void UpdateLanguages(LanguagesModel languages);
        void DeleteLanguages(LanguagesModel languages);
    }
}