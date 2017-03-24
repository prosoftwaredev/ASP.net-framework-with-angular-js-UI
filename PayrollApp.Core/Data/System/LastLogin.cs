using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.System
{
    public class LastLogin : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long LastLoginID { get; set; } 

        public long UserID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        public string IPAddress { get; set; }

        public virtual User User { get; set; }
    }
}
