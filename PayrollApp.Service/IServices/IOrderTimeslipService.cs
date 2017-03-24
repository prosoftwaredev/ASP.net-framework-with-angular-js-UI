using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IOrderTimeslipService
    {
        Task<PagedData<OrderTimeslip>> Get(SearchOrder search);
        Task<PagedData<OrderTimeslip>> GetAll(SearchOrder search); //temp
        Task<PagedData<OrderTimeslip>> GetByBillState(SearchOrder search);
        Task<OrderTimeslip> GetByID(long OrderTimeslipID);
        Task<string> Create(OrderTimeslip OrderTimeslip);
        Task<string> Update(OrderTimeslip OrderTimeslip);

        Task<OrderTimeslip> GetAllOrderTimeslipsByEquipmentIDAndOrderID(long OrderID, long EquipmentID, bool displayAll = false, bool isDelete = false);
        Task<List<OrderTimeslip>> GetAllOrderTimeslipsByOrderID(long OrderID, bool displayAll = false, bool isDelete = false);
        Task<OrderTimeslip> GetByOrderID(long OrderID);
    }
}
