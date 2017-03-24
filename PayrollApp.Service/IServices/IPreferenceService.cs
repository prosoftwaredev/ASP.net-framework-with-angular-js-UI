using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IPreferenceService
    {
        Task<PagedData<Preference>> Get(SearchDataTable search);
        Task<Preference> GetByID(long PreferenceID);
        Task<string> Create(Preference Preference);
        Task<string> Update(Preference Preference);

        Task<List<Preference>> GetAllPreferences(bool displayAll = false, bool isDelete = false);
    }
}
