using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class SearchDataTable
    {
        public SearchDataTable()
        {
            IsDelete = false;
        }

        public int Draw { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }
        public string SearchValue { get; set; }
        public int RecordsTotal { get; set; }
        public bool IsDelete { get; set; }

        public long SkillID { get; set; }
        public long CertificationID { get; set; }
    }
}
