using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeCertification : BaseEntity
    {
        [Key]
        public long EmployeeCertificationID { get; set; }

        public long EmployeeID { get; set; }

        public long CertificationID { get; set; }

        [StringLength(255)]
        public string Picture { get; set; }//

        public virtual Employee Employee { get; set; }

        public virtual Certification Certification { get; set; }
    }
}
