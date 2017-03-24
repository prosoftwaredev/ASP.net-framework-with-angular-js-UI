using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.System
{
    public class Role : BaseEntity
    {
        [Key]
        public int RoleID { get; set; }

        [StringLength(250)]
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
