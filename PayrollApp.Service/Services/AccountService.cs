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
    public class AccountService : IAccountService, IDisposable
    {
        #region Variables
        public readonly IRepository<User> _userRepository;
        public readonly IRepository<UserRole> _userRoleRepository;
        int response;

        #endregion

        #region _ctor

        public AccountService(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public AccountService()
        {
            PayrollAppDbContext context = new PayrollAppDbContext();
            _userRepository = new GenericRepository<User>(context);
            _userRoleRepository = new GenericRepository<UserRole>(context);
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            //this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<string> RegisterUser(User user)
        {
            response = await _userRepository.InsertAsync(user);
            if (response == 1)
                return user.UserID.ToString();
            else
                return response.ToString();
        }

        public async Task<long> CreateUserRole(UserRole userRole)
        {
            await _userRoleRepository.InsertAsync(userRole);
            return userRole.RoleID;
        }

        public async Task<User> GetUserById(long userId)
        {
            User user = new User();

            var query = _userRepository.Table;
            query = query.Where(x => x.UserID == userId);

            if (await query.AnyAsync())
                return await query.Take(1).SingleOrDefaultAsync();
            else
                return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = new User();

            var query = _userRepository.Table;
            query = query.Where(x => x.Email == email);

            if (await query.AnyAsync())
                return await query.Take(1).SingleOrDefaultAsync();
            else
                return user;
        }

        public async Task<User> GetEnabledUserByEmail(string email)
        {
            User user = new User();

            var query = _userRepository.Table;
            query = query.Where(x => x.Email == email && x.IsEnable == true);

            if (await query.AnyAsync())
                return await query.Take(1).SingleOrDefaultAsync();
            else
                return user;
        }

        public async Task<User> GetEnabledAndVerifyUserByEmailAndPassword(string email, string pasword)
        {
            User user = new User();

            var query = _userRepository.Table;
            query = query.Where(x => x.Email == email && x.Password == pasword && x.IsEnable == true);

            if (await query.AnyAsync())
                return await query.Take(1).SingleOrDefaultAsync();
            else
                return user;
        }

        public async Task<string> GetPasswordByEmail(string email)
        {
            var query = _userRepository.Table;
            query = query.Where(x => x.Email == email && x.IsEnable == true);



            if (await query.AnyAsync())
            {
                User user = await query.Take(1).SingleOrDefaultAsync();
                return user.Password;
            }
            else
                return "";


        }

        public async Task<List<UserRole>> GetUserRolesByUserId(long userId, bool displayAll = false, bool isDelete = false)
        {
            List<UserRole> userRoleList = new List<UserRole>();

            var query = _userRoleRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.UserID == userId);

            if (await query.AnyAsync())
            {
                if (displayAll)
                    userRoleList = await query.ToListAsync();
                else
                    userRoleList = await query.Where(x => x.IsEnable == true).ToListAsync();

                return userRoleList;
            }
            else
                return userRoleList;
        }

        public List<UserRole> GetUserRolesById(long userId)
        {
            List<UserRole> userRoleList = new List<UserRole>();

            var query = _userRoleRepository.Table;
            query = query.Where(x => x.UserID == userId);

            if (query.Any())
            {
                userRoleList = query.ToList();
                return userRoleList;
            }
            else
                return userRoleList;
        }

        #endregion
    }
}
