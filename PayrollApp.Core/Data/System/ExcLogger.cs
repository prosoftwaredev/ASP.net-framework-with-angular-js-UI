using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.System
{
    public class ExcLogger : BaseEntity
    {
        [Key]
        public long ExcLoggerID { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string HResult { get; set; }
        public string StackTrace { get; set; }        
        public string InnerException { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
