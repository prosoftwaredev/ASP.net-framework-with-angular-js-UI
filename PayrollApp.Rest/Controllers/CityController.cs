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
    public class CityController : ApiController
    {
        private readonly ICityService _cityService;
        string response;

        public CityController() { }

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        //[Authorize(Citys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCitys(FormDataCollection form)
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

            PagedData<City> pagedData = await _cityService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.CityID, x.State.StateName, x.State.Country.CountryName, x.CityName, x.Created, x.IsEnable }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(Citys = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCityByID(int CityID)
        {
            if (CityID <= 0)
                return NotFound();

            City City = await _cityService.GetByID(CityID);

            if (City != null)
            {
                var data = new { City.CityID, City.StateID, City.State.CountryID, City.CityName, City.Created, City.IsEnable, City.LastUpdated, City.Remark, City.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Citys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCity([FromBody]City City)
        {
            if (City != null)
            {
                response = await _cityService.Create(City);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Citys = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCity([FromBody]City City)
        {
            if (City != null)
            {
                City newCity = await _cityService.GetByID(City.CityID);

                newCity.StateID = City.StateID;
                newCity.CityName = City.CityName;
                newCity.IsEnable = City.IsEnable;
                newCity.Remark = City.Remark;
                newCity.LastUpdated = DateTime.Now;
                newCity.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _cityService.Update(newCity);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Citys = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCity(int ID)
        {
            if (ID != 0)
            {
                City newCity = await _cityService.GetByID(ID);

                newCity.IsDelete = true;
                newCity.LastUpdated = DateTime.Now;

                response = await _cityService.Update(newCity);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCities(bool isDisplayAll)
        {
            List<City> CityList = await _cityService.GetAllCities();

            if (CityList != null)
            {
                CityList = CityList.OrderBy(x => x.CityName).ToList();
                var data = CityList.Select(x => new { x.CityID, x.CityName, x.StateID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCitiesByStateID(bool isDisplayAll, long StateID)
        {
            List<City> CityList = await _cityService.GetAllCitiesByStateID(StateID);

            if (CityList != null)
            {
                CityList = CityList.OrderBy(x => x.CityName).ToList();
                var data = CityList.Select(x => new { x.CityID, x.CityName, x.StateID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
