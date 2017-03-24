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
    public class PaymentTermController : ApiController
    {
         private readonly IPaymentTermService _paymentTermService;
        string response;

        public PaymentTermController() { }

        public PaymentTermController(IPaymentTermService paymentTermService)
        {
            _paymentTermService = paymentTermService;
        }

        //[Authorize(PaymentTerms = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPaymentTerms(FormDataCollection form)
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

            PagedData<PaymentTerm> pagedData = await _paymentTermService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.PaymentTermID, x.PaymentTermName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(PaymentTerms = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPaymentTermByID(long PaymentTermID)
        {
            if (PaymentTermID <= 0)
                return NotFound();

            PaymentTerm PaymentTerm = await _paymentTermService.GetByID(PaymentTermID);

            if (PaymentTerm != null)
            {
                var data = new { PaymentTerm.PaymentTermID, PaymentTerm.PaymentTermName, PaymentTerm.Created, PaymentTerm.IsEnable, PaymentTerm.LastUpdated, PaymentTerm.Remark, PaymentTerm.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(PaymentTerms = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreatePaymentTerm([FromBody]PaymentTerm PaymentTerm)
        {
            if (PaymentTerm != null)
            {
                response = await _paymentTermService.Create(PaymentTerm);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(PaymentTerms = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePaymentTerm([FromBody]PaymentTerm PaymentTerm)
        {
            if (PaymentTerm != null)
            {
                PaymentTerm newPaymentTerm = await _paymentTermService.GetByID(PaymentTerm.PaymentTermID);

                newPaymentTerm.PaymentTermName = PaymentTerm.PaymentTermName;
                newPaymentTerm.IsEnable = PaymentTerm.IsEnable;
                newPaymentTerm.Remark = PaymentTerm.Remark;
                newPaymentTerm.LastUpdated = DateTime.Now;
                newPaymentTerm.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _paymentTermService.Update(newPaymentTerm);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(PaymentTerms = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePaymentTerm(long ID)
        {
            if (ID != 0)
            {
                PaymentTerm newPaymentTerm = await _paymentTermService.GetByID(ID);

                newPaymentTerm.IsDelete = true;
                newPaymentTerm.LastUpdated = DateTime.Now;

                response = await _paymentTermService.Update(newPaymentTerm);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(PaymentTerms = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPaymentTerms(bool isDisplayAll)
        {
            List<PaymentTerm> PaymentTermList = await _paymentTermService.GetAllPaymentTerms();

            if (PaymentTermList != null)
            {
                PaymentTermList = PaymentTermList.OrderBy(x => x.PaymentTermName).ToList();
                var data = PaymentTermList.Select(x => new { x.PaymentTermID, x.PaymentTermName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
