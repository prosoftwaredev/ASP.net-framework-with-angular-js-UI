using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class Customer : BaseEntity
    {
        [Key]
        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public string AccountNo { get; set; }

        public long? SalesRepID { get; set; }

        public long? PaymentTermID { get; set; }

        public bool RequirePO { get; set; }

        public bool UniquePO { get; set; }

        public bool PayByCC { get; set; }

        public bool Delinquent { get; set; }

        public bool InvoiceDiscountMessage { get; set; }

        public bool HideCustomerName { get; set; }

        public string XmlNote { get; set; }

        public virtual PaymentTerm PaymentTerm { get; set; }

        public virtual SalesRep SalesRep { get; set; }

        public virtual ICollection<EmployeeBlacklist> EmployeeBlacklists { get; set; }

        public virtual ICollection<CustomerSite> CustomerSites { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<OrderTimeslip> OrderTimeslips { get; set; }

        [NotMapped]
        public long[] EquipmentIDs { get; set; }
    }
}
