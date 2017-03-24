using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Title : BaseEntity
    {
        [Key]
        public long TitleID { get; set; }

        [StringLength(250)]
        public string TitleName { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }       
    }
}
