using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeType : BaseEntity
    {
        [Key]
        public long EmployeeTypeID { get; set; }

        [StringLength(50)]
        public string EmployeeTypeName { get; set; }

        public bool IsEmployee { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
