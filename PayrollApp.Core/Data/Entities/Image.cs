using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.Entities
{
    public class Image : BaseEntity
    {
        [Key]
        public long ImageID { get; set; }

        [StringLength(255)]
        public string ImageName { get; set; }
    }
}
