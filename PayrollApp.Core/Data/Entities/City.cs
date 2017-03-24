using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class City : BaseEntity
    {
        [Key]
        public long CityID { get; set; }

        [StringLength(255)]
        public string CityName { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [StringLength(2)]
        public string StateCode { get; set; }

        [StringLength(255)]
        public string TimeZone { get; set; }

        public long StateID { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        [InverseProperty("PrCity")]
        public virtual ICollection<CustomerSite> PrCustomerSite { get; set; }

         [InverseProperty("InCity")]
        public virtual ICollection<CustomerSite> InCustomerSite { get; set; }
    }
}
