using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class OrderTimeslipService : IOrderTimeslipService
    {
        #region Variables

        private readonly IRepository<OrderTimeslip> _orderTimeslipRepository;
        int response;

        #endregion

        #region _ctor

        public OrderTimeslipService(IRepository<OrderTimeslip> orderTimeslipRepository)
        {
            _orderTimeslipRepository = orderTimeslipRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<OrderTimeslip>> Get(SearchOrder search)
        {
            PagedData<OrderTimeslip> pageData = new PagedData<OrderTimeslip>();

            var orderTimeslip = _orderTimeslipRepository.Table;

            orderTimeslip = orderTimeslip.Where(x => x.IsDelete == search.IsDelete);

            orderTimeslip = orderTimeslip.Where(x => x.EmployeeID != null && x.LabourClassificationID != null && x.CustomerSiteJobLocationID != null);



            var sow = search.SOW;
            var eow = search.EOW;
            var index = search.Index;

            var query = from ot in orderTimeslip
                        where
                          ot.Note.Substring(0, 6) == "<Stat>" ||
                          (ot.BillState == (-1) ||
                          ot.BillState > (-1)) &&
                          ot.Note.Substring(0, 6) == "<Stat>" &&
                          ot.WorkStartRsv >= sow &&
                          ot.WorkStartRsv <= eow &&
                          (ot.Note.Substring(0, 7 + index)).Substring((ot.Note.Substring(0, 7 + index)).Length - 1, 1) != "0" &&
                          (ot.Note.Substring(0, 7 + index)).Substring((ot.Note.Substring(0, 7 + index)).Length - 1, 1) != "5"
                        select ot;

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.OrderTimeslipID).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;


            //using (PayrollAppDbContext _context = new PayrollAppDbContext())
            //{
            //    string query = "SELECT * FROM OrderTimeslips INNER JOIN Orders ON OrderTimeslips.OrderID = Orders.OrderID INNER JOIN Customers ON  Orders.CustomerID = Customers.CustomerID INNER JOIN Employees ON OrderTimeslips.EmployeeID = Employees.EmployeeID  " +
            //         "WHERE   (LEFT(OrderTimeslips.Note, 6) = '<Stat>') OR" +
            //         " ((OrderTimeslips.BillState = - 1) OR ((OrderTimeslips.BillState > - 1))) AND" +
            //         " (LEFT(OrderTimeslips.Note, 6) = '<Stat>') AND" +
            //         " (OrderTimeslips.WorkStartRsv >= CONVERT(DATETIME, '" + search.SOW + "', 102)) AND" +
            //         " (OrderTimeslips.WorkStartRsv <= CONVERT(DATETIME, '" + search.EOW + "', 102)) AND" +
            //         " (RIGHT(LEFT(OrderTimeslips.Note, 7 + " + search.Index + "), 1) <> '" + 0 + "') AND" +
            //         " (RIGHT(LEFT(OrderTimeslips.Note, 7 + " + search.Index + "), 1) <> '" + 5 + "')";
            //    orderTimeslipList = await _context.Database.SqlQuery<OrderTimeslip>(query).ToListAsync();
            //}


        }

        public async Task<PagedData<OrderTimeslip>> GetAll(SearchOrder search)
        {
            PagedData<OrderTimeslip> pageData = new PagedData<OrderTimeslip>();

            var orderTimeslip = _orderTimeslipRepository.Table;

            orderTimeslip = orderTimeslip.Where(x => x.IsDelete == search.IsDelete);

            orderTimeslip = orderTimeslip.Where(x => x.EmployeeID != null && x.LabourClassificationID != null && x.CustomerSiteJobLocationID != null);

            pageData.Count = await orderTimeslip.CountAsync();

            orderTimeslip = orderTimeslip.OrderBy(x => x.OrderTimeslipID).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await orderTimeslip.ToListAsync();

            return pageData;
        }

        public async Task<PagedData<OrderTimeslip>> GetByBillState(SearchOrder search)
        {
            PagedData<OrderTimeslip> pageData = new PagedData<OrderTimeslip>();

            var orderTimeslip = _orderTimeslipRepository.Table;

            orderTimeslip = orderTimeslip.Where(x => x.IsDelete == search.IsDelete);

            if (search.BillState != -100)
                orderTimeslip = orderTimeslip.Where(x => x.BillState == search.BillState);

            orderTimeslip = orderTimeslip.Where(x => x.EmployeeID != null && x.LabourClassificationID != null && x.CustomerSiteJobLocationID != null);

            var query = orderTimeslip;

            if (!string.IsNullOrEmpty(search.GlobalSearch))
            {
                long OrderTimeslipID = 0, tempOrderTimeslipID = 0;
                bool? isEnable = null;
                DateTime? SrartDate = null; DateTime tempStartDate;
                int BillState = 0, tempBillState = 0;

                if (long.TryParse(search.GlobalSearch, out tempOrderTimeslipID))
                    OrderTimeslipID = Convert.ToInt64(search.GlobalSearch);
                else
                    if (int.TryParse(search.GlobalSearch, out tempBillState))
                        BillState = Convert.ToInt32(search.GlobalSearch);
                    else
                        if (DateTime.TryParse(search.GlobalSearch, out tempStartDate))
                            SrartDate = Convert.ToDateTime(search.GlobalSearch);
                        else
                            if (search.GlobalSearch.ToLower() == "yes")
                                isEnable = true;
                            else
                                if (search.GlobalSearch.ToLower() == "no")
                                    isEnable = false;

                query = query.Where(x => x.OrderTimeslipID == OrderTimeslipID ||
                    x.Order.Customer.CustomerName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Employee.FirstName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Employee.MiddleName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Employee.LastName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.StartTimeRsv.Value.ToString().Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Order.Reporting.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.CustomerSiteJobLocation.JobLocation.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.LabourClassification.LabourClassificationName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Order.Comment.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.BillState == BillState ||
                    x.EmployeeID == search.EmployeeID ||
                    x.Order.CustomerID == search.CustomerID ||
                    x.WorkStartRsv.Value.Day == SrartDate.Value.Day &&
                    x.WorkStartRsv.Value.Month == SrartDate.Value.Month &&
                    x.WorkStartRsv.Value.Year == SrartDate.Value.Year ||
                    x.IsEnable == isEnable);
            }

            if (search.CustomerID > 0)
                query = query.Where(x => x.Order.CustomerID == search.CustomerID);

            if (search.EmployeeID > 0)
                query = query.Where(x => x.EmployeeID == search.EmployeeID);

            query = query.Where(x => x.WorkStartRsv.Value.Day >= search.WorkStartRsv.Day && x.WorkStartRsv.Value.Month >= search.WorkStartRsv.Month && x.WorkStartRsv.Value.Year >= search.WorkStartRsv.Year);

            query = query.Where(x => x.WorkStartRsv.Value.Day <= search.WorkEndRsv.Day && x.WorkStartRsv.Value.Month <= search.WorkEndRsv.Month && x.WorkStartRsv.Value.Year <= search.WorkEndRsv.Year);



            //query = query.GroupBy(x => x.OrderTimeslipID).Select(group => group.FirstOrDefault());
            //query = query.GroupBy(x => x.q.e.OrderTimeslipID).Select(group => group.FirstOrDefault());

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.OrderTimeslipID).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<OrderTimeslip> GetByID(long OrderTimeslipID)
        {
            var query = await _orderTimeslipRepository.GetByIdAsync(OrderTimeslipID); ;
            return query;
        }

        public async Task<OrderTimeslip> GetAllOrderTimeslipsByEquipmentIDAndOrderID(long OrderID, long EquipmentID, bool displayAll = false, bool isDelete = false)
        {
            OrderTimeslip OrderTimeslip = new OrderTimeslip();

            var query = _orderTimeslipRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            //query = query.Where(x => x.OrderID == OrderID && x.EquipmentID == EquipmentID);

            if (displayAll)
                OrderTimeslip = await query.Take(1).SingleOrDefaultAsync();
            else
                OrderTimeslip = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return OrderTimeslip;
        }

        public async Task<List<OrderTimeslip>> GetAllOrderTimeslipsByOrderID(long OrderID, bool displayAll = false, bool isDelete = false)
        {
            List<OrderTimeslip> OrderTimeslipList = new List<OrderTimeslip>();

            var query = _orderTimeslipRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.OrderID == OrderID);

            if (displayAll)
                OrderTimeslipList = await query.ToListAsync();
            else
                OrderTimeslipList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return OrderTimeslipList;
        }

        public async Task<string> Create(OrderTimeslip OrderTimeslip)
        {
            response = await _orderTimeslipRepository.InsertAsync(OrderTimeslip);
            if (response == 1)
                return OrderTimeslip.OrderTimeslipID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(OrderTimeslip OrderTimeslip)
        {
            response = await _orderTimeslipRepository.UpdateAsync(OrderTimeslip);
            if (response == 1)
                return OrderTimeslip.OrderTimeslipID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<OrderTimeslip> GetByOrderID(long OrderID)
        {
            OrderTimeslip OrderTimeslip = new OrderTimeslip();

            var query = _orderTimeslipRepository.Table;

            query = query.Where(x => x.OrderID == OrderID);

            if (await query.AnyAsync())
                OrderTimeslip = await query.Take(1).SingleOrDefaultAsync();

            return OrderTimeslip;
        }

        #endregion
    }
}
