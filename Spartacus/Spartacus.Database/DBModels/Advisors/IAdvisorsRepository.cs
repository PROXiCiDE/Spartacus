using System.Collections.Generic;

namespace Spartacus.Database.DBModels.Advisors
{
    public interface IAdvisorsRepository
    {
        List<AdvisorsModel> SelectAdvisors();
        void InsertAdvisors(AdvisorsModel advisors);
        void UpdateAdvisors(AdvisorsModel advisors);
        void DeleteAdvisors(AdvisorsModel advisors);
    }
}