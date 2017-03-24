using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IUserService
    {
        Task<PagedData<TempUser>> Get(SearchDataTable search);
        Task<User> GetById(long userId);
        Task<string> Create(User User);
        Task<string> Update(User user);

        Task<string> GetPasswordByUserId(long userId);
        Task<User> GetUserByEmail(string email);
    }
}
