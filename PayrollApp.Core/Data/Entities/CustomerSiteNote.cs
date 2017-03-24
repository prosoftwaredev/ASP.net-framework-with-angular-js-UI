using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class CustomerSiteNote : BaseEntity
    {
        [Key]
        public long CustomerSiteNoteID { get; set; }

        public long CustomerSiteID { get; set; }

        public string Note { get; set; }

        public virtual CustomerSite CustomerSite { get; set; }

        [NotMapped]
        public long CustomerID { get; set; }
    }
}
