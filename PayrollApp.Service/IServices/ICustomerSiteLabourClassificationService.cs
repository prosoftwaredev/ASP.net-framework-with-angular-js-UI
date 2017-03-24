using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICustomerSiteLabourClassificationService
    {
        Task<CustomerSiteLabourClassification> GetByID(long CustomerSiteLabourClassificationID);
        Task<string> Create(CustomerSiteLabourClassification CustomerSiteLabourClassification);
        Task<string> Update(CustomerSiteLabourClassification CustomerSiteLabourClassification);

        Task<CustomerSiteLabourClassification> GetAllCustomerSiteLabourClassificationsByLabourClassificationIDAndCustomerSiteID(long CustomerSiteID, long LabourClassificationID, bool displayAll = false, bool isDelete = false);
        Task<List<CustomerSiteLabourClassification>> GetAllCustomerSiteLabourClassificationsByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false);
    }
}
