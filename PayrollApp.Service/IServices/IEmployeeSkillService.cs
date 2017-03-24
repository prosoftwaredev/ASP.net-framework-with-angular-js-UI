using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeSkillService
    {
        Task<EmployeeSkill> GetByID(long EmployeeSkillID);
        Task<string> Create(EmployeeSkill EmployeeSkill);
        Task<string> Update(EmployeeSkill EmployeeSkill);

        Task<EmployeeSkill> GetAllEmployeeSkillsBySkillIDAndEmployeeID(long EmployeeID, long SkillID, bool displayAll = false, bool isDelete = false);
        Task<List<EmployeeSkill>> GetAllEmployeeSkillsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
