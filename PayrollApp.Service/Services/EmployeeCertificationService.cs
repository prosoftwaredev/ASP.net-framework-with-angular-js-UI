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
    public class EmployeeCertificationService : IEmployeeCertificationService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeCertification> _employeeCertificationRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeCertificationService(IRepository<EmployeeCertification> employeeCertificationRepository)
        {
            _employeeCertificationRepository = employeeCertificationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<EmployeeCertification> GetByID(long EmployeeCertificationID)
        {
            var query = await _employeeCertificationRepository.GetByIdAsync(EmployeeCertificationID); ;
            return query;
        }

        public async Task<EmployeeCertification> GetAllEmployeeCertificationsByCertificationIDAndEmployeeID(long EmployeeID, long CertificationID, bool displayAll = false, bool isDelete = false)
        {
            EmployeeCertification EmployeeCertification = new EmployeeCertification();

            var query = _employeeCertificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID && x.CertificationID == CertificationID);

            if (displayAll)
                EmployeeCertification = await query.Take(1).SingleOrDefaultAsync();
            else
                EmployeeCertification = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return EmployeeCertification;
        }

        public async Task<List<EmployeeCertification>> GetAllEmployeeCertificationsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeCertification> EmployeeCertificationList = new List<EmployeeCertification>();

            var query = _employeeCertificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeCertificationList = await query.ToListAsync();
            else
                EmployeeCertificationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeCertificationList;
        }

        public async Task<string> Create(EmployeeCertification EmployeeCertification)
        {
            response = await _employeeCertificationRepository.InsertAsync(EmployeeCertification);
            if (response == 1)
                return EmployeeCertification.EmployeeCertificationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeCertification EmployeeCertification)
        {
            response = await _employeeCertificationRepository.UpdateAsync(EmployeeCertification);
            if (response == 1)
                return EmployeeCertification.EmployeeCertificationID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
