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
    public class TitleController : ApiController
    {
        private readonly ITitleService _titleService;
        string response;

        public TitleController() { }

        public TitleController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTitles(FormDataCollection form)
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

            PagedData<Title> pagedData = await _titleService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.TitleID, x.TitleName, x.Gender, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(Titles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTitleByID(long TitleID)
        {
            if (TitleID <= 0)
                return NotFound();

            Title Title = await _titleService.GetByID(TitleID);

            if (Title != null)
            {
                var data = new { Title.TitleID, Title.TitleName, Title.Gender, Title.Created, Title.IsEnable, Title.LastUpdated, Title.Remark, Title.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Titles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateTitle([FromBody]Title Title)
        {
            if (Title != null)
            {
                response = await _titleService.Create(Title);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Titles = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTitle([FromBody]Title Title)
        {
            if (Title != null)
            {
                Title newTitle = await _titleService.GetByID(Title.TitleID);

                newTitle.TitleName = Title.TitleName;
                newTitle.Gender = Title.Gender;
                newTitle.IsEnable = Title.IsEnable;
                newTitle.Remark = Title.Remark;
                newTitle.LastUpdated = DateTime.Now;
                newTitle.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _titleService.Update(newTitle);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Titles = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTitle(long ID)
        {
            if (ID != 0)
            {
                Title newTitle = await _titleService.GetByID(ID);

                newTitle.IsDelete = true;
                newTitle.LastUpdated = DateTime.Now;

                response = await _titleService.Update(newTitle);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Titles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTitles(bool isDisplayAll)
        {
            List<Title> TitleList = await _titleService.GetAllTitles();

            if (TitleList != null)
            {
                TitleList = TitleList.OrderBy(x => x.TitleName).ToList();
                var data = TitleList.Select(x => new { x.TitleID, x.TitleName, x.Gender });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
