using PayrollApp.Core.Data.System;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class UserRoleService : IUserRoleService, IDisposable
    {
        #region Variables

        private readonly IRepository<UserRole> _userRoleRepository;
        int response;

        #endregion

        #region _ctor

        public UserRoleService(IRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<UserRole>> Get(int PageIndex, int PageSize)
        {
            PagedData<UserRole> pageData = new PagedData<UserRole>();

            var query = _userRoleRepository.Table;

            pageData.Count = query.Count();

            query = query.OrderByDescending(x => x.UserRoleID).Skip(PageIndex).Take(PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<UserRole> GetByID(long UserRoleID)
        {
            var query = await _userRoleRepository.GetByIdAsync(UserRoleID); ;
            return query;
        }

        public async Task<string> Create(UserRole UserRole)
        {
            response = await _userRoleRepository.InsertAsync(UserRole);
            if (response == 1)
                return UserRole.UserRoleID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(UserRole UserRole)
        {
            response = await _userRoleRepository.UpdateAsync(UserRole);
            if (response == 1)
                return UserRole.UserRoleID.ToString();
            else
                return response.ToString();
        }

        public async Task<List<UserRole>> GetAllUserRolesByUserID(long UserID, bool displayAll = false, bool isDelete = false)
        {
            List<UserRole> UserRoleList = new List<UserRole>();

            var query = _userRoleRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.UserID == UserID);

            if (displayAll)
                UserRoleList = await query.ToListAsync();
            else
                UserRoleList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return UserRoleList;
        }

        public async Task<UserRole> GetAllUserRolesByRoleIDAndUserID(long UserID, long RoleID, bool displayAll = false, bool isDelete = false)
        {
            UserRole UserRole = new UserRole();

            var query = _userRoleRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.UserID == UserID && x.RoleID == RoleID);

            if (displayAll)
                UserRole = await query.Take(1).SingleOrDefaultAsync();
            else
                UserRole = await query.Where(x => x.IsEnable == true).Take(1).SingleOrDefaultAsync();

            return UserRole;
        }
        #endregion
    }
}
