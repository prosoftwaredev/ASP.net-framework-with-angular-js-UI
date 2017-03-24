using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class SearchTimeslip
    {
        public SearchTimeslip() { IsDelete = false; }

        public string GlobalSearch { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool IsDelete { get; set; }
    }
}
