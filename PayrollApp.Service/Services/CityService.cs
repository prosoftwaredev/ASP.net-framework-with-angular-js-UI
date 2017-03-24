using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class CityService : ICityService, IDisposable
    {
        #region Variables

        private readonly IRepository<City> _cityRepository;
        int response;

        #endregion

        #region _ctor

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<City>> Get(SearchDataTable search)
        {
            PagedData<City> pageData = new PagedData<City>();

            var query = _cityRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long CityID = 0, tempCityID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempCityID))
                    CityID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CityID == CityID ||
                    x.CityName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.State.StateName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.State.Country.CountryName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "CityID":
                            query = query.OrderBy(x => x.CityID);
                            break;

                        case "CityName":
                            query = query.OrderBy(x => x.CityName);
                            break;

                        default:
                            query = query.OrderBy(x => x.CityID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "CityID":
                            query = query.OrderByDescending(x => x.CityID);
                            break;

                        case "CityName":
                            query = query.OrderByDescending(x => x.CityName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.CityID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<City> GetByID(long CityID)
        {
            var query = await _cityRepository.GetByIdAsync(CityID); ;
            return query;
        }

        public async Task<string> Create(City City)
        {
            response = await _cityRepository.InsertAsync(City);
            if (response == 1)
                return City.CityID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(City City)
        {
            response = await _cityRepository.UpdateAsync(City);
            if (response == 1)
                return City.CityID.ToString();
            else
                return response.ToString();
        }


        #endregion

        #region Extra

        public async Task<List<City>> GetAllCities(bool displayAll = false, bool isDelete = false)
        {
            List<City> CityList = new List<City>();

            var query = _cityRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CityList = await query.ToListAsync();
            else
                CityList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CityList;
        }

        public async Task<List<City>> GetAllCitiesByStateID(long StateID, bool displayAll = false, bool isDelete = false)
        {
            List<City> CityList = new List<City>();

            var query = _cityRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.StateID == StateID);

            if (displayAll)
                CityList = await query.ToListAsync();
            else
                CityList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CityList;
        }
        #endregion
    }
}
