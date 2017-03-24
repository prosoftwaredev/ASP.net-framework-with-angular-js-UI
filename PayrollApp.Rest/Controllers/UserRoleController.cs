using PayrollApp.Core.Data.System;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class UserRoleController : ApiController
    {
         private readonly IUserRoleService _userRoleService;
        string response;

        public UserRoleController() { }

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserRoles(int draw, int length, int start)
        {
            PagedData<UserRole> pagedData = await _userRoleService.Get(start, length);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.UserRoleID, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserRoleByID(int UserRoleID)
        {
            if (UserRoleID <= 0)
                return NotFound();

            UserRole UserRole = await _userRoleService.GetByID(UserRoleID);

            if (UserRole != null)
            {
                var data = new { UserRole.UserRoleID, UserRole.UserID, UserRole.RoleID, UserRole.Created, UserRole.IsEnable, UserRole.LastUpdated, UserRole.Remark, UserRole.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserRole([FromBody]UserRole UserRole)
        {
            if (UserRole != null)
            {
                UserRole newUserRole = await _userRoleService.GetAllUserRolesByRoleIDAndUserID(UserRole.UserID, UserRole.RoleID);

                if (newUserRole == null)
                {
                    response = await _userRoleService.Create(UserRole);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserRole([FromBody]UserRole UserRole)
        {
            if (UserRole != null)
            {
                UserRole newUserRole = await _userRoleService.GetByID(UserRole.UserRoleID);
                newUserRole.IsEnable = UserRole.IsEnable;
                newUserRole.Remark = UserRole.Remark;
                newUserRole.LastUpdated = DateTime.Now;
                newUserRole.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _userRoleService.Update(newUserRole);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUserRole([FromBody] UserRole UserRole)
        {
            if (UserRole != null)
            {
                UserRole newUserRole = await _userRoleService.GetAllUserRolesByRoleIDAndUserID(UserRole.UserID, UserRole.RoleID);

                if (newUserRole != null)
                {
                    newUserRole.IsDelete = true;
                    newUserRole.LastUpdated = DateTime.Now;

                    response = await _userRoleService.Update(newUserRole);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRolesByUserID(long UserID)
        {
            List<UserRole> UserRoleList = await _userRoleService.GetAllUserRolesByUserID(UserID);

            if (UserRoleList != null)
            {
                var data = UserRoleList.Select(x => new { x.UserRoleID, x.RoleID, x.UserID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserRoleByRadio([FromBody]UserRole UserRole)
        {
            if (UserRole != null)
            {
                List<UserRole> UserRoleList = await _userRoleService.GetAllUserRolesByUserID(UserRole.UserID);

                if (UserRoleList == null)
                {
                    response = await _userRoleService.Create(UserRole);
                    return Ok(response);
                }
                else
                {
                    foreach (var item in UserRoleList)
                    {
                        UserRole newUserRole = await _userRoleService.GetAllUserRolesByRoleIDAndUserID(item.UserID, item.RoleID);
                        newUserRole.IsDelete = true;
                        newUserRole.LastUpdated = DateTime.Now;
                        response = await _userRoleService.Update(newUserRole);
                    }

                    response = await _userRoleService.Create(UserRole);
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
