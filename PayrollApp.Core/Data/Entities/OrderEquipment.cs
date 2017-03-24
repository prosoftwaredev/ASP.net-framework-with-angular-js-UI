using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class OrderEquipment : BaseEntity
    {
        [Key]
        public long OrderEquipmentID { get; set; }

        public long OrderID { get; set; }

        public long EquipmentID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
