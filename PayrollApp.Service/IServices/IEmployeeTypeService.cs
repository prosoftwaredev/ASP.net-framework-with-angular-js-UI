using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeTypeService
    {
        Task<PagedData<EmployeeType>> Get(SearchDataTable search);
        Task<EmployeeType> GetByID(long EmployeeTypeID);
        Task<string> Create(EmployeeType EmployeeType);
        Task<string> Update(EmployeeType EmployeeType);

        Task<List<EmployeeType>> GetAllEmployeeTypes(bool displayAll = false, bool isDelete = false);
    }
}
