using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICityService
    {
        //Task<PagedData<City>> Get(int PageIndex, int PageSize);
        Task<PagedData<City>> Get(SearchDataTable search);
        Task<City> GetByID(long CityID);
        Task<string> Create(City City);
        Task<string> Update(City City);

        Task<List<City>> GetAllCities(bool displayAll = false, bool isDelete = false);
        Task<List<City>> GetAllCitiesByStateID(long StateID, bool displayAll = false, bool isDelete = false);
    }
}
