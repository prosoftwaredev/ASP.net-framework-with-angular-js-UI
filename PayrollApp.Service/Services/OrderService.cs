using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        #region Variables

        private readonly IRepository<Order> _orderRepository;
        int response;

        #endregion

        #region _ctor

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<Order>> Get(SearchOrder search)
        {
            PagedData<Order> pageData = new PagedData<Order>();

            var order = _orderRepository.Table;

            order = order.Where(x => x.IsDelete == search.IsDelete);

            var query = order;

            if (!string.IsNullOrEmpty(search.GlobalSearch))
            {
                long OrderID = 0, tempOrderID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.GlobalSearch, out tempOrderID))
                    OrderID = Convert.ToInt64(search.GlobalSearch);
                else
                    if (DateTime.TryParse(search.GlobalSearch, out tempCreated))
                        Created = Convert.ToDateTime(search.GlobalSearch);
                    else
                        if (search.GlobalSearch.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.GlobalSearch.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.OrderID == OrderID ||
                    x.Customer.CustomerName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Customer.CompanyName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.ContactName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Phone.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.CustomerSite.PrContactName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year ||
                    x.IsEnable == isEnable);
            }

            query = query.GroupBy(x => x.OrderID).Select(group => group.FirstOrDefault());
            //query = query.GroupBy(x => x.q.e.OrderID).Select(group => group.FirstOrDefault());

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.Customer.CompanyName).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Order> GetByID(long OrderID)
        {
            var query = await _orderRepository.GetByIdAsync(OrderID); ;
            return query;
        }

        public async Task<string> Create(Order Order)
        {
            response = await _orderRepository.InsertAsync(Order);
            if (response == 1)
                return Order.OrderID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Order Order)
        {
            response = await _orderRepository.UpdateAsync(Order);
            if (response == 1)
                return Order.OrderID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<bool> GetUniquePONumber(string str)
        {
            var query = _orderRepository.Table;

            query = query.Where(x => x.PONumber.ToLower().Trim() == str.ToLower().Trim());

            query = query.OrderByDescending(x => x.PONumber);

            if (await query.AnyAsync())
                return true;

            return false;
        }

        #endregion
    }
}
