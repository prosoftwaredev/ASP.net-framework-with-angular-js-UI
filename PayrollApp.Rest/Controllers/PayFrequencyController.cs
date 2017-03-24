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
    public class PayFrequencyController : ApiController
    {
        private readonly IPayFrequencyService _payFrequencyService;
        string response;

        public PayFrequencyController() { }

        public PayFrequencyController(IPayFrequencyService payFrequencyService)
        {
            _payFrequencyService = payFrequencyService;
        }

        //[Authorize(PayFrequencys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPayFrequencys(FormDataCollection form)
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

            PagedData<PayFrequency> pagedData = await _payFrequencyService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.PayFrequencyID, x.PayFrequencyName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(PayFrequencys = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPayFrequencyByID(long PayFrequencyID)
        {
            if (PayFrequencyID <= 0)
                return NotFound();

            PayFrequency PayFrequency = await _payFrequencyService.GetByID(PayFrequencyID);

            if (PayFrequency != null)
            {
                var data = new { PayFrequency.PayFrequencyID, PayFrequency.PayFrequencyName, PayFrequency.Created, PayFrequency.IsEnable, PayFrequency.LastUpdated, PayFrequency.Remark, PayFrequency.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(PayFrequencys = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreatePayFrequency([FromBody]PayFrequency PayFrequency)
        {
            if (PayFrequency != null)
            {
                response = await _payFrequencyService.Create(PayFrequency);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(PayFrequencys = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePayFrequency([FromBody]PayFrequency PayFrequency)
        {
            if (PayFrequency != null)
            {
                PayFrequency newPayFrequency = await _payFrequencyService.GetByID(PayFrequency.PayFrequencyID);

                newPayFrequency.PayFrequencyName = PayFrequency.PayFrequencyName;
                newPayFrequency.IsEnable = PayFrequency.IsEnable;
                newPayFrequency.Remark = PayFrequency.Remark;
                newPayFrequency.LastUpdated = DateTime.Now;
                newPayFrequency.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _payFrequencyService.Update(newPayFrequency);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(PayFrequencys = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePayFrequency(long ID)
        {
            if (ID != 0)
            {
                PayFrequency newPayFrequency = await _payFrequencyService.GetByID(ID);

                newPayFrequency.IsDelete = true;
                newPayFrequency.LastUpdated = DateTime.Now;

                response = await _payFrequencyService.Update(newPayFrequency);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(PayFrequencys = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPayFrequencies(bool isDisplayAll)
        {
            List<PayFrequency> PayFrequencyList = await _payFrequencyService.GetAllPayFrequencies();

            if (PayFrequencyList != null)
            {
                PayFrequencyList = PayFrequencyList.OrderBy(x => x.PayFrequencyName).ToList();
                var data = PayFrequencyList.Select(x => new { x.PayFrequencyID, x.PayFrequencyName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
