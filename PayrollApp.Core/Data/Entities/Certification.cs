using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Certification : BaseEntity
    {
        [Key]
        public long CertificationID { get; set; }

        [StringLength(255)]
        public string CertificationName { get; set; }

        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
    }
}
