using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Equipment : BaseEntity
    {
        [Key]
        public long EquipmentID { get; set; }

        [StringLength(255)]
        public string EquipmentName { get; set; }

        public decimal Rate { get; set; }

        public virtual ICollection<OrderEquipment> OrderEquipments { get; set; }
    }
}
