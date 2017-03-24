using PayrollApp.Service.IServices;
using PayrollApp.Core.Data.Core;
using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using AutoMapper;

namespace PayrollApp.Rest.Controllers
{
    public class ReportController : RelayController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public ApiResult<IEnumerable<Report>> Reports()
        {
            return Execute(() => _reportService.GetReports());//.Select(Mapper.Map<Report>));
        }

        [HttpGet]
        public ApiResult<ReportRequest> Report(int ReportId)
        {
            return Execute(() =>
            {
                var request = Mapper.Map<ReportRequest>(_reportService.GetReport(ReportId));
                foreach (var one in request.ReportRequestParameters)
                {
                    if (one.ParameterViewName == "activeFlagDropDown")
                    {
                        one.ParameterValue = "1";
                    }
                } 

                return request;
            });
        }


        [HttpPost]
        public ApiResult<string> CreateRequest(ReportRequest ReportRequest)
        {
            return Execute(() =>
            {
                var result = Guid.NewGuid();
                ReportRequest.UniqueId = result;
                _reportService.CreateRequest(ReportRequest);//Mapper.Map<ReportRequest>(ReportRequest));
                return result.ToString();
            });
        }
    }
}
