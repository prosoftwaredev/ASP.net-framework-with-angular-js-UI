using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IOrderEquipmentService
    {
        Task<OrderEquipment> GetByID(long OrderEquipmentID);
        Task<string> Create(OrderEquipment OrderEquipment);
        Task<string> Update(OrderEquipment OrderEquipment);

        Task<OrderEquipment> GetAllOrderEquipmentsByEquipmentIDAndOrderID(long OrderID, long EquipmentID, bool displayAll = false, bool isDelete = false);
        Task<List<OrderEquipment>> GetAllOrderEquipmentsByOrderID(long OrderID, bool displayAll = false, bool isDelete = false);
    }
}
