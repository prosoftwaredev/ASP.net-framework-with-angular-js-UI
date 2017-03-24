using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollApp.Repository;
using PayrollApp.Core.Data.Entities;

namespace PayrollApp.Service.IServices
{
    public interface IReportService
    {
        IEnumerable<Report> GetReports();
        Report GetReport(int reportId);
        bool CreateRequest(ReportRequest reportRequest);

        ReportRequest GetRequest(Guid reportRequestId);

        bool Delete(ReportRequest request);

        bool Delete(ReportRequestParameter requestParameter);
    }

    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _reportRepository;
        private readonly IRepository<ReportRequest> _reportRequestRepository;
        private readonly IRepository<ReportRequestParameter> _reportRequestParameterRepository;

        public ReportService(
            IRepository<Report> reportRepository, 
            IRepository<ReportRequest> reportRequestRepository, 
            IRepository<ReportRequestParameter> reportRequestParameterRepository) 
        {
            _reportRepository = reportRepository;
            _reportRequestRepository = reportRequestRepository;
            _reportRequestParameterRepository = reportRequestParameterRepository;
        }

        public IEnumerable<Report> GetReports()
        {
            return _reportRepository.Entities.OrderBy(one => one.ReportName).AsEnumerable();
        }

        public Report GetReport(int reportId)
        {
            return _reportRepository.Entities.Include("ReportParameters").FirstOrDefault(one => one.ReportId == reportId);
        }

        public bool CreateRequest(ReportRequest reportRequest)
        {
            _reportRequestRepository.Insert(reportRequest);
            return true;
        }

        public ReportRequest GetRequest(Guid reportRequestId)
        {
            return _reportRequestRepository.Entities.Include("ReportRequestParameters")
                .FirstOrDefault(one => one.UniqueId == reportRequestId);
        }


        public bool Delete(ReportRequest request)
        {
            _reportRequestRepository.Delete(request);
            return true;
        }

        public bool Delete(ReportRequestParameter requestParameter)
        {
            _reportRequestParameterRepository.Delete(requestParameter);
            return true;
        }
    }
}
