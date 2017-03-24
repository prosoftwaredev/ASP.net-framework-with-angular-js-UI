using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICustomerService
    {
        Task<PagedData<Customer>> Get(SearchDataTable search);
        Task<PagedData<TempCustomer>> Get(SearchCustomerSite search);
        Task<Customer> GetByID(long CustomerID);
        Task<string> Create(Customer Customer);
        Task<string> Update(Customer Customer);

        Task<string> GetAccountNumber(string str);
        Task<List<Customer>> GetAllCustomers(bool displayAll = false, bool isDelete = false);
        Task<List<Customer>> GetAllCustomers(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
