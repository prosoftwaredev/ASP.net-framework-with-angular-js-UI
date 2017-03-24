using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class PayFrequency : BaseEntity
    {
        [Key]
        public long PayFrequencyID { get; set; }

        public string PayFrequencyName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
