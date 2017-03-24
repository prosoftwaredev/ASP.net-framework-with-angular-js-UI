using System;
using System.Collections.Generic;


namespace PayrollApp.Core.Data.Entities
{
    public class ReportRequest : BaseEntity
    {
        public int ReportRequestId { get; set; }

        public int ReportId { get; set; }

        public string ReportFileName { get; set; }

        public Guid UniqueId { get; set; }

        public virtual Report Report { get; set; }

        public virtual ICollection<ReportRequestParameter> ReportRequestParameters { get; set; }
    }
}
