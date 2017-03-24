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
    public class CustomerSiteService : ICustomerSiteService, IDisposable
    {
        #region Variables

        private readonly IRepository<CustomerSite> _customerSiteRepository;
        int response;

        #endregion

        #region _ctor

        public CustomerSiteService(IRepository<CustomerSite> customerSiteRepository)
        {
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

        public async Task<PagedData<CustomerSite>> Get(SearchCustomerSite search)
        {
            PagedData<CustomerSite> pageData = new PagedData<CustomerSite>();

            var CustomerSite = _customerSiteRepository.Table;

            CustomerSite = CustomerSite.Where(x => x.IsDelete == search.IsDelete);

            var query = CustomerSite;

            if (!string.IsNullOrEmpty(search.GlobalSearch))
            {
                long CustomerSiteID = 0, tempCustomerSiteID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.GlobalSearch, out tempCustomerSiteID))
                    CustomerSiteID = Convert.ToInt64(search.GlobalSearch);
                else
                    if (DateTime.TryParse(search.GlobalSearch, out tempCreated))
                        Created = Convert.ToDateTime(search.GlobalSearch);
                    else
                        if (search.GlobalSearch.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.GlobalSearch.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CustomerSiteID == CustomerSiteID ||
                    x.AccountNo.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Customer.CustomerName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
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

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.Customer.CustomerName).ThenBy(x => x.PrContactName).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;

        }

        public async Task<CustomerSite> GetByID(long CustomerSiteID)
        {
            var query = await _customerSiteRepository.GetByIdAsync(CustomerSiteID); ;
            return query;
        }

        //public async Task<CustomerSite> GetAllCustomerSitesByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false)
        //{
        //    CustomerSite CustomerSite = new CustomerSite();

        //    var query = _customerSiteRepository.Table;

        //    query = query.Where(x => x.IsDelete == isDelete);

        //    query = query.Where(x => x.CustomerID == CustomerID);

        //    if (displayAll)
        //        CustomerSite = await query.Take(1).SingleOrDefaultAsync();
        //    else
        //        CustomerSite = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

        //    return CustomerSite;
        //}

        public async Task<List<CustomerSite>> GetAllCustomerSitesByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false)
        {
            List<CustomerSite> CustomerSiteList = new List<CustomerSite>();

            var query = _customerSiteRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerID == CustomerID);

            if (displayAll)
                CustomerSiteList = await query.ToListAsync();
            else
                CustomerSiteList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CustomerSiteList;
        }

        public async Task<string> Create(CustomerSite CustomerSite)
        {
            response = await _customerSiteRepository.InsertAsync(CustomerSite);
            if (response == 1)
                return CustomerSite.CustomerSiteID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(CustomerSite CustomerSite)
        {
            response = await _customerSiteRepository.UpdateAsync(CustomerSite);
            if (response == 1)
                return CustomerSite.CustomerSiteID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<string> GetAccountNumber(string str)
        {
            string accountNo = string.Empty;

            str = str.ToUpper();

            var query = _customerSiteRepository.Table;

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

        public async Task<bool> GetIsPrimaryCustomerSite(long CustomerID, bool isDelete = false)
        {
            bool isPrimary = false;
            var query = _customerSiteRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerID == CustomerID && x.IsPrimary == true);

            if (await query.AnyAsync())
                isPrimary = true;

            return isPrimary;
        }

        public async Task<long> GetIsPrimaryCustomerSite(long CustomerID)
        {
            long CustomerSiteID = 0;
            var query = _customerSiteRepository.Table;

            query = query.Where(x => x.CustomerID == CustomerID && x.IsPrimary == true);

            if (await query.AnyAsync())
                CustomerSiteID = await query.Select(x => x.CustomerSiteID).Take(1).SingleOrDefaultAsync();

            return CustomerSiteID;
        }

        #endregion
    }
}
