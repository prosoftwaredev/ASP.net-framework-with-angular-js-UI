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
    public class CountryService : ICountryService, IDisposable
    {
        #region Variables

        private readonly IRepository<Country> _countryRepository;
        int response;

        #endregion

        #region _ctor

        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Country>> Get(SearchDataTable search)
        {
            PagedData<Country> pageData = new PagedData<Country>();

            var query = _countryRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long CountryID = 0, tempCountryID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempCountryID))
                    CountryID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CountryID == CountryID ||
                    x.CountryName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "CountryID":
                            query = query.OrderBy(x => x.CountryID);
                            break;

                        case "CountryName":
                            query = query.OrderBy(x => x.CountryName);
                            break;

                        default:
                            query = query.OrderBy(x => x.CountryID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "CountryID":
                            query = query.OrderByDescending(x => x.CountryID);
                            break;

                        case "CountryName":
                            query = query.OrderByDescending(x => x.CountryName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.CountryID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Country> GetByID(long CountryID)
        {
            var query = await _countryRepository.GetByIdAsync(CountryID); ;
            return query;
        }

        public async Task<string> Create(Country Country)
        {
            response = await _countryRepository.InsertAsync(Country);
            if (response == 1)
                return Country.CountryID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Country Country)
        {
            response = await _countryRepository.UpdateAsync(Country);
            if (response == 1)
                return Country.CountryID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Country>> GetAllCountries(bool displayAll = false, bool isDelete = false)
        {
            List<Country> CountryList = new List<Country>();

            var query = _countryRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CountryList = await query.ToListAsync();
            else
                CountryList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CountryList;
        }
        #endregion
    }
}
