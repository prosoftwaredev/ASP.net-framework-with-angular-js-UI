using PayrollApp.Core.Data.System;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsEnable = true;
            SortOrder = 1;
            Remark = "--";
            Created = DateTime.Now;
            IsDelete = false;
            CreatedBy = RoleHelper.GetCurrentUserID;
        }

        public int? SortOrder { get; set; }

        public bool? IsEnable { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastUpdated { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public bool? IsDelete { get; set; }

        public long? CreatedBy { get; set; }

        public long? LastUpdatedBy { get; set; }

        [NotMapped]
        public string CreatedByName { get; set; }

        [NotMapped]
        public string LastUpdatedByName { get; set; }
    }
}
