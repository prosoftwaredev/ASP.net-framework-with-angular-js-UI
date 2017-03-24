using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class State : BaseEntity
    {
        [Key]
        public long StateID { get; set; }

        [StringLength(2)]
        public string StateCode { get; set; }

        [StringLength(255)]
        public string StateName { get; set; }

        public long CountryID { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
