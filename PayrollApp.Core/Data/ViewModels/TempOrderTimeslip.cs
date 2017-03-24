using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class TempOrderTimeslip : BaseEntity
    {

        public long OrderTimeslipID { get; set; }
        public long OrderID { get; set; }
        public DateTime? WeekStart { get; set; }
        public DateTime? WeekEnd { get; set; }
        public long? EmployeeID { get; set; }
        public long? LabourClassificationID { get; set; }
        public long? CustomerSiteJobLocationID { get; set; }
        public string Phone { get; set; }
        public string Reporting { get; set; }
        public string Comment { get; set; }
        public string DispatchNote { get; set; }
        public decimal PayRate { get; set; }
        public decimal InvoiceRate { get; set; }
        public bool RollOver { get; set; }
        public DateTime? RollOverStart { get; set; }
        public TimeSpan? RollOverTime { get; set; }
        public string Stat { get; set; }
        public string Roll { get; set; }
        public string Week { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsOneDay { get; set; }
        public string Note { get; set; }
        public int HrsReg { get; set; }
        public int HrsOT { get; set; }
        public int HrsTotal { get; set; }
        public int BillState { get; set; }

        public string XmlNote { get; set; }

        public decimal GrossPay { get; set; }

        public decimal WithHolding { get; set; }

        public decimal NetPay { get; set; }

        public decimal ItemsDue { get; set; }

        public decimal Advances { get; set; }

        public decimal CreditBalance { get; set; }

        public decimal BalanceFwd { get; set; }

        public decimal NetPay1 { get; set; }

        public decimal ReturnItems { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal PayNow { get; set; }

        public decimal ClosingBalance { get; set; }
        public DayStatusWithData DayStatusData { get; set; }
        public string JobLocation { get; set; }
        public string EmployeeName { get; set; }
        public string AccountNo { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }

        public long CustomerID { get; set; }
        public string CustomerName { get; set; }

        public long CustomerSiteID { get; set; }

        public string PONumber { get; set; }

        public string ContactName { get; set; }

        public int People { get; set; }

        public DateTime? WorkStartRsv { get; set; }

        public TimeSpan? StartTimeRsv { get; set; }

        public DateTime? WorkEndRsv { get; set; }

        public TimeSpan? EndTimeRsv { get; set; }

        public DateTime? WorkStartCust { get; set; }

        public TimeSpan? StartTimeCust { get; set; }

        public DateTime? WorkEndCust { get; set; }

        public TimeSpan? EndTimeCust { get; set; }

        public string OTPerDay { get; set; }

        public string OTPerWeek { get; set; }

        public int JobDuration { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
