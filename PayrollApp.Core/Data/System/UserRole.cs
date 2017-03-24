using PayrollApp.Core.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.System
{
    public class UserRole : BaseEntity
    {
        [Key]
        public long UserRoleID { get; set; }   //PK

        public long UserID { get; set; } //FK  <---- User

        public int RoleID { get; set; }  //FK <----- Role

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
