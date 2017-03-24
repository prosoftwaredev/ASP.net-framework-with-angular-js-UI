using PayrollApp.Core.Data.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IUserRoleService
    {
        Task<PagedData<UserRole>> Get(int PageIndex, int PageSize);
        Task<UserRole> GetByID(long UserRoleID);
        Task<string> Create(UserRole UserRole);
        Task<string> Update(UserRole UserRole);

        Task<UserRole> GetAllUserRolesByRoleIDAndUserID(long UserID, long RoleID, bool displayAll = false, bool isDelete = false);
        Task<List<UserRole>> GetAllUserRolesByUserID(long UserID, bool displayAll = false, bool isDelete = false);
    }
}
