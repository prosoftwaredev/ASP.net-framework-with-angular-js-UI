
using System;
namespace PayrollApp.Core.Data.ViewModels
{
    public class SearchOrder
    {
        public SearchOrder()
        {
            IsDelete = false;
        }

        public string GlobalSearch { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool IsDelete { get; set; }

        public int? BillState { get; set; }

        public long CustomerID { get; set; }

        public long EmployeeID { get; set; }

        public DateTime WorkStartRsv { get; set; }

        public DateTime WorkEndRsv { get; set; }

        public DateTime DispatchDate { get; set; }

        public int Index { get; set; }

        public DateTime SOW { get; set; }

        public DateTime EOW { get; set; }
    }
}
