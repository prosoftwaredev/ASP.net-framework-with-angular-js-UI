using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ISkillService
    {
        Task<PagedData<Skill>> Get(SearchDataTable search);
        Task<Skill> GetByID(long SkillID);
        Task<string> Create(Skill Skill);
        Task<string> Update(Skill Skill);

        Task<List<Skill>> GetAllSkills(bool displayAll = false, bool isDelete = false);
        Task<List<Skill>> GetAllSkills(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
