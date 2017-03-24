using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Skill : BaseEntity
    {
        [Key]
        public long SkillID { get; set; }

        [StringLength(255)]
        public string SkillName { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
