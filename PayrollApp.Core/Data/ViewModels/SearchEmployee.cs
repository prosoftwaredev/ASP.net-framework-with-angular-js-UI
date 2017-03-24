using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class SearchEmployee
    {
        public SearchEmployee() { IsDelete = false; }

        public string GlobalSearch { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long SkillID { get; set; }

        public long CertificationID { get; set; }

        public bool IsDelete { get; set; }
    }
}
