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
    public class SalesRepController : ApiController
    {
        private readonly ISalesRepService _salesRepService;
        string response;

        public SalesRepController() { }

        public SalesRepController(ISalesRepService salesRepService)
        {
            _salesRepService = salesRepService;
        }

        //[Authorize(SalesReps = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSalesReps(FormDataCollection form)
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

            PagedData<SalesRep> pagedData = await _salesRepService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.SalesRepID, x.SalesRepName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(SalesReps = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSalesRepByID(long SalesRepID)
        {
            if (SalesRepID <= 0)
                return NotFound();

            SalesRep SalesRep = await _salesRepService.GetByID(SalesRepID);

            if (SalesRep != null)
            {
                var data = new { SalesRep.SalesRepID, SalesRep.SalesRepName, SalesRep.Created, SalesRep.IsEnable, SalesRep.LastUpdated, SalesRep.Remark, SalesRep.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(SalesReps = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateSalesRep([FromBody]SalesRep SalesRep)
        {
            if (SalesRep != null)
            {
                response = await _salesRepService.Create(SalesRep);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(SalesReps = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateSalesRep([FromBody]SalesRep SalesRep)
        {
            if (SalesRep != null)
            {
                SalesRep newSalesRep = await _salesRepService.GetByID(SalesRep.SalesRepID);

                newSalesRep.SalesRepName = SalesRep.SalesRepName;
                newSalesRep.IsEnable = SalesRep.IsEnable;
                newSalesRep.Remark = SalesRep.Remark;
                newSalesRep.LastUpdated = DateTime.Now;
                newSalesRep.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _salesRepService.Update(newSalesRep);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(SalesReps = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteSalesRep(long ID)
        {
            if (ID != 0)
            {
                SalesRep newSalesRep = await _salesRepService.GetByID(ID);

                newSalesRep.IsDelete = true;
                newSalesRep.LastUpdated = DateTime.Now;

                response = await _salesRepService.Update(newSalesRep);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(SalesReps = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllSalesReps(bool isDisplayAll)
        {
            List<SalesRep> SalesRepList = await _salesRepService.GetAllSalesReps();

            if (SalesRepList != null)
            {
                SalesRepList = SalesRepList.OrderBy(x => x.SalesRepName).ToList();
                var data = SalesRepList.Select(x => new { x.SalesRepID, x.SalesRepName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
