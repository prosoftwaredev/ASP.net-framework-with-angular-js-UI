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
    public class LabourClassificationController : ApiController
    {
        private readonly ILabourClassificationService _labourClassificationService;
        string response;

        public LabourClassificationController() { }

        public LabourClassificationController(ILabourClassificationService labourClassificationService)
        {
            _labourClassificationService = labourClassificationService;
        }

        //[Authorize(LabourClassifications = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetLabourClassifications(FormDataCollection form)
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

            PagedData<LabourClassification> pagedData = await _labourClassificationService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { LabourClassificationID = x.LabourClassificationID, LabourClassificationName = x.LabourClassificationName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(LabourClassifications = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLabourClassificationByID(long LabourClassificationID)
        {
            if (LabourClassificationID <= 0)
                return NotFound();

            LabourClassification LabourClassification = await _labourClassificationService.GetByID(LabourClassificationID);

            if (LabourClassification != null)
            {
                var data = new { LabourClassificationID = LabourClassification.LabourClassificationID, LabourClassificationName = LabourClassification.LabourClassificationName, LabourClassification.IsInStd10, LabourClassification.Created, LabourClassification.IsEnable, LabourClassification.LastUpdated, LabourClassification.Remark, LabourClassification.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(LabourClassifications = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateLabourClassification([FromBody]LabourClassification LabourClassification)
        {
            if (LabourClassification != null)
            {
                response = await _labourClassificationService.Create(LabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(LabourClassifications = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateLabourClassification([FromBody]LabourClassification LabourClassification)
        {
            if (LabourClassification != null)
            {
                LabourClassification newLabourClassification = await _labourClassificationService.GetByID(LabourClassification.LabourClassificationID);

                newLabourClassification.LabourClassificationName = LabourClassification.LabourClassificationName;
                newLabourClassification.IsInStd10 = LabourClassification.IsInStd10;
                newLabourClassification.IsEnable = LabourClassification.IsEnable;
                newLabourClassification.Remark = LabourClassification.Remark;
                newLabourClassification.LastUpdated = DateTime.Now;
                newLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _labourClassificationService.Update(newLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(LabourClassifications = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLabourClassification(long ID)
        {
            if (ID != 0)
            {
                LabourClassification newLabourClassification = await _labourClassificationService.GetByID(ID);

                newLabourClassification.IsDelete = true;
                newLabourClassification.LastUpdated = DateTime.Now;

                response = await _labourClassificationService.Update(newLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(LabourClassifications = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllLabourClassifications(bool isDisplayAll)
        {
            List<LabourClassification> LabourClassificationList = await _labourClassificationService.GetAllLabourClassifications();

            if (LabourClassificationList != null)
            {
                LabourClassificationList = LabourClassificationList.OrderBy(x => x.LabourClassificationName).ToList();
                var data = LabourClassificationList.Select(x => new { x.LabourClassificationID, x.LabourClassificationName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
