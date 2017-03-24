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
    public class CustomerSiteNoteService : ICustomerSiteNoteService, IDisposable
    {
        #region Variables

        private readonly IRepository<CustomerSiteNote> _customerSiteNoteRepository;
        int response;

        #endregion

        #region _ctor

        public CustomerSiteNoteService(IRepository<CustomerSiteNote> customerSiteNoteRepository)
        {
            _customerSiteNoteRepository = customerSiteNoteRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<CustomerSiteNote> GetByID(long CustomerSiteNoteID)
        {
            var query = await _customerSiteNoteRepository.GetByIdAsync(CustomerSiteNoteID); ;
            return query;
        }

        public async Task<List<CustomerSiteNote>> GetAllCustomerSiteNotesByCustomerSiteID(long CustomerSiteID, bool displayAll = false, bool isDelete = false)
        {
            List<CustomerSiteNote> CustomerSiteNoteList = new List<CustomerSiteNote>();

            var query = _customerSiteNoteRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerSiteID == CustomerSiteID);

            if (displayAll)
                CustomerSiteNoteList = await query.ToListAsync();
            else
                CustomerSiteNoteList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CustomerSiteNoteList;
        }

        public async Task<string> Create(CustomerSiteNote CustomerSiteNote)
        {
            response = await _customerSiteNoteRepository.InsertAsync(CustomerSiteNote);
            if (response == 1)
                return CustomerSiteNote.CustomerSiteNoteID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(CustomerSiteNote CustomerSiteNote)
        {
            response = await _customerSiteNoteRepository.UpdateAsync(CustomerSiteNote);
            if (response == 1)
                return CustomerSiteNote.CustomerSiteNoteID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
