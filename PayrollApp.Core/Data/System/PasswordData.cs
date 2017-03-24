using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.System
{
    public class PasswordData
    {
        public long UserID { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
