using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICustomerSiteService
    {
        Task<PagedData<CustomerSite>> Get(SearchCustomerSite search);
        Task<CustomerSite> GetByID(long CustomerSiteID);
        Task<string> Create(CustomerSite CustomerSite);
        Task<string> Update(CustomerSite CustomerSite);

        //Task<CustomerSite> GetAllCustomerSitesByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false);
        Task<List<CustomerSite>> GetAllCustomerSitesByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false);

        Task<string> GetAccountNumber(string str);
        Task<bool> GetIsPrimaryCustomerSite(long CustomerID, bool isDelete = false);
        Task<long> GetIsPrimaryCustomerSite(long CustomerID);
    }
}
