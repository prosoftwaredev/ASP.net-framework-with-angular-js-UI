using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeService
    {
        Task<PagedData<Employee>> Get(SearchDataTable search);
        Task<PagedData<Employee>> GetSearchResult(SearchDataTable search);
        Task<List<Employee>> GetSearchResult(long SkillID, long CertificationID);
        Task<PagedData<Employee>> Get(SearchEmployee search);
        Task<Employee> GetByID(long EmployeeID);
        Task<string> Create(Employee Employee);
        Task<string> Update(Employee Employee);

        Task<string> GetAccountNumber(string str);
        Task<bool> GetSINNumber(string str);
        Task<List<Employee>> GetAllEmployees(bool displayAll = false, bool isDelete = false);
    }
}
