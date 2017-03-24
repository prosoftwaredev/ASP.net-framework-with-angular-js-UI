using PayrollApp.Core.Data.Entities;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class OrderEquipmentService : IOrderEquipmentService, IDisposable
    {
        #region Variables

        private readonly IRepository<OrderEquipment> _orderEquipmentRepository;
        int response;

        #endregion

        #region _ctor

        public OrderEquipmentService(IRepository<OrderEquipment> orderEquipmentRepository)
        {
            _orderEquipmentRepository = orderEquipmentRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<OrderEquipment> GetByID(long OrderEquipmentID)
        {
            var query = await _orderEquipmentRepository.GetByIdAsync(OrderEquipmentID); ;
            return query;
        }

        public async Task<OrderEquipment> GetAllOrderEquipmentsByEquipmentIDAndOrderID(long OrderID, long EquipmentID, bool displayAll = false, bool isDelete = false)
        {
            OrderEquipment OrderEquipment = new OrderEquipment();

            var query = _orderEquipmentRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.OrderID == OrderID && x.EquipmentID == EquipmentID);

            if (displayAll)
                OrderEquipment = await query.Take(1).SingleOrDefaultAsync();
            else
                OrderEquipment = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return OrderEquipment;
        }

        public async Task<List<OrderEquipment>> GetAllOrderEquipmentsByOrderID(long OrderID, bool displayAll = false, bool isDelete = false)
        {
            List<OrderEquipment> OrderEquipmentList = new List<OrderEquipment>();

            var query = _orderEquipmentRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.OrderID == OrderID);

            if (displayAll)
                OrderEquipmentList = await query.ToListAsync();
            else
                OrderEquipmentList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return OrderEquipmentList;
        }

        public async Task<string> Create(OrderEquipment OrderEquipment)
        {
            response = await _orderEquipmentRepository.InsertAsync(OrderEquipment);
            if (response == 1)
                return OrderEquipment.OrderEquipmentID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(OrderEquipment OrderEquipment)
        {
            response = await _orderEquipmentRepository.UpdateAsync(OrderEquipment);
            if (response == 1)
                return OrderEquipment.OrderEquipmentID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
