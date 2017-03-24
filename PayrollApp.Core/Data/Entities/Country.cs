using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Country : BaseEntity
    {
        [Key]
        public long CountryID { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [StringLength(255)]
        public string CountryName { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
