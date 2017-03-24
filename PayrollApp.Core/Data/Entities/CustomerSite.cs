using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class CustomerSite : BaseEntity
    {
        [Key]
        public long CustomerSiteID { get; set; }

        public long CustomerID { get; set; }

        public string AccountNo { get; set; }

        public string SiteName { get; set; }

        public string SiteDescription { get; set; }

        public string PrContactName { get; set; }

        public string PrAddress1 { get; set; }

        public string PrAddress2 { get; set; }

        public long? PrCityID { get; set; }

        public string PrPostalCode { get; set; }

        public string PrMobile { get; set; }

        public string PrPhone { get; set; }

        public string PrFax { get; set; }

        public string PrEmail { get; set; }

        public string InContactName { get; set; }

        public string InAddress1 { get; set; }

        public string InAddress2 { get; set; }

        public long? InCityID { get; set; }

        public string InPostalCode { get; set; }

        public string InMobile { get; set; }

        public string InPhone { get; set; }

        public string InFax { get; set; }

        public string InEmail { get; set; }

        public bool InvoiceViaMail { get; set; }

        public bool InvoiceViaFax { get; set; }

        public bool InvoiceViaEmail { get; set; }

        public bool InvoiceAutomatically { get; set; }

        public bool InvoiceCombine { get; set; }

        public string CertificateNo { get; set; }

        public string OTPerDay { get; set; }

        public string OTPerWeek { get; set; }

        public string TimeslipMsg { get; set; }

        public string Reminder { get; set; }

        public bool IsPrimary { get; set; }

        [ForeignKey("PrCityID")]
        public virtual City PrCity { get; set; }

        [ForeignKey("InCityID")]
        public virtual City InCity { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<CustomerSiteNote> CustomerSiteNotes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<CustomerSiteLabourClassification> CustomerSiteLabourClassifications { get; set; }

        public virtual ICollection<CustomerSiteJobLocation> CustomerSiteJobLocations { get; set; }
    }
}
