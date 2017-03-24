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
    public class EmployeeLabourClassificationService : IEmployeeLabourClassificationService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeLabourClassification> _employeeLabourClassificationRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeLabourClassificationService(IRepository<EmployeeLabourClassification> employeeLabourClassificationRepository)
        {
            _employeeLabourClassificationRepository = employeeLabourClassificationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<EmployeeLabourClassification> GetByID(long EmployeeLabourClassificationID)
        {
            var query = await _employeeLabourClassificationRepository.GetByIdAsync(EmployeeLabourClassificationID); ;
            return query;
        }

        public async Task<EmployeeLabourClassification> GetAllEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(long EmployeeID, long LabourClassificationID, bool displayAll = false, bool isDelete = false)
        {
            EmployeeLabourClassification EmployeeLabourClassification = new EmployeeLabourClassification();

            var query = _employeeLabourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID && x.LabourClassificationID == LabourClassificationID);

            if (displayAll)
                EmployeeLabourClassification = await query.Take(1).SingleOrDefaultAsync();
            else
                EmployeeLabourClassification = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return EmployeeLabourClassification;
        }

        public async Task<List<EmployeeLabourClassification>> GetAllEmployeeLabourClassificationsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeLabourClassification> EmployeeLabourClassificationList = new List<EmployeeLabourClassification>();

            var query = _employeeLabourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeLabourClassificationList = await query.ToListAsync();
            else
                EmployeeLabourClassificationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeLabourClassificationList;
        }

        public async Task<string> Create(EmployeeLabourClassification EmployeeLabourClassification)
        {
            response = await _employeeLabourClassificationRepository.InsertAsync(EmployeeLabourClassification);
            if (response == 1)
                return EmployeeLabourClassification.EmployeeLabourClassificationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeLabourClassification EmployeeLabourClassification)
        {
            response = await _employeeLabourClassificationRepository.UpdateAsync(EmployeeLabourClassification);
            if (response == 1)
                return EmployeeLabourClassification.EmployeeLabourClassificationID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
