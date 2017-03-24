using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class LabourClassification : BaseEntity
    {
        [Key]
        public long LabourClassificationID { get; set; }

        public string LabourClassificationName { get; set; }

        public bool IsInStd10 { get; set; }

        public virtual ICollection<EmployeeLabourClassification> EmployeeLabourClassifications { get; set; }

        public virtual ICollection<OrderTimeslip> OrderTimeslips { get; set; }

        public virtual ICollection<CustomerSiteLabourClassification> CustomerSiteLabourClassifications { get; set; }
    }
}
