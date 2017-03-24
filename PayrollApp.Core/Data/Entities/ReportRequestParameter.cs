
namespace PayrollApp.Core.Data.Entities
{
    public class ReportRequestParameter : BaseEntity
    {

        public int ReportRequestParameterId { get; set; }

        public int ReportRequestId { get; set; }

        public string ParameterValue { get; set; }

        public string ReportParameterName { get; set; }

        public string ParameterViewName { get; set; }

        public virtual ReportRequest ReportRequest { get; set; }
    }
}
