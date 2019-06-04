using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Advisors
{
    public interface IAdvisorsRepository
    {
        List<Advisors> SelectAdvisors();
        void InsertAdvisors(Advisors advisors);
        void UpdateAdvisors(Advisors advisors);
        void DeleteAdvisors(Advisors advisors);
    }
}