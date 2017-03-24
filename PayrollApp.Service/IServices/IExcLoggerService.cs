using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IExcLoggerService
    {
        Task<PagedData<ExcLogger>> Get(SearchDataTable search);
        Task<ExcLogger> GetByID(long ExceptionLoggerID);
        Task<string> Create(ExcLogger ExceptionLogger);
        Task<string> Update(ExcLogger ExceptionLogger);
    }
}
