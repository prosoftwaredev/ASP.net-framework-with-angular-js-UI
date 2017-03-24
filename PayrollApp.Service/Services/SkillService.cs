using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class SkillService : ISkillService, IDisposable
    {
        #region Variables

        private readonly IRepository<Skill> _skillRepository;
        private readonly IRepository<EmployeeSkill> _employeeSkillRepository;
        int response;

        #endregion

        #region _ctor

        public SkillService(IRepository<Skill> skillRepository, IRepository<EmployeeSkill> employeeSkillRepository)
        {
            _skillRepository = skillRepository;
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


        public async Task<PagedData<Skill>> Get(SearchDataTable search)
        {
            PagedData<Skill> pageData = new PagedData<Skill>();

            var query = _skillRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long SkillID = 0, tempSkillID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempSkillID))
                    SkillID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.SkillID == SkillID ||
                    x.SkillName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "SkillID":
                            query = query.OrderBy(x => x.SkillID);
                            break;

                        case "SkillName":
                            query = query.OrderBy(x => x.SkillName);
                            break;

                        default:
                            query = query.OrderBy(x => x.SkillID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "SkillID":
                            query = query.OrderByDescending(x => x.SkillID);
                            break;

                        case "SkillName":
                            query = query.OrderByDescending(x => x.SkillName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.SkillID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Skill> GetByID(long SkillID)
        {
            var query = await _skillRepository.GetByIdAsync(SkillID); ;
            return query;
        }

        public async Task<string> Create(Skill Skill)
        {
            response = await _skillRepository.InsertAsync(Skill);
            if (response == 1)
                return Skill.SkillID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Skill Skill)
        {
            response = await _skillRepository.UpdateAsync(Skill);
            if (response == 1)
                return Skill.SkillID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Skill>> GetAllSkills(bool displayAll = false, bool isDelete = false)
        {
            List<Skill> SkillList = new List<Skill>();

            var query = _skillRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                SkillList = await query.ToListAsync();
            else
                SkillList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return SkillList;
        }

        public async Task<List<Skill>> GetAllSkills(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeSkill> EmployeeSkillList = new List<EmployeeSkill>();

            var query1 = _employeeSkillRepository.Table;

            query1 = query1.Where(x => x.IsDelete == isDelete);

            query1 = query1.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeSkillList = await query1.ToListAsync();
            else
                EmployeeSkillList = await query1.Where(x => x.IsEnable == true).ToListAsync();

            List<long> SkillIDList = EmployeeSkillList.Select(x => x.SkillID).ToList();

            List<Skill> SkillList = new List<Skill>();

            var query2 = _skillRepository.Table;

            query2 = query2.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                SkillList = await query2.ToListAsync();
            else
                SkillList = await query2.Where(x => x.IsEnable == true).ToListAsync();

            var newList = SkillList.Where(x => !SkillIDList.Contains(x.SkillID)).ToList();

            return newList;
        }

        #endregion
    }
}
