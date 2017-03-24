using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeLabourClassification : BaseEntity
    {
        public EmployeeLabourClassification() { IsExpire = false; }

        [Key]
        public long EmployeeLabourClassificationID { get; set; }

        public long EmployeeID { get; set; }

        public long LabourClassificationID { get; set; }

        public bool IsExpire { get; set; }

        public decimal Rate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LabourClassification LabourClassification { get; set; }
    }
}
