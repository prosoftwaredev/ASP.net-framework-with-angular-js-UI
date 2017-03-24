﻿using System.Collections.Generic;

namespace PayrollApp.Core.Data.Reports
{
    public class Report
    {
        public int ReportId { get; set; }

        public string ReportFileName { get; set; }

        public string ReportName { get; set; }

        public string ReportDescription { get; set; }

        public List<ReportParameter> ReportParameters { get; set; }

    }
}
