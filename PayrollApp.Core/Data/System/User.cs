using PayrollApp.Core.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApp.Core.Data.System
{
    public class User : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long UserID { get; set; } //

        [StringLength(255)]
        public string Firstname { get; set; } //

        [StringLength(255)]
        public string Lastname { get; set; } //

        [StringLength(255)]
        public string Email { get; set; } //

        [StringLength(255)]
        public string Password { get; set; } //

        [StringLength(255)]
        public string Phone { get; set; }//

        [StringLength(255)]
        public string Gender { get; set; } //

        [StringLength(255)]
        public string Picture { get; set; }//

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [NotMapped]
        public string LastLogin { get; set; }

        [StringLength(100)]
        public string Hash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<LastLogin> LastLogins { get; set; }
    }
}
