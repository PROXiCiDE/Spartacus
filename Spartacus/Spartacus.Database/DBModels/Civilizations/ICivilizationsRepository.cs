using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Civilizations
{
    public interface ICivilizationsRepository
    {
        List<CivilizationsModel> SelectCivilizations();
        void InsertCivilizations(CivilizationsModel civilizations);
        void UpdateCivilizations(CivilizationsModel civilizations);
        void DeleteCivilizations(CivilizationsModel civilizations);
    }
}