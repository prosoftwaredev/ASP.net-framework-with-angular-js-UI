using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class RoleController : ApiController
    {
         private readonly IRoleService _roleService;
        string response;

        public RoleController() { }

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRoles(FormDataCollection form)
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

            PagedData<Role> pagedData = await _roleService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.RoleID, x.RoleName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetRoleByID(int RoleID)
        {
            if (RoleID <= 0)
                return NotFound();

            Role Role = await _roleService.GetByID(RoleID);

            if (Role != null)
            {
                var data = new { Role.RoleID, Role.RoleName, Role.Created, Role.IsEnable, Role.LastUpdated, Role.Remark, Role.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody]Role Role)
        {
            if (Role != null)
            {
                response = await _roleService.Create(Role);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRole([FromBody]Role Role)
        {
            if (Role != null)
            {
                Role newRole = await _roleService.GetByID(Role.RoleID);

                List<string> propList = new List<string> { "RoleName", "IsEnable", "Remark", "LastUpdated", "LastUpdatedBy" };
                UpdateModel<Role, Role, List<string>> updateModel = new UpdateModel<Role, Role, List<string>>();
                newRole =  updateModel.AssignNewValue(newRole, Role, propList);

                response = await _roleService.Update(newRole);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Roles = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(int ID)
        {
            if (ID != 0)
            {
                Role newRole = await _roleService.GetByID(ID);

                newRole.IsDelete = true;
                newRole.LastUpdated = DateTime.Now;

                response = await _roleService.Update(newRole);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRoles(bool isDisplayAll)
        {
            List<Role> RoleList = await _roleService.GetAllRoles();

            if (RoleList != null)
            {
                var data = RoleList.Select(x => new { x.RoleID, x.RoleName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
