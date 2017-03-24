using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeBlacklist : BaseEntity
    {
        [Key]
        public long EmployeeBlacklistID { get; set; }

        public long EmployeeID { get; set; }

        public long CustomerID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
