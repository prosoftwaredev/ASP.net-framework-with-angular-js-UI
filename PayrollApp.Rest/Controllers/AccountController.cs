using Microsoft.Owin.Security;
using PayrollApp.Core.Data.System;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;


        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public AccountController() { }

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(User user)
        {
            if (user.Email == null)
                return NotFound();

            User gotUser = await _accountService.GetUserByEmail(user.Email);

            if (gotUser.UserID == 0)
            {
                Random rand = new Random();
                string hash = CommonHelper.GetSha1HashCode(rand.Next(000001, 999999).ToString());
                user.Hash = hash;

                user.Password = CommonHelper.GetSha1HashCode(user.Password);

                string response = await _accountService.RegisterUser(user);
                long userId = Convert.ToInt64(response);

                if (userId > 0)
                {
                   
                    UserRole userRole = null;
                    long roleId = 0;

                    userRole = new UserRole { UserID = userId, RoleID = RoleHelper.GetRoleID(RoleHelper.RoleName.Admin), Created = DateTime.Now, IsEnable = true, LastUpdated = DateTime.Now, SortOrder = 1, Remark = "NA" };
                    roleId = await _accountService.CreateUserRole(userRole);

                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
                if (gotUser.UserID != 0)
                    return BadRequest("This email is already registered with us...");
                else
                    return InternalServerError();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> SendForgotPasswordLink(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Random rand = new Random();
                string hash = CommonHelper.GetSha1HashCode(rand.Next(000001, 999999).ToString());

                User User = await _accountService.GetUserByEmail(email);

                if (User.UserID > 0)
                {
                    if (User.IsDelete == true)
                    {
                        return BadRequest("Your account was deleted by admin !!!");
                    }
                    else if (User.IsEnable == false)
                    {
                        return BadRequest("Your account is disable by admin !!!");
                    }
                    else
                    {
                        User.Hash = hash;
                        await _userService.Update(User);

                        string url = EmailHelper.PayrollDomain + "/forgot-password/" + User.Email + "/" + User.Hash;
                        bool IsMailSend = EmailHelper.SendForgotPasswordLink(User.Email, User.Firstname + " " + User.Lastname, url);

                        return Ok(IsMailSend);
                    }
                }
                else
                    if (User.UserID == 0 || User == null)
                        return BadRequest("This email is not registered with us...");
                    else
                        return InternalServerError();
            }

            return BadRequest("The email is madetory to send forgot password link");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(User User)
        {
            if (User == null)
                return NotFound();

            User gotUser = await _accountService.GetUserByEmail(User.Email);

            if (gotUser.UserID > 0)
            {
                if (gotUser.Hash == User.Hash)
                {
                    gotUser.Password = CommonHelper.GetSha1HashCode(User.Password);
                    gotUser.LastUpdated = DateTime.Now;
                    gotUser.Hash = "";

                    await _userService.Update(gotUser);
                    return Ok(1);
                }
                else
                    return BadRequest("The link has been malfunctioned");
            }
            else
            {
                return BadRequest("Sorry... User not found..");
            }
        }
    }
}
