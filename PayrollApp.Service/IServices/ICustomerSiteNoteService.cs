using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICustomerSiteNoteService
    {
        Task<CustomerSiteNote> GetByID(long CityID);
        Task<string> Create(CustomerSiteNote CustomerSiteNote);
        Task<string> Update(CustomerSiteNote CustomerSiteNote);
        Task<List<CustomerSiteNote>> GetAllCustomerSiteNotesByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false);
    }
}
