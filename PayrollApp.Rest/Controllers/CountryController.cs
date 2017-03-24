using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class CountryController : ApiController
    {
        private readonly ICountryService _countryService;
        string response;

        public CountryController() { }

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //[Authorize(Countrys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCountrys(FormDataCollection form)
        {
            var draw = form.GetValues("draw").FirstOrDefault();
            var start = form.GetValues("start").FirstOrDefault();
            var length = form.GetValues("length").FirstOrDefault();
            var sortColumn = form.GetValues("columns[" + form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            SearchDataTable search = new SearchDataTable
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchValue = searchValue,
                RecordsTotal = recordsTotal
            };

            PagedData<Country> pagedData = await _countryService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.CountryID, x.CountryName, x.CountryCode, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Countrys = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCountryByID(long CountryID)
        {
            if (CountryID <= 0)
                return NotFound();

            Country Country = await _countryService.GetByID(CountryID);

            if (Country != null)
            {
                var data = new { Country.CountryID, Country.CountryName, Country.CountryCode, Country.Created, Country.IsEnable, Country.LastUpdated, Country.Remark, Country.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Countrys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCountry([FromBody]Country Country)
        {
            if (Country != null)
            {
                response = await _countryService.Create(Country);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Countrys = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCountry([FromBody]Country Country)
        {
            if (Country != null)
            {
                Country newCountry = await _countryService.GetByID(Country.CountryID);

                newCountry.CountryName = Country.CountryName;
                newCountry.CountryCode = Country.CountryCode;
                newCountry.IsEnable = Country.IsEnable;
                newCountry.Remark = Country.Remark;
                newCountry.LastUpdated = DateTime.Now;
                newCountry.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _countryService.Update(newCountry);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Countrys = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCountry(long ID)
        {
            if (ID != 0)
            {
                Country newCountry = await _countryService.GetByID(ID);

                newCountry.IsDelete = true;
                newCountry.LastUpdated = DateTime.Now;

                response = await _countryService.Update(newCountry);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Countrys = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCountries(bool isDisplayAll)
        {
            List<Country> CountryList = await _countryService.GetAllCountries();

            if (CountryList != null)
            {
                CountryList = CountryList.OrderBy(x => x.CountryName).ToList();
                var data = CountryList.Select(x => new { x.CountryID, x.CountryName, x.CountryCode });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
