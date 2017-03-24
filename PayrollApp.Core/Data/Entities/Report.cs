using System.Collections.Generic;

namespace PayrollApp.Core.Data.Entities
{
    public class Report : BaseEntity
    {
        public int ReportId { get; set; }

        public string ReportFileName { get; set; }

        public string ReportName { get; set; }

        public string ReportDescription { get; set; }

        public virtual ICollection<ReportParameter> ReportParameters { get; set; }

        public virtual ICollection<ReportRequest> ReportRequests { get; set; }
    }
}
