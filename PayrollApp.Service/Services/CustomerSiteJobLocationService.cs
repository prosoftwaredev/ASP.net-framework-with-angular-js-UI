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
    public class CustomerSiteJobLocationService : ICustomerSiteJobLocationService, IDisposable
    {
        #region Variables

        private readonly IRepository<CustomerSiteJobLocation> _customerSiteJobLocationRepository;
        int response;

        #endregion

        #region _ctor

        public CustomerSiteJobLocationService(IRepository<CustomerSiteJobLocation> customerSiteJobLocationRepository)
        {
            _customerSiteJobLocationRepository = customerSiteJobLocationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<CustomerSiteJobLocation> GetByID(long CustomerSiteJobLocationID)
        {
            var query = await _customerSiteJobLocationRepository.GetByIdAsync(CustomerSiteJobLocationID); ;
            return query;
        }

        public async Task<List<CustomerSiteJobLocation>> GetAllCustomerSiteJobLocationsByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false)
        {
            List<CustomerSiteJobLocation> CustomerSiteJobLocationList = new List<CustomerSiteJobLocation>();

            var query = _customerSiteJobLocationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerSiteID == CustomerSiteID);

            if (displayAll)
                CustomerSiteJobLocationList = await query.ToListAsync();
            else
                CustomerSiteJobLocationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CustomerSiteJobLocationList;
        }

        public async Task<string> Create(CustomerSiteJobLocation CustomerSiteJobLocation)
        {
            response = await _customerSiteJobLocationRepository.InsertAsync(CustomerSiteJobLocation);
            if (response == 1)
                return CustomerSiteJobLocation.CustomerSiteJobLocationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(CustomerSiteJobLocation CustomerSiteJobLocation)
        {
            response = await _customerSiteJobLocationRepository.UpdateAsync(CustomerSiteJobLocation);
            if (response == 1)
                return CustomerSiteJobLocation.CustomerSiteJobLocationID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
