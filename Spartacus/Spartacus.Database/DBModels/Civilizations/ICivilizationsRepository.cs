using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Civilizations
{
    public interface ICivilizationsRepository
    {
        List<Civilizations> SelectCivilizations();
        void InsertCivilizations(Civilizations civilizations);
        void UpdateCivilizations(Civilizations civilizations);
        void DeleteCivilizations(Civilizations civilizations);
    }
}