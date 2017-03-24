using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.Helper;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class EmployeeService : IEmployeeService, IDisposable
    {
        #region Variables

        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<EmployeeNote> _employeeNoteRepository;

        private readonly IRepository<EmployeeLabourClassification> _employeeLabourClassificationRepository;
        private readonly IRepository<EmployeeCertification> _employeeCertificationRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<EmployeeNote> employeeNoteRepository, IRepository<EmployeeLabourClassification> employeeLabourClassificationRepository, IRepository<EmployeeCertification> employeeCertificationRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeNoteRepository = employeeNoteRepository;
            _employeeLabourClassificationRepository = employeeLabourClassificationRepository;
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

        public async Task<PagedData<Employee>> Get(SearchDataTable search)
        {
            PagedData<Employee> pageData = new PagedData<Employee>();

            var query = _employeeRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long EmployeeID = 0, tempEmployeeID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;
                //long EmployeeTypeID = 0, tempEmployeeTypeID = 0;

                if (long.TryParse(search.SearchValue, out tempEmployeeID))
                    EmployeeID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    //else
                    //        if (long.TryParse(search.SearchValue, out tempEmployeeTypeID))
                    //            EmployeeTypeID = Convert.ToInt64(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;


                query = query.Where(x => x.EmployeeID == EmployeeID ||
                    x.FirstName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.LastName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.EmailMain.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Phone.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.SIN.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.AccountNo.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    //x.EmployeeType.EmployeeTypeName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year ||
                    x.IsEnable == isEnable);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "EmployeeID":
                            query = query.OrderBy(x => x.EmployeeID);
                            break;

                        case "FirstName":
                            query = query.OrderBy(x => x.FirstName);
                            break;

                        case "LastName":
                            query = query.OrderBy(x => x.LastName);
                            break;


                        case "EmailAddr":
                            query = query.OrderBy(x => x.EmailMain);
                            break;

                        case "HomePhone":
                            query = query.OrderBy(x => x.Phone);
                            break;


                        case "SIN":
                            query = query.OrderBy(x => x.SIN);
                            break;

                        case "AccountNo":
                            query = query.OrderBy(x => x.AccountNo);
                            break;

                        default:
                            query = query.OrderBy(x => x.EmployeeID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "EmployeeID":
                            query = query.OrderByDescending(x => x.EmployeeID);
                            break;

                        case "FirstName":
                            query = query.OrderByDescending(x => x.FirstName);
                            break;

                        case "LastName":
                            query = query.OrderByDescending(x => x.LastName);
                            break;


                        case "EmailAddr":
                            query = query.OrderByDescending(x => x.EmailMain);
                            break;

                        case "HomePhone":
                            query = query.OrderByDescending(x => x.Phone);
                            break;


                        case "SIN":
                            query = query.OrderByDescending(x => x.SIN);
                            break;

                        case "AccountNo":
                            query = query.OrderByDescending(x => x.AccountNo);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.EmployeeID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Employee> GetByID(long EmployeeID)
        {
            var query = await _employeeRepository.GetByIdAsync(EmployeeID); ;
            return query;
        }

        public async Task<string> Create(Employee Employee)
        {
            response = await _employeeRepository.InsertAsync(Employee);
            if (response == 1)
                return Employee.EmployeeID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Employee Employee)
        {
            response = await _employeeRepository.UpdateAsync(Employee);
            if (response == 1)
                return Employee.EmployeeID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<PagedData<Employee>> GetSearchResult(SearchDataTable search)
        {
            PagedData<Employee> pageData = new PagedData<Employee>();

            var employee = _employeeRepository.Table;

            employee = employee.Where(x => x.IsDelete == search.IsDelete);

            var skill = _employeeLabourClassificationRepository.Table;

            var cert = _employeeCertificationRepository.Table;

            //var query = employee.Select(x => x.EmployeeSkills.Where(y => y.SkillID == search.SkillID));

            //var query = employee.Join(skill, e => e.EmployeeID, s => s.EmployeeID, (e, s) => new { e, s }).Where(x => x.s.SkillID == search.SkillID);
            //.Join(cert, q => q.e.EmployeeID, c => c.EmployeeID, (q, c) => new { q, c }).Where(x => x.c.CertificationID == search.CertificationID);

            pageData.Count = await employee.CountAsync();

            var result = employee.OrderByDescending(x => x.EmployeeID).Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await result.ToListAsync();

            return pageData;
        }

        public async Task<List<Employee>> GetSearchResult(long SkillID, long CertificationID)
        {
            List<Employee> Employees = new List<Employee>();

            var employee = _employeeRepository.Table;

            var skill = _employeeLabourClassificationRepository.Table;

            var cert = _employeeCertificationRepository.Table;

            var query = employee.Join(skill, e => e.EmployeeID, s => s.EmployeeID, (e, s) => new { e, s })
                .Join(cert, q => q.e.EmployeeID, c => c.EmployeeID, (q, c) => new { q, c });

            if (SkillID > 0)
                query = query.Where(x => x.q.s.LabourClassificationID == SkillID);

            if (CertificationID > 0)
                query = query.Where(x => x.c.CertificationID == CertificationID);

            var result = query.Select(x => x.q.e).OrderByDescending(x => x.EmployeeID);

            Employees = await result.ToListAsync();

            return Employees;
        }

        public async Task<PagedData<Employee>> Get(SearchEmployee search)
        {
            PagedData<Employee> pageData = new PagedData<Employee>();

            var employee = _employeeRepository.Table;

            employee = employee.Where(x => x.IsDelete == search.IsDelete);

            var skill = _employeeLabourClassificationRepository.Table;

            skill = skill.Where(x => x.IsDelete == search.IsDelete);

            var cert = _employeeCertificationRepository.Table;

            cert = cert.Where(x => x.IsDelete == search.IsDelete);


            var query = employee;
            //.Join(skill, e => e.EmployeeID, s => s.EmployeeID, (e, s) => new { e, s })
            //.Join(cert, q => q.e.EmployeeID, c => c.EmployeeID, (q, c) => new { q, c })
            //.GroupBy(x => x.q.e.EmployeeID).Select(group => group.FirstOrDefault());

            //var query = employee.GroupJoin(skill, e => e.EmployeeID, s => s.EmployeeID, (e, s) => new { e, s }).SelectMany(x => x.s.DefaultIfEmpty(), (x, y) => new { p = x, q = y });

            if (search.SkillID > 0)
            {
                query = from e in employee
                        from p in e.EmployeeLabourClassifications
                        where p.LabourClassificationID == search.SkillID
                        select e;
            }
            //query = query.Where(x => x.EmployeeSkills.)
            //query = query.Where(x => x.q.s.SkillID == search.SkillID);

            if (search.CertificationID > 0)
            {
                query = from e in employee
                        from p in e.EmployeeCertifications
                        where p.CertificationID == search.CertificationID
                        select e;
            }
            //query = query.Where(x => x.c.CertificationID == search.CertificationID);

            if (!string.IsNullOrEmpty(search.GlobalSearch))
            {
                long EmployeeID = 0, tempEmployeeID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.GlobalSearch, out tempEmployeeID))
                    EmployeeID = Convert.ToInt64(search.GlobalSearch);
                else
                    if (DateTime.TryParse(search.GlobalSearch, out tempCreated))
                        Created = Convert.ToDateTime(search.GlobalSearch);
                    else
                        if (search.GlobalSearch.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.GlobalSearch.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.EmployeeID == EmployeeID ||
                    x.FirstName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.LastName.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.EmailMain.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Phone.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.SIN.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.AccountNo.Trim().ToLower().Contains(search.GlobalSearch.Trim().ToLower()) ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year ||
                    x.IsEnable == isEnable);
            }

            query = query.GroupBy(x => x.EmployeeID).Select(group => group.FirstOrDefault());
            //query = query.GroupBy(x => x.q.e.EmployeeID).Select(group => group.FirstOrDefault());

            pageData.Count = await query.CountAsync();

            query = query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ThenBy(x => x.MiddleName).Skip((search.PageSize * search.PageNumber) - search.PageSize).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<string> GetAccountNumber(string str)
        {
            string accountNo = string.Empty;

            str = str.ToUpper();

            var query = _employeeRepository.Table;

            var accountNoList = await query.Select(x => x.AccountNo).Where(x => x != null).ToListAsync();

            if (accountNoList.Count > 0)
            {
                Lookup<string, string> lookup = (Lookup<string, string>)accountNoList.ToLookup(x => CommonHelper.FindAlphas(x), x => CommonHelper.FindNumber(x));

                var matchedList = lookup.Where(x => x.Key == str).ToList();

                if (matchedList.Count > 0)
                {
                    var matchedKey = matchedList.FirstOrDefault();

                    var matchedMax = matchedKey.Max();

                    accountNo = matchedKey.Key + matchedMax;
                }
                else
                    return accountNo;
            }

            return accountNo;
        }

        public async Task<bool> GetSINNumber(string str)
        {
            var query = _employeeRepository.Table;

            query = query.Where(x => x.SIN.ToLower().Trim() == str.ToLower().Trim());

            query = query.OrderByDescending(x => x.AccountNo);

            if (await query.AnyAsync())
                return true;

            return false;
        }

        public async Task<List<Employee>> GetAllEmployees(bool displayAll = false, bool isDelete = false)
        {
            List<Employee> EmployeeList = new List<Employee>();

            var query = _employeeRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                EmployeeList = await query.ToListAsync();
            else
                EmployeeList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeList;
        }

        #endregion
    }
}
