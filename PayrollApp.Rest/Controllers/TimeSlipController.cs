using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class TimeSlipController : ApiController
    {
        private readonly IOrderTimeslipService _orderTimeslipService;

        string response;

        public TimeSlipController() { }

        public TimeSlipController(IOrderTimeslipService orderTimeslipService)
        {
            _orderTimeslipService = orderTimeslipService;
        }


        [HttpPost]
        public async Task<IHttpActionResult> GetOrderTimeslips(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            int? BillState = Convert.ToInt32(form.Get("BillState"));

            string customerID = (Convert.ToString(form.Get("CustomerID")) == "") ? null : Convert.ToString(form.Get("CustomerID"));
            string employeeID = (Convert.ToString(form.Get("EmployeeID")) == "") ? null : Convert.ToString(form.Get("EmployeeID"));

            long CustomerID = customerID == null ? 0 : Convert.ToInt64(customerID);
            long EmployeeID = employeeID == null ? 0 : Convert.ToInt64(employeeID);

            string workStartRsv = (Convert.ToString(form.Get("WorkStartRsv")) == "") ? null : Convert.ToString(form.Get("WorkStartRsv"));
            string workEndRsv = (Convert.ToString(form.Get("WorkEndRsv")) == "") ? null : Convert.ToString(form.Get("WorkEndRsv"));

            DateTime WorkStartRsv = workStartRsv == null ? DateTime.Now : DateTime.ParseExact(workStartRsv, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime WorkEndRsv = workEndRsv == null ? DateTime.Now : DateTime.ParseExact(workEndRsv, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            SearchOrder search = new SearchOrder
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
                BillState = BillState,
                CustomerID = CustomerID,
                EmployeeID = EmployeeID,
                WorkStartRsv = WorkStartRsv,
                WorkEndRsv = WorkEndRsv
            };

            PagedData<OrderTimeslip> PagedData = await _orderTimeslipService.GetByBillState(search);


            if (PagedData != null)
            {
                var data = new
                {
                    Items = PagedData.Items.Select(x => new
                    {
                        OrderID = x.OrderID,
                        OrderTimeslipID = x.OrderTimeslipID, //
                        x.Order.Customer.CustomerName, //
                        x.CustomerSiteJobLocation.JobLocation, //
                        EmployeeName = x.Employee.FirstName + " " + x.Employee.MiddleName + " " + x.Employee.LastName, //
                        x.WorkStartRsv, //
                        x.StartTimeRsv, //
                        x.Order.Reporting, //
                        x.LabourClassification.LabourClassificationName, //
                        x.Order.Comment, //
                        x.BillState, //
                    }),
                    Count = PagedData.Count
                };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


    }
}
