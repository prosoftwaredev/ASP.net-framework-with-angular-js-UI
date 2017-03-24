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
    public class EmployeeTypeService : IEmployeeTypeService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeType> _employeeTypeRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeTypeService(IRepository<EmployeeType> employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<EmployeeType>> Get(SearchDataTable search)
        {
            PagedData<EmployeeType> pageData = new PagedData<EmployeeType>();

            var query = _employeeTypeRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long EmployeeTypeID = 0, tempEmployeeTypeID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempEmployeeTypeID))
                    EmployeeTypeID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;


                query = query.Where(x => x.EmployeeTypeID == EmployeeTypeID ||
                    x.EmployeeTypeName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "EmployeeTypeID":
                            query = query.OrderBy(x => x.EmployeeTypeID);
                            break;

                        case "EmployeeTypeName":
                            query = query.OrderBy(x => x.EmployeeTypeName);
                            break;

                        default:
                            query = query.OrderBy(x => x.EmployeeTypeID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "EmployeeTypeID":
                            query = query.OrderByDescending(x => x.EmployeeTypeID);
                            break;

                        case "EmployeeTypeName":
                            query = query.OrderByDescending(x => x.EmployeeTypeName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.EmployeeTypeID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<EmployeeType> GetByID(long EmployeeTypeID)
        {
            var query = await _employeeTypeRepository.GetByIdAsync(EmployeeTypeID); ;
            return query;
        }

        public async Task<string> Create(EmployeeType EmployeeType)
        {
            response = await _employeeTypeRepository.InsertAsync(EmployeeType);
            if (response == 1)
                return EmployeeType.EmployeeTypeID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeType EmployeeType)
        {
            response = await _employeeTypeRepository.UpdateAsync(EmployeeType);
            if (response == 1)
                return EmployeeType.EmployeeTypeID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<EmployeeType>> GetAllEmployeeTypes(bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeType> EmployeeTypeList = new List<EmployeeType>();

            var query = _employeeTypeRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                EmployeeTypeList = await query.ToListAsync();
            else
                EmployeeTypeList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeTypeList;
        }

        #endregion
    }
}
