using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class CustomerSiteLabourClassification : BaseEntity
    {
        [Key]
        public long CustomerSiteLabourClassificationID { get; set; }

        public long CustomerSiteID { get; set; }

        public long LabourClassificationID { get; set; }

        public bool IsExpire { get; set; }

        public decimal PayRate { get; set; }

        public decimal InvoiceRate { get; set; }

        public virtual CustomerSite CustomerSite { get; set; }

        public virtual LabourClassification LabourClassification { get; set; }
    }
}
