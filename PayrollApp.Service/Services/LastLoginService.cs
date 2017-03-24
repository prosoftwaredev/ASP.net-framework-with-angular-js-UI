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
    public class LastLoginService : ILastLoginService, IDisposable
    {
        #region Variables

        private readonly IRepository<LastLogin> _lastLoginRepository;
        int response;

        #endregion

        #region _ctor

        public LastLoginService(IRepository<LastLogin> lastLoginRepository)
        {
            _lastLoginRepository = lastLoginRepository;
        }

        public LastLoginService()
        {
            PayrollAppDbContext context = new PayrollAppDbContext();
            _lastLoginRepository = new GenericRepository<LastLogin>(context);
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<LastLogin>> Get(SearchDataTable search)
        {
            PagedData<LastLogin> pageData = new PagedData<LastLogin>();

            var query = _lastLoginRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long LastLoginID = 0, tempLastLoginID = 0;
                bool? isEnable = null;

                if (long.TryParse(search.SearchValue, out tempLastLoginID))
                    LastLoginID = Convert.ToInt64(search.SearchValue);
                else
                    if (search.SearchValue.ToLower() == "yes")
                        isEnable = true;
                    else
                        if (search.SearchValue.ToLower() == "no")
                            isEnable = false;

                query = query.Where(x => x.LastLoginID == LastLoginID ||
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
                        case "LastLoginID":
                            query = query.OrderBy(x => x.LastLoginID);
                            break;

                        default:
                            query = query.OrderBy(x => x.LastLoginID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "LastLoginID":
                            query = query.OrderByDescending(x => x.LastLoginID);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.LastLoginID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<LastLogin> GetByID(long LastLoginID)
        {
            var query = await _lastLoginRepository.GetByIdAsync(LastLoginID); ;
            return query;
        }

        public async Task<string> Create(LastLogin LastLogin)
        {
            response = await _lastLoginRepository.InsertAsync(LastLogin);
            if (response == 1)
                return LastLogin.LastLoginID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(LastLogin LastLogin)
        {
            response = await _lastLoginRepository.UpdateAsync(LastLogin);
            if (response == 1)
                return LastLogin.LastLoginID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<LastLogin>> GetAllLastLogins(bool displayAll = false, bool isDelete = false)
        {
            List<LastLogin> LastLoginList = new List<LastLogin>();

            var query = _lastLoginRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                LastLoginList = await query.ToListAsync();
            else
                LastLoginList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return LastLoginList;
        }

        public async Task<LastLogin> GetLastByUserID(long UserID)
        {
            LastLogin lastLogin = new LastLogin();

            var query = _lastLoginRepository.Table;

            query = query.Where(x => x.UserID == UserID);

            var logins = await query.OrderByDescending(x => x.LastLoginID).FirstOrDefaultAsync();

            if (logins != null)
                lastLogin = logins;

            return lastLogin;
        }

        #endregion
    }
}
