using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICountryService
    {
        //Task<PagedData<Country>> Get(int PageIndex, int PageSize);
        Task<PagedData<Country>> Get(SearchDataTable search);
        Task<Country> GetByID(long CountryID);
        Task<string> Create(Country Country);
        Task<string> Update(Country Country);

        Task<List<Country>> GetAllCountries(bool displayAll = false, bool isDelete = false);
    }
}
