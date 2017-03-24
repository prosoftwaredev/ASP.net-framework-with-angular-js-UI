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
    public class EmployeeBlacklistService : IEmployeeBlacklistService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeBlacklist> _employeeBlacklistRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeBlacklistService(IRepository<EmployeeBlacklist> employeeBlacklistRepository)
        {
            _employeeBlacklistRepository = employeeBlacklistRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<EmployeeBlacklist> GetByID(long EmployeeBlacklistID)
        {
            var query = await _employeeBlacklistRepository.GetByIdAsync(EmployeeBlacklistID); ;
            return query;
        }

        public async Task<EmployeeBlacklist> GetAllEmployeeBlacklistsByCustomerIDAndEmployeeID(long EmployeeID, long CustomerID, bool displayAll = false, bool isDelete = false)
        {
            EmployeeBlacklist EmployeeBlacklist = new EmployeeBlacklist();

            var query = _employeeBlacklistRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID && x.CustomerID == CustomerID);

            if (displayAll)
                EmployeeBlacklist = await query.Take(1).SingleOrDefaultAsync();
            else
                EmployeeBlacklist = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return EmployeeBlacklist;
        }

        public async Task<List<EmployeeBlacklist>> GetAllEmployeeBlacklistsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeBlacklist> EmployeeBlacklistList = new List<EmployeeBlacklist>();

            var query = _employeeBlacklistRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeBlacklistList = await query.ToListAsync();
            else
                EmployeeBlacklistList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeBlacklistList;
        }

        public async Task<string> Create(EmployeeBlacklist EmployeeBlacklist)
        {
            response = await _employeeBlacklistRepository.InsertAsync(EmployeeBlacklist);
            if (response == 1)
                return EmployeeBlacklist.EmployeeBlacklistID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeBlacklist EmployeeBlacklist)
        {
            response = await _employeeBlacklistRepository.UpdateAsync(EmployeeBlacklist);
            if (response == 1)
                return EmployeeBlacklist.EmployeeBlacklistID.ToString();
            else
                return response.ToString();
        }

        public async Task<List<EmployeeBlacklist>> GetAllEmployeeBlacklistsByCustomerID(long CustomerID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeBlacklist> EmployeeBlacklistList = new List<EmployeeBlacklist>();

            var query = _employeeBlacklistRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CustomerID == CustomerID);

            if (displayAll)
                EmployeeBlacklistList = await query.ToListAsync();
            else
                EmployeeBlacklistList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeBlacklistList;
        }

        #endregion
    }
}
