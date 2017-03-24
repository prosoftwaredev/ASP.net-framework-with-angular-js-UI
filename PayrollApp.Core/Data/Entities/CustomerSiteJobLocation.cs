
using System.Collections.Generic;
namespace PayrollApp.Core.Data.Entities
{
    public class CustomerSiteJobLocation : BaseEntity
    {
        public long CustomerSiteJobLocationID { get; set; }

        public long CustomerSiteID { get; set; }

        public string JobLocation { get; set; }

        public string JobAddress { get; set; }

        public string JobNote { get; set; }

        public virtual CustomerSite CustomerSite { get; set; }

        public virtual ICollection<OrderTimeslip> OrderTimeslips { get; set; }
    }
}
