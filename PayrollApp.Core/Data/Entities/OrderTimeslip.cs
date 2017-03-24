using PayrollApp.Core.Data.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class OrderTimeslip : BaseEntity
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public long OrderTimeslipID { get; set; }

        /// <summary>
        /// Foreign Key from Order
        /// </summary>
        public long OrderID { get; set; }

        /// <summary>
        /// Work start date for employee
        /// </summary>
        public DateTime? WorkStartRsv { get; set; }

        /// <summary>
        /// Work start time for employee
        /// </summary>
        public TimeSpan? StartTimeRsv { get; set; }

        /// <summary>
        /// Work end date for employee
        /// </summary>
        public DateTime? WorkEndRsv { get; set; }

        /// <summary>
        /// Work end time for employee
        /// </summary>
        public TimeSpan? EndTimeRsv { get; set; }

        /// <summary>
        /// Week start from WorkStart
        /// </summary>
        public DateTime? WeekStart { get; set; }

        /// <summary>
        /// Week end to saturday
        /// </summary>
        public DateTime? WeekEnd { get; set; }

        /// <summary>
        /// Employee assign for this timeslip
        /// </summary>
        public long? EmployeeID { get; set; }

        /// <summary>
        /// Labour classification assign for selected employee
        /// </summary>
        public long? LabourClassificationID { get; set; }

        /// <summary>
        /// Job location allocated for selected employee
        /// </summary>
        public long? CustomerSiteJobLocationID { get; set; }

        /// <summary>
        /// Phone number of customer to report to.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Reporting person name where employee report to.
        /// </summary>
        public string Reporting { get; set; }

        /// <summary>
        /// Any comment to employee to the customer
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Dispatched note that remind during dispatching of employee
        /// </summary>
        public string DispatchNote { get; set; }

        /// <summary>
        /// Pay rate that define to perticular employee
        /// </summary>
        public decimal PayRate { get; set; }

        /// <summary>
        /// Invoice rate that will apply on customer
        /// </summary>
        public decimal InvoiceRate { get; set; }

        /// <summary>
        /// Is the timeslip will be rollover.
        /// </summary>
        public bool RollOver { get; set; }

        /// <summary>
        /// Rollover start date
        /// </summary>
        public DateTime? RollOverStart { get; set; }

        /// <summary>
        /// Rollover start time
        /// </summary>
        public TimeSpan? RollOverTime { get; set; }

        /// <summary>
        /// Status of working days of employee
        /// </summary>
        public string Stat { get; set; }

        /// <summary>
        /// Status of Rollover day of employee
        /// </summary>
        public string Roll { get; set; }

        /// <summary>
        /// week start and week end of current working day
        /// </summary>
        public string Week { get; set; }

        /// <summary>
        /// week start and week end of current working day
        /// </summary>
        public string DayOfWeek { get; set; }

        /// <summary>
        /// Is the timeslip generated for daily or weekly.
        /// </summary>
        public bool IsOneDay { get; set; }

        /// <summary>
        /// It contains all the XML tag that are synch woth QB.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Registered working hours to employee.
        /// </summary>
        public int HrsReg { get; set; }

        /// <summary>
        /// Extra overtime performed by employee
        /// </summary>
        public int HrsOT { get; set; }

        /// <summary>
        /// Total hours worked performed by employee.
        /// </summary>
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

        public virtual Order Order { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LabourClassification LabourClassification { get; set; }

        public virtual CustomerSiteJobLocation CustomerSiteJobLocation { get; set; }

        [NotMapped]
        public DayStatusWithData DayStatusData { get; set; }

        [NotMapped]
        public string JobLocation { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }

        [NotMapped]
        public string AccountNo { get; set; }

        [NotMapped]
        public string CompanyName { get; set; }

        [NotMapped]
        public string Address { get; set; }
    }
}
