using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        public long OrderID { get; set; }

        public long CustomerID { get; set; }

        public long CustomerSiteID { get; set; }

        public long LabourClassificationID { get; set; }

        public long CustomerSiteJobLocationID { get; set; }

        public string PONumber { get; set; }

        public string ContactName { get; set; }

        public string Phone { get; set; }

        public int People { get; set; }

        public DateTime? WorkStartRsv { get; set; }

        public TimeSpan? StartTimeRsv { get; set; }

        public DateTime? WorkEndRsv { get; set; }

        public TimeSpan? EndTimeRsv { get; set; }

        public DateTime? WorkStartCust { get; set; }

        public TimeSpan? StartTimeCust { get; set; }

        public DateTime? WorkEndCust { get; set; }

        public TimeSpan? EndTimeCust { get; set; }

        public string Reporting { get; set; }

        public string Comment { get; set; }

        public string DispatchNote { get; set; }

        public string OTPerDay { get; set; }

        public string OTPerWeek { get; set; }

        public int JobDuration { get; set; }

        public string XmlNote { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual CustomerSite CustomerSite { get; set; }

        public virtual ICollection<OrderEquipment> OrderEquipments { get; set; }

        public virtual ICollection<OrderTimeslip> OrderTimeslips { get; set; }

        [NotMapped]
        public long[] EquipmentIDs { get; set; }

        [NotMapped]
        public string JobLocation { get; set; }

        [NotMapped]
        public string JobAddress { get; set; }

        [NotMapped]
        public string JobNote { get; set; }
    }
}
