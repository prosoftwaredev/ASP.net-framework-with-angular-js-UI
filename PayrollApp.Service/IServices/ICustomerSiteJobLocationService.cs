
using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PayrollApp.Service.IServices
{
    public interface ICustomerSiteJobLocationService
    {
        Task<CustomerSiteJobLocation> GetByID(long CustomerSiteJobLocationID);
        Task<string> Create(CustomerSiteJobLocation CustomerSiteJobLocation);
        Task<string> Update(CustomerSiteJobLocation CustomerSiteJobLocation);

        Task<List<CustomerSiteJobLocation>> GetAllCustomerSiteJobLocationsByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false);
    }
}
