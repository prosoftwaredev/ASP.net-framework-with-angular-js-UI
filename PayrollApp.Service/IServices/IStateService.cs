using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IStateService
    {
        //Task<PagedData<State>> Get(int PageIndex, int PageSize);
        Task<PagedData<State>> Get(SearchDataTable search);
        Task<State> GetByID(long StateID);
        Task<string> Create(State State);
        Task<string> Update(State State);

        Task<List<State>> GetAllStates(bool displayAll = false, bool isDelete = false);
        Task<List<State>> GetAllStatesByCountryID(long CountryID, bool displayAll = false, bool isDelete = false);
    }
}
