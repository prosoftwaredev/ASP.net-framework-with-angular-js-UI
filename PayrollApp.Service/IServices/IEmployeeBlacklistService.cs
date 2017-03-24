using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeBlacklistService
    {
        Task<EmployeeBlacklist> GetByID(long EmployeeBlacklistID);
        Task<string> Create(EmployeeBlacklist EmployeeBlacklist);
        Task<string> Update(EmployeeBlacklist EmployeeBlacklist);

        Task<EmployeeBlacklist> GetAllEmployeeBlacklistsByCustomerIDAndEmployeeID(long EmployeeID, long CustomerID, bool displayAll = false, bool isDelete = false);
        Task<List<EmployeeBlacklist>> GetAllEmployeeBlacklistsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false);
        Task<List<EmployeeBlacklist>> GetAllEmployeeBlacklistsByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false);
    }
}
