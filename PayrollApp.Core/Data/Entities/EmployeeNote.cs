using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeNote : BaseEntity
    {
        [Key]
        public long EmployeeNoteID { get; set; }

        public long EmployeeID { get; set; }

        public string Note { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
