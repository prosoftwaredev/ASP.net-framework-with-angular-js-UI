using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        string _response;

        public UserController() { }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUsers(FormDataCollection form)
        {
            var draw = form.GetValues("draw").FirstOrDefault();
            var start = form.GetValues("start").FirstOrDefault();
            var length = form.GetValues("length").FirstOrDefault();
            var sortColumn = form.GetValues("columns[" + form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            SearchDataTable search = new SearchDataTable
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchValue = searchValue,
                RecordsTotal = recordsTotal
            };

            PagedData<TempUser> pagedData = await _userService.Get(search);

            if (pagedData != null)
            {
                //var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.UserID, x.Firstname, x.Lastname, x.Email, x.Phone, x.Gender, x.Picture, x.Created, x.IsEnable }) };
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.UserID, x.Firstname, x.Lastname, x.Email, x.Phone, x.Gender, x.Picture, x.Created, x.IsEnable, LastLogin =  x.LastLogin.HasValue ? x.LastLogin.Value.ToString("dd-MMM-yyyy HH:mm:ss tt") + " IP: " + x.IPAddress : "" }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserById(long userId)
        {
            if (userId <= 0)
                return NotFound();

            User user = await _userService.GetById(userId);

            if (user != null)
            {
                var data = new { user.UserID, user.Firstname, user.Lastname, user.Email, user.Phone, user.Gender, user.DOB, user.LastLogin, user.Picture, user.Hash, user.Created, user.IsEnable, user.SortOrder, user.LastUpdated, user.Remark };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody]User User)
        {
            if (User != null)
            {
                List<string> emailParameters = new List<string>();

                Random rand = new Random();
                string password = rand.Next(10000000, 99999999).ToString();
                User.Password = CommonHelper.GetSha1HashCode(password);

                User gotUser = await _userService.GetUserByEmail(User.Email);

                if (gotUser.UserID == 0)
                {
                    _response = await _userService.Create(User);

                    emailParameters.Add(User.Email);
                    emailParameters.Add(User.Firstname + " " + User.Lastname);
                    emailParameters.Add(password);
                    EmailHelper.SendAccountEmail(emailParameters);

                    return Ok(_response);
                }
                else
                    return BadRequest("This email is already registered with us...");
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUser([FromBody]User user)
        {
            if (user != null)
            {
                User newUser = await _userService.GetById(user.UserID);

                newUser.Firstname = user.Firstname;
                newUser.Lastname = user.Lastname;

                if(newUser.Email != user.Email)
                    return BadRequest("You can not change the email");

                newUser.Email = user.Email;
                newUser.Gender = user.Gender;
                newUser.Phone = user.Phone;
                newUser.DOB = user.DOB;
                newUser.Remark = user.Remark;
                newUser.IsEnable = user.IsEnable;
                newUser.LastUpdated = DateTime.Now;
                newUser.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                _response = await _userService.Update(newUser);
                return Ok(_response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Roles = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(long id)
        {
            if (id != 0)
            {
                User newUser = await _userService.GetById(id);
                newUser.IsDelete = true;
                newUser.LastUpdated = DateTime.Now;

                _response = await _userService.Update(newUser);
                return Ok(_response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(Roles = "1,2")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateProfile()
        {
            //Allow to update only picture
            System.Collections.Specialized.NameValueCollection formdata = System.Web.HttpContext.Current.Request.Form;

            Dictionary<string, string> dr = new Dictionary<string, string>();

            foreach (var Key in formdata.AllKeys)
            {
                int i = 0;
                string[] U = formdata.GetValues(Key);
                dr[Key] = U[i];
                i++;
            }

            User User = new User();

            foreach (var item in dr)
            {
                if (item.Key == "UserID")
                    User.UserID = Convert.ToInt64(item.Value);

                if (item.Key == "Firstname")
                    User.Firstname = item.Value;

                if (item.Key == "Lastname")
                    User.Lastname = item.Value;

                if (item.Key == "Email")
                    User.Email = item.Value;

                if (item.Key == "Phone")
                    User.Phone = item.Value;

                if (item.Key == "DOB")
                {
                    if (!string.IsNullOrEmpty(item.Value) && item.Value != "null")
                        User.DOB = Convert.ToDateTime(item.Value);
                    else
                        User.DOB = null;
                }  

                //if(item.Key == "DOB")
                //    User.DOB = Convert.ToDateTime(item.Value);

                if (item.Key == "Gender")
                    User.Gender = item.Value;

                if (item.Key == "Picture")
                    User.Picture = item.Value;
            }

            string folderNameForFile = string.Empty;
            string fileName = string.Empty;

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            if (User == null)
                return NotFound();

            User newUser = await _userService.GetById(User.UserID);

            HttpPostedFile hpf = null;

            if (hfc.Count > 0)
            {
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid().ToString("N");
                        folderNameForFile = fileName.Substring(0, 2);

                        if (File.Exists(HttpContext.Current.Server.MapPath("~/Files/Users/" + User.Picture)))
                            File.Delete(HttpContext.Current.Server.MapPath("~/Files/Users/" + User.Picture));

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Users/" + folderNameForFile + "/" + fileName)))
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Users/" + folderNameForFile + "/" + fileName));

                        var uploadPath = HttpContext.Current.Server.MapPath("~/Files/Users/" + folderNameForFile + "/" + fileName);

                        DirectoryInfo info = new DirectoryInfo(uploadPath);

                        DirectorySecurity security = info.GetAccessControl();

                        security.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

                        info.SetAccessControl(security);

                        if (!File.Exists(uploadPath + "/" + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(uploadPath + "/" + Path.GetFileName(hpf.FileName));
                        }

                        //newUser.Firstname = User.Firstname;
                        //newUser.Lastname = User.Lastname;
                        //newUser.Email = User.Email;
                        newUser.Picture = folderNameForFile + "/" + fileName + "/" + Path.GetFileName(hpf.FileName);
                        //newUser.Gender = User.Gender;
                        //newUser.Phone = User.Phone;
                        //newUser.DOB = User.DOB;
                        newUser.LastUpdated = DateTime.Now;
                        newUser.LastUpdatedBy = RoleHelper.GetCurrentUserID;
                    }
                }
            }
            //else
            //{
            //    newUser.Firstname = User.Firstname;
            //    newUser.Lastname = User.Lastname;
            //    newUser.Email = User.Email;
            //    newUser.Gender = User.Gender;
            //    newUser.Phone = User.Phone;
            //    newUser.DOB = User.DOB;
            //    newUser.LastUpdated = DateTime.Now;
            //}

            _response = await _userService.Update(newUser);
            return Ok(_response);
        }

        //[Authorize(Roles = "1,3")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword([FromBody]PasswordData PasswordData)
        {
            if (PasswordData != null)
            {
                string currentPassword = await _userService.GetPasswordByUserId(PasswordData.UserID);
                string newPassword = CommonHelper.GetSha1HashCode(PasswordData.Password);

                if (currentPassword == newPassword)
                {
                    User newUser = await _userService.GetById(PasswordData.UserID);

                    newUser.Password = CommonHelper.GetSha1HashCode(PasswordData.NewPassword);
                    newUser.LastUpdated = DateTime.Now;
                    newUser.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    _response = await _userService.Update(newUser);
                    return Ok(_response);
                }
                else
                    return Ok(-1);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
