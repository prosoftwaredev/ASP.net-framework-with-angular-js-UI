using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeLabourClassificationService
    {
        Task<EmployeeLabourClassification> GetByID(long EmployeeLabourClassificationID);
        Task<string> Create(EmployeeLabourClassification EmployeeLabourClassification);
        Task<string> Update(EmployeeLabourClassification EmployeeLabourClassification);

        Task<EmployeeLabourClassification> GetAllEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(long EmployeeID, long LabourClassificationID, bool displayAll = false, bool isDelete = false);
        Task<List<EmployeeLabourClassification>> GetAllEmployeeLabourClassificationsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
