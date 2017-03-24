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
    public class CustomerSiteLabourClassificationService : ICustomerSiteLabourClassificationService, IDisposable
    {
        #region Variables

        private readonly IRepository<CustomerSiteLabourClassification> _customerSiteLabourClassificationRepository;
        int response;

        #endregion

        #region _ctor

        public CustomerSiteLabourClassificationService(IRepository<CustomerSiteLabourClassification> customerSiteLabourClassificationRepository)
        {
            _customerSiteLabourClassificationRepository = customerSiteLabourClassificationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<CustomerSiteLabourClassification> GetByID(long CustomerSiteLabourClassificationID)
        {
            var query = await _customerSiteLabourClassificationRepository.GetByIdAsync(CustomerSiteLabourClassificationID); ;
            return query;
        }

        public async Task<CustomerSiteLabourClassification> GetAllCustomerSiteLabourClassificationsByLabourClassificationIDAndCustomerSiteID(long CustomerSiteID, long LabourClassificationID, bool displayAll = false, bool isDelete = false)
        {
            CustomerSiteLabourClassification CustomerSiteLabourClassification = new CustomerSiteLabourClassification();

            var query = _customerSiteLabourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerSiteID == CustomerSiteID && x.LabourClassificationID == LabourClassificationID);

            if (displayAll)
                CustomerSiteLabourClassification = await query.Take(1).SingleOrDefaultAsync();
            else
                CustomerSiteLabourClassification = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return CustomerSiteLabourClassification;
        }

        public async Task<List<CustomerSiteLabourClassification>> GetAllCustomerSiteLabourClassificationsByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false)
        {
            List<CustomerSiteLabourClassification> CustomerSiteLabourClassificationList = new List<CustomerSiteLabourClassification>();

            var query = _customerSiteLabourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerSiteID == CustomerSiteID);

            if (displayAll)
                CustomerSiteLabourClassificationList = await query.ToListAsync();
            else
                CustomerSiteLabourClassificationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CustomerSiteLabourClassificationList;
        }

        public async Task<string> Create(CustomerSiteLabourClassification CustomerSiteLabourClassification)
        {
            response = await _customerSiteLabourClassificationRepository.InsertAsync(CustomerSiteLabourClassification);
            if (response == 1)
                return CustomerSiteLabourClassification.CustomerSiteLabourClassificationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(CustomerSiteLabourClassification CustomerSiteLabourClassification)
        {
            response = await _customerSiteLabourClassificationRepository.UpdateAsync(CustomerSiteLabourClassification);
            if (response == 1)
                return CustomerSiteLabourClassification.CustomerSiteLabourClassificationID.ToString();
            else
                return response.ToString();
        }

        #endregion

    }
}
