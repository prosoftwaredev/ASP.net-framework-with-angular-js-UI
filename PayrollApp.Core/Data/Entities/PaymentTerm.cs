using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class PaymentTerm : BaseEntity
    {
        [Key]
        public long PaymentTermID { get; set; }

        public string PaymentTermName { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
