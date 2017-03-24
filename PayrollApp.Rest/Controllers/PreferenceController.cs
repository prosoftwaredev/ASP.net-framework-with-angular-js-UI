using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PayrollApp.Rest.Controllers
{
    public class PreferenceController : ApiController
    {
        private readonly IPreferenceService _preferenceService;
        string response;

        public PreferenceController() { }

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetPreferences(FormDataCollection form)
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

            PagedData<Preference> pagedData = await _preferenceService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.PreferenceID, x.PreferenceName, x.PreferenceValue, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetPreferenceByID(long PreferenceID)
        {
            if (PreferenceID <= 0)
                return NotFound();

            Preference Preference = await _preferenceService.GetByID(PreferenceID);

            if (Preference != null)
            {
                var data = new { Preference.PreferenceID, Preference.PreferenceName, Preference.PreferenceValue, Preference.Created, Preference.IsEnable, Preference.LastUpdated, Preference.Remark, Preference.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> CreatePreference([FromBody]Preference Preference)
        {
            if (Preference != null)
            {
                response = await _preferenceService.Create(Preference);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> UpdatePreference([FromBody]Preference Preference)
        {
            if (Preference != null)
            {
                Preference newPreference = await _preferenceService.GetByID(Preference.PreferenceID);

                newPreference.PreferenceName = Preference.PreferenceName;
                newPreference.PreferenceValue = Preference.PreferenceValue;
                newPreference.IsEnable = Preference.IsEnable;
                newPreference.Remark = Preference.Remark;
                newPreference.LastUpdated = DateTime.Now;
                newPreference.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _preferenceService.Update(newPreference);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpDelete]
        public async Task<IHttpActionResult> DeletePreference(long ID)
        {
            if (ID != 0)
            {
                Preference newPreference = await _preferenceService.GetByID(ID);

                newPreference.IsDelete = true;
                newPreference.LastUpdated = DateTime.Now;

                response = await _preferenceService.Update(newPreference);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Preferences = "1")]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAllPreferences(bool isDisplayAll)
        {
            List<Preference> PreferenceList = await _preferenceService.GetAllPreferences();

            if (PreferenceList != null)
            {
                PreferenceList = PreferenceList.OrderBy(x => x.PreferenceName).ToList();
                var data = PreferenceList.Select(x => new { x.PreferenceID, x.PreferenceName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}