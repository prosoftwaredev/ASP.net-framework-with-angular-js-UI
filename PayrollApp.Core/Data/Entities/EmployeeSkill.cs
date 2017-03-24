using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class EmployeeSkill : BaseEntity
    {
        [Key]
        public long EmployeeSkillID { get; set; }

        public long EmployeeID { get; set; }

        public long SkillID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Skill Skill { get; set; }
    }
}
