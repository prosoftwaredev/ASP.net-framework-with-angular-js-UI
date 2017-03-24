using PayrollApp.Core.Data.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IAccountService
    {
        Task<string> RegisterUser(User user);

        Task<long> CreateUserRole(UserRole userRole);

        Task<User> GetUserById(long userId);

        Task<User> GetUserByEmail(string email);

        Task<User> GetEnabledUserByEmail(string email);

        Task<User> GetEnabledAndVerifyUserByEmailAndPassword(string email, string pasword);

        Task<string> GetPasswordByEmail(string email);

        Task<List<UserRole>> GetUserRolesByUserId(long userId, bool displayAll = false, bool isDelete = false);

        List<UserRole> GetUserRolesById(long userId);
    }
}
