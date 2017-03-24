using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IRoleService
    {
        //Task<PagedData<Role>> Get(int PageIndex, int PageSize);
        Task<PagedData<Role>> Get(SearchDataTable search);
        Task<Role> GetByID(int RoleID);
        Task<string> Create(Role Role);
        Task<string> Update(Role Role);

        Task<List<Role>> GetAllRoles(bool displayAll = false, bool IsDelete = false);
    }
}
