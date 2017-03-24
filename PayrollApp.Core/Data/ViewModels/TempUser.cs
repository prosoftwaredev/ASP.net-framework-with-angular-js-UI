using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
     public class TempUser
    {
         public long LastLoginID { get; set; } 
        public long UserID { get; set; } 
        public string Firstname { get; set; } 
        public string Lastname { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Phone { get; set; }
        public string Gender { get; set; } 
        public string Picture { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? LastLogin { get; set; }
        public string IPAddress { get; set; }
        public string Hash { get; set; }

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
