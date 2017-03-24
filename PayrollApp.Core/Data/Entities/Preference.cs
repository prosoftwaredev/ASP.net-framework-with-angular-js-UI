using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Preference : BaseEntity
    {
        [Key]
        public long PreferenceID { get; set; }

        [StringLength(255)]
        public string PreferenceName { get; set; }

        [StringLength(255)]
        public string PreferenceValue { get; set; }   
        
            
    }
}
