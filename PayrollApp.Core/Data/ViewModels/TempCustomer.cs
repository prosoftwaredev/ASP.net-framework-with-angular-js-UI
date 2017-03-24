using System;

namespace PayrollApp.Core.Data.ViewModels
{
    public class TempCustomer
    {
        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public bool Delinquent { get; set; }

        public string AccountNo { get; set; }

        public string PrContactName { get; set; }

        public string PrAddress1 { get; set; }

        public string PrAddress2 { get; set; }

        public long? PrCityID { get; set; }

        public string PrPostalCode { get; set; }

        public string PrMobile { get; set; }

        public string PrPhone { get; set; }

        public string PrFax { get; set; }

        public string PrEmail { get; set; }

        public int SitesCount { get; set; }

        public int? SortOrder { get; set; }
        public bool? IsEnable { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Remark { get; set; }
        public bool? IsDelete { get; set; }
        public long CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }
    }
}
