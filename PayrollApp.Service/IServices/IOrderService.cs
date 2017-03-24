using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Threading.Tasks;
namespace PayrollApp.Service.IServices
{
    public interface IOrderService
    {
        Task<PagedData<Order>> Get(SearchOrder search);
        Task<Order> GetByID(long OrderID);
        Task<string> Create(Order Order);
        Task<string> Update(Order Order);

        Task<bool> GetUniquePONumber(string str);
    }
}
