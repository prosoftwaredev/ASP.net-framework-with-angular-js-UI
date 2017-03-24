using PayrollApp.Core.Data.System;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PayrollApp.Rest.Helpers
{
    public class UserHelper
    {
        public static IUserService UserService;
        public IUserService UserService1;

        public async static Task<string> GetUserName(long? UserID)
        {
            User User = new User();
            if (UserID.HasValue && UserID.Value > 0)
            {
                User = await UserService.GetById(UserID.Value);
                return User.Firstname + " " + User.Lastname;
            }
            else
                return "";
        }

        public async Task<string> GetUserName1(long? UserID)
        {
            User User = new User();
            if (UserID.HasValue && UserID.Value > 0)
            {
                User = await UserService1.GetById(UserID.Value);
                return User.Firstname + " " + User.Lastname;
            }
            else
                return "";
        }
    }
}