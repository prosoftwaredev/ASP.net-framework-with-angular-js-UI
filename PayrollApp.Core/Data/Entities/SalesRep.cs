using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class SalesRep : BaseEntity
    {
        [Key]
        public long SalesRepID { get; set; }

        public string SalesRepName { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
