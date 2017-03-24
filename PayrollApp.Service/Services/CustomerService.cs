using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.Helper;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class CustomerService : ICustomerService, IDisposable
    {
        #region Variables

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerSite> _customerSiteRepository;
        private readonly IRepository<EmployeeBlacklist> _employeeBlacklistRepository;
        int response;

        #endregion

        #region _ctor

        public CustomerService(IRepository<Customer> customerRepository, IRepository<EmployeeBlacklist> employeeBlacklistRepository, IRepository<CustomerSite> customerSiteRepository)
        {
            _customerRepository = customerRepository;
            _employeeBlacklistRepository = employeeBlacklistRepository;
            _customerSiteRepository = customerSiteRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Customer>> Get(SearchDataTable search)
        {
            PagedData<Customer> pageData = new PagedData<Customer>();

            var query = _customerRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long CustomerID = 0, tempCustomerID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempCustomerID))
                    CustomerID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CustomerID == CustomerID ||
                    x.CustomerName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.CompanyName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "CustomerID":
                            query = query.OrderBy(x => x.CustomerID);
                            break;

                        case "CustomerName":
                            query = query.OrderBy(x => x.CustomerName);
                            break;

                        case "CompanyName":
                            query = query.OrderBy(x => x.CompanyName);
                            break;

                        default:
                            query = query.OrderBy(x => x.CustomerID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "CustomerID":
                            query = query.OrderByDescending(x => x.CustomerID);
                            break;

                        case "CustomerName":
                            query = query.OrderByDescending(x => x.CustomerName);
                            break;

                        case "CompanyName":
                            query = query.OrderByDescending(x => x.CompanyName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.CustomerID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Customer> GetByID(long CustomerID)
        {
            var query = await _customerRepository.GetByIdAsync(CustomerID); ;
            return query;
        }

        public async Task<string> Create(Customer Customer)
        {
            response = await _customerRepository.InsertAsync(Customer);
            if (response == 1)
                return Customer.CustomerID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Customer Customer)
        {
            response = await _customerRepository.UpdateAsync(Customer);
            if (response == 1)
                return Customer.CustomerID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<PagedData<TempCustomer>> Get(SearchCustomerSite search)
        {
            PagedData<TempCustomer> pageData = new PagedData<TempCustomer>();

            var customer = _customerRepository.Table;

            customer = customer.Where(x => x.IsDelete == search.IsDelete);

            var customerSite = _customerSiteRepository.Table;

            customerSite = customerSite.Where(x => x.IsDelete == search.IsDelete);

            var customerSiteForCount = customerSite;

            customerSite = customerSite.Where(x => x.IsPrimary == true);


            var query = from c in customer
                        select new TempCustomer
                        {
                            CustomerID = c.CustomerID,
                            CustomerName = c.CustomerName,
                            CompanyName = c.CompanyName,
                            AccountNo = c.AccountNo,
                            PrContactName = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrContactName).FirstOrDefault(),
                            PrAddress1 = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrAddress1).FirstOrDefault(),
                            PrAddress2 = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrAddress2).FirstOrDefault(),
                            PrPostalCode = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrPostalCode).FirstOrDefault(),
                            PrPhone = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrPhone).FirstOrDefault(),
                            PrMobile = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrMobile).FirstOrDefault(),
                            PrFax = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrFax).FirstOrDefault(),
                            PrEmail = customerSite.Where(x => x.CustomerID == c.CustomerID && x.IsPrimary == true).Select(x => x.PrEmail).FirstOrDefault(),
                            SortOrder = c.SortOrder,
                            IsEnable = c.IsEnable,
                            Created = c.Created,
                            LastUpdated = c.LastUpdated,
                            Remark = c.Remark,
                            IsDelete = c.IsDelete,
                            Delinquent = c.Delinquent,
                            SitesCount = customerSiteForCount.Where(x => x.CustomerID == c.CustomerID).Count()
                        };

            if (!string.IsNullOrEmpty(search.GlobalSearch))
            {
                long CustomerID = 0, tempCustomerID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.GlobalSearch, out tempCustomerID))
                    CustomerID = Convert.ToInt64(search.GlobalSearch);
                else
                    if (DateTime.TryParse(search.GlobalSearch, out tempCreated))
                        Created = Convert.ToDateTime(search.GlobalSearch);
                    else
                        if (search.GlobalSearch.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.GlobalSearch.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CustomerID == CustomerID ||
                    x.AccountNo.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.CustomerName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.PrContactName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.PrEmail.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.PrMobile.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.PrPhone.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.PrFax.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year ||
                    x.IsEnable == isEnable);
            }

            query = query.GroupBy(x => x.CustomerID).Select(group => group.FirstOrDefault());

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.CustomerName).ThenBy(x => x.PrContactName).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<List<Customer>> GetAllCustomers(bool displayAll = false, bool isDelete = false)
        {
            List<Customer> CustomerList = new List<Customer>();

            var query = _customerRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CustomerList = await query.ToListAsync();
            else
                CustomerList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CustomerList;
        }

        public async Task<List<Customer>> GetAllCustomers(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeBlacklist> EmployeeBlacklistList = new List<EmployeeBlacklist>();

            var query1 = _employeeBlacklistRepository.Table;

            query1 = query1.Where(x => x.IsDelete == isDelete);

            query1 = query1.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeBlacklistList = await query1.ToListAsync();
            else
                EmployeeBlacklistList = await query1.Where(x => x.IsEnable == true).ToListAsync();

            List<long> customerIDList = EmployeeBlacklistList.Select(x => x.CustomerID).ToList();

            List<Customer> CustomerList = new List<Customer>();

            var query2 = _customerRepository.Table;

            //List<long> customerList = new List<long>();
            //if (displayAll)
            //var customerList = query2.Select(x => x.EmployeeBlacklists.Where(z => z.EmployeeID == EmployeeID && z.IsDelete == isDelete).Select(y => y.CustomerID).ToList()).ToList().ToList();



            query2 = query2.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CustomerList = await query2.ToListAsync();
            else
                CustomerList = await query2.Where(x => x.IsEnable == true).ToListAsync();

            var newList = CustomerList.Where(x => !customerIDList.Contains(x.CustomerID)).ToList();

            return newList;
        }

        public async Task<string> GetAccountNumber(string str)
        {
            string accountNo = string.Empty;

            str = str.ToUpper();

            var query = _customerRepository.Table;

            var accountNoList = await query.Select(x => x.AccountNo).Where(x => x != null).ToListAsync();

            if (accountNoList.Count > 0)
            {
                Lookup<string, string> lookup = (Lookup<string, string>)accountNoList.ToLookup(x => CommonHelper.FindAlphas(x), x => CommonHelper.FindNumber(x));

                var matchedList = lookup.Where(x => x.Key == str).ToList();

                if (matchedList.Count > 0)
                {
                    var matchedKey = matchedList.FirstOrDefault();

                    var matchedMax = matchedKey.Max();

                    accountNo = matchedKey.Key + matchedMax;
                }
                else
                    return accountNo;
            }

            return accountNo;
        }

        #endregion

    }
}
