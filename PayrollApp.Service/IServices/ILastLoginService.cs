using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ILastLoginService
    {
        Task<PagedData<LastLogin>> Get(SearchDataTable search);
        Task<LastLogin> GetByID(long LastLoginID);
        Task<string> Create(LastLogin LastLogin);
        Task<string> Update(LastLogin LastLogin);

        Task<LastLogin> GetLastByUserID(long UserID);
        Task<List<LastLogin>> GetAllLastLogins(bool displayAll = false , bool isDelete  = false);
    }
}
