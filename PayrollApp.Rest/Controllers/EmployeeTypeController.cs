using PayrollApp.Core.Data.Entities;
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
    public class EmployeeTypeController : ApiController
    {
        private readonly IEmployeeTypeService _employeeTypeService;
        string response;

        public EmployeeTypeController() { }

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;
        }

        //[Authorize(EmployeeTypes = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEmployeeTypes(FormDataCollection form)
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

            PagedData<EmployeeType> pagedData = await _employeeTypeService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.EmployeeTypeID, x.EmployeeTypeName, x.IsEmployee, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(EmployeeTypes = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeTypeByID(long EmployeeTypeID)
        {
            if (EmployeeTypeID <= 0)
                return NotFound();

            EmployeeType EmployeeType = await _employeeTypeService.GetByID(EmployeeTypeID);

            if (EmployeeType != null)
            {
                var data = new { EmployeeType.EmployeeTypeID, EmployeeType.EmployeeTypeName, EmployeeType.IsEmployee, EmployeeType.Created, EmployeeType.IsEnable, EmployeeType.LastUpdated, EmployeeType.Remark, EmployeeType.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(EmployeeTypes = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeType([FromBody]EmployeeType EmployeeType)
        {
            if (EmployeeType != null)
            {
                response = await _employeeTypeService.Create(EmployeeType);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(EmployeeTypes = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployeeType([FromBody]EmployeeType EmployeeType)
        {
            if (EmployeeType != null)
            {
                EmployeeType newEmployeeType = await _employeeTypeService.GetByID(EmployeeType.EmployeeTypeID);

                newEmployeeType.EmployeeTypeName = EmployeeType.EmployeeTypeName;
                newEmployeeType.IsEmployee = EmployeeType.IsEmployee;
                newEmployeeType.IsEnable = EmployeeType.IsEnable;
                newEmployeeType.Remark = EmployeeType.Remark;
                newEmployeeType.LastUpdated = DateTime.Now;
                newEmployeeType.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeTypeService.Update(newEmployeeType);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(EmployeeTypes = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployeeType(long ID)
        {
            if (ID != 0)
            {
                EmployeeType newEmployeeType = await _employeeTypeService.GetByID(ID);

                newEmployeeType.IsDelete = true;
                newEmployeeType.LastUpdated = DateTime.Now;

                response = await _employeeTypeService.Update(newEmployeeType);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeTypes(bool isDisplayAll)
        {
            List<EmployeeType> EmployeeTypeList = await _employeeTypeService.GetAllEmployeeTypes();

            if (EmployeeTypeList != null)
            {
                var data = EmployeeTypeList.Select(x => new { x.EmployeeTypeID, x.EmployeeTypeName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
