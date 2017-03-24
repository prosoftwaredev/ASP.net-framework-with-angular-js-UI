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
    public class RoleService : IRoleService, IDisposable
    {
        #region Variables

        private readonly IRepository<Role> _roleRepository;
        int response;

        #endregion

        #region _ctor

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<Role>> Get(SearchDataTable search)
        {
            PagedData<Role> pageData = new PagedData<Role>();

            var query = _roleRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long roleID = 0, tempRoleID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempRoleID))
                    roleID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.RoleID == roleID ||
                    x.RoleName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "RoleID":
                            query = query.OrderBy(x => x.RoleID);
                            break;

                        case "RoleName":
                            query = query.OrderBy(x => x.RoleName);
                            break;

                        default:
                            query = query.OrderBy(x => x.RoleID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "RoleID":
                            query = query.OrderByDescending(x => x.RoleID);
                            break;

                        case "RoleName":
                            query = query.OrderByDescending(x => x.RoleName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.RoleID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Role> GetByID(int RoleID)
        {
            var query = await _roleRepository.GetByIdAsync(RoleID); ;
            return query;
        }

        public async Task<string> Create(Role Role)
        {
            response = await _roleRepository.InsertAsync(Role);
            if (response == 1)
                return Role.RoleID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Role Role)
        {
            response = await _roleRepository.UpdateAsync(Role);
            if (response == 1)
                return Role.RoleID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Role>> GetAllRoles(bool displayAll = false, bool IsDelete = false)
        {
            List<Role> RoleList = new List<Role>();

            var query = _roleRepository.Table;

            query = query.Where(x => x.IsDelete == IsDelete);

            if (displayAll)
                RoleList = await query.ToListAsync();
            else
                RoleList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return RoleList;
        }
        #endregion
    }
}
