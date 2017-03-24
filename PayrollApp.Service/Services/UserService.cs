using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
//using System.Linq.Dynamic;

namespace PayrollApp.Service.Services
{
    public class UserService : IUserService, IDisposable
    {
        #region Variables

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<LastLogin> _lastLoginRepository;
        int _response;

        #endregion

        #region _ctor

        public UserService(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository, IRepository<LastLogin> lastLoginRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _lastLoginRepository = lastLoginRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<TempUser>> Get(SearchDataTable search)
        {
            PagedData<TempUser> pageData = new PagedData<TempUser>();

            var user = _userRepository.Table;

            var logins = _lastLoginRepository.Table;

            user = user.Where(x => x.IsDelete == search.IsDelete);

            #region Global Search

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long userID = 0, tempUserID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempUserID))
                    userID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                user = user.Where(x => x.UserID == userID ||
                    x.Firstname.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Lastname.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Email.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Phone.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            #endregion

            //var newQuery = user.Select(x => new 
            //UserViewModel {
            //    UserID = x.UserID,
            //    Firstname = x.Firstname,
            //    Lastname = x.Lastname,
            //    Email = x.Email,
            //    Phone = x.Phone,
            //    Gender = x.Gender,
            //    Picture = x.Picture,
            //    Created = x.Created,
            //    IsEnable = x.IsEnable,
            //    LastLogin = x.Created,
            //    IPAddress = x.Remark
            //});

            //var newQuery = (from u in user
            //         join l in logins 
            //         on u.UserID equals l.UserID 
            //         into l_join
            //         from l in l_join.DefaultIfEmpty()
            //         select new UserViewModel
            //         {
            //           UserID = u.UserID,
            //           Firstname = u.Firstname,
            //           Lastname = u.Lastname,
            //           Email = u.Email,
            //           Phone = u.Phone,
            //           Gender = u.Gender,
            //           Picture = u.Picture,
            //           Created = u.Created,
            //           IsEnable = u.IsEnable,
            //           LastLogin = l.DateTime,
            //           IPAddress = l.IPAddress,
            //           LastLoginID = l.LastLoginID
            //         });

            //var newQuery = from i in query
            //           group i by i.UserID into g
            //           //orderby g.FirstOrDefault().LastLoginID descending
            //           select g.FirstOrDefault();

            //var query = user.GroupJoin(logins, u => u.UserID, l => l.UserID, (u, l) => new { u, l }).SelectMany(x => x.l.DefaultIfEmpty(), (x, y) => new { x, y });

            //query = query.GroupBy(x => x.x.u.UserID).Select(group => group.LastOrDefault());

            //var newQuery = query.Select(x => new UserViewModel { UserID = x.x.u.UserID, Firstname = x.x.u.Firstname, Lastname = x.x.u.Lastname, Email = x.x.u.Email, Phone = x.x.u.Phone, Gender = x.x.u.Gender, Picture = x.x.u.Picture, Created = x.x.u.Created, IsEnable = x.x.u.IsEnable, LastLogin = x.y.DateTime, IPAddress = x.y.IPAddress });

            var newQuery = from u in user
                           select new TempUser
                           {
                               UserID = u.UserID,
                               Firstname = u.Firstname,
                               Lastname = u.Lastname,
                               Email = u.Email,
                               Phone = u.Phone,
                               Gender = u.Gender,
                               Picture = u.Picture,
                               Created = u.Created,
                               IsEnable = u.IsEnable,
                               LastLogin =
                                  ((from l in logins
                                    where
                                      l.UserID == u.UserID
                                    orderby
                                      l.DateTime descending
                                    select new
                                    {
                                        l.DateTime
                                    }).Take(1).FirstOrDefault().DateTime),
                               IPAddress =
                                 ((from l in logins
                                   where
                                     l.UserID == u.UserID
                                   orderby
                                     l.DateTime descending
                                   select new
                                   {
                                       l.IPAddress
                                   }).Take(1).FirstOrDefault().IPAddress)
                           };

            #region Sorting

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "UserID":
                            newQuery = newQuery.OrderBy(x => x.UserID);
                            break;

                        case "Firstname":
                            newQuery = newQuery.OrderBy(x => x.Firstname);
                            break;

                        case "Lastname":
                            newQuery = newQuery.OrderBy(x => x.Lastname);
                            break;

                        case "Email":
                            newQuery = newQuery.OrderBy(x => x.Email);
                            break;

                        case "Phone":
                            newQuery = newQuery.OrderBy(x => x.Phone);
                            break;

                        case "Created":
                            newQuery = newQuery.OrderBy(x => x.Created);
                            break;

                        case "IsEnable":
                            newQuery = newQuery.OrderBy(x => x.IsEnable);
                            break;

                        default:
                            newQuery = newQuery.OrderBy(x => x.UserID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "UserID":
                            newQuery = newQuery.OrderByDescending(x => x.UserID);
                            break;

                        case "Firstname":
                            newQuery = newQuery.OrderByDescending(x => x.Firstname);
                            break;

                        case "Lastname":
                            newQuery = newQuery.OrderByDescending(x => x.Lastname);
                            break;

                        case "Email":
                            newQuery = newQuery.OrderByDescending(x => x.Email);
                            break;

                        case "Phone":
                            newQuery = newQuery.OrderByDescending(x => x.Phone);
                            break;

                        case "Created":
                            newQuery = newQuery.OrderByDescending(x => x.Created);
                            break;

                        case "IsEnable":
                            newQuery = newQuery.OrderByDescending(x => x.IsEnable);
                            break;

                        default:
                            newQuery = newQuery.OrderByDescending(x => x.UserID);
                            break;
                    }
                }
            }

            #endregion

            pageData.Count = await newQuery.CountAsync();

            newQuery = newQuery.Skip(search.Skip).Take(search.PageSize);

            //newQuery = newQuery.OrderByDescending(x => x.UserID).Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await newQuery.ToListAsync();

            return pageData;
        }

        public async Task<User> GetById(long userId)
        {
            var query = await _userRepository.GetByIdAsync(userId); ;
            return query;
        }

        public async Task<string> Create(User User)
        {
            _response = await _userRepository.InsertAsync(User);
            if (_response == 1)
                return User.UserID.ToString();
            else
                return _response.ToString();
        }

        public async Task<string> Update(User user)
        {
            _response = await _userRepository.UpdateAsync(user);
            if (_response == 1)
                return user.UserID.ToString();
            else
                return _response.ToString();
        }

        public async Task<string> GetPasswordByUserId(long userId)
        {
            var query = _userRepository.Table;
            query = query.Where(x => x.UserID == userId && x.IsEnable == true);

            if (query.Any())
            {
                User user = await query.Take(1).SingleOrDefaultAsync();
                return user.Password;
            }
            else
                return "";
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


        #endregion
    }
}
