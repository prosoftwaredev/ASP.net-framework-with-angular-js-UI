using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IPayFrequencyService
    {
        Task<PagedData<PayFrequency>> Get(SearchDataTable search);
        Task<PayFrequency> GetByID(long PayFrequencyID);
        Task<string> Create(PayFrequency PayFrequency);
        Task<string> Update(PayFrequency PayFrequency);

        Task<List<PayFrequency>> GetAllPayFrequencies(bool displayAll = false, bool isDelete = false);
    }
}
