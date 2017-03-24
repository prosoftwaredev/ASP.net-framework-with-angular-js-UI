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
    public class EmployeeSkillService : IEmployeeSkillService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeSkill> _employeeSkillRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeSkillService(IRepository<EmployeeSkill> employeeSkillRepository)
        {
            _employeeSkillRepository = employeeSkillRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<EmployeeSkill> GetByID(long EmployeeSkillID)
        {
            var query = await _employeeSkillRepository.GetByIdAsync(EmployeeSkillID); ;
            return query;
        }

        public async Task<EmployeeSkill> GetAllEmployeeSkillsBySkillIDAndEmployeeID(long EmployeeID, long SkillID, bool displayAll = false, bool isDelete = false)
        {
            EmployeeSkill EmployeeSkill = new EmployeeSkill();

            var query = _employeeSkillRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID && x.SkillID == SkillID);

            if (displayAll)
                EmployeeSkill = await query.Take(1).SingleOrDefaultAsync();
            else
                EmployeeSkill = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return EmployeeSkill;
        }

        public async Task<List<EmployeeSkill>> GetAllEmployeeSkillsByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeSkill> EmployeeSkillList = new List<EmployeeSkill>();

            var query = _employeeSkillRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeSkillList = await query.ToListAsync();
            else
                EmployeeSkillList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeSkillList;
        }

        public async Task<string> Create(EmployeeSkill EmployeeSkill)
        {
            response = await _employeeSkillRepository.InsertAsync(EmployeeSkill);
            if (response == 1)
                return EmployeeSkill.EmployeeSkillID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeSkill EmployeeSkill)
        {
            response = await _employeeSkillRepository.UpdateAsync(EmployeeSkill);
            if (response == 1)
                return EmployeeSkill.EmployeeSkillID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
