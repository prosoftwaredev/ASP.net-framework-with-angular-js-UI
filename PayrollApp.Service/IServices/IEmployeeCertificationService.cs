using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeCertificationService
    {
        Task<EmployeeCertification> GetByID(long EmployeeCertificationID);
        Task<string> Create(EmployeeCertification EmployeeCertification);
        Task<string> Update(EmployeeCertification EmployeeCertification);

        Task<EmployeeCertification> GetAllEmployeeCertificationsByCertificationIDAndEmployeeID(long EmployeeID, long CertificationID, bool displayAll = false, bool isDelete = false);
        Task<List<EmployeeCertification>> GetAllEmployeeCertificationsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
