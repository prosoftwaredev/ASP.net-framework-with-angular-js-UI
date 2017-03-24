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
    public class EquipmentController : ApiController
    {
        private readonly IEquipmentService _equipmentService;
        string response;

        public EquipmentController() { }

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        //[Authorize(Equipments = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEquipments(FormDataCollection form)
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

            PagedData<Equipment> pagedData = await _equipmentService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.EquipmentID, x.EquipmentName, x.Rate, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Equipments = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEquipmentByID(long EquipmentID)
        {
            if (EquipmentID <= 0)
                return NotFound();

            Equipment Equipment = await _equipmentService.GetByID(EquipmentID);

            if (Equipment != null)
            {
                var data = new { Equipment.EquipmentID, Equipment.EquipmentName, Equipment.Rate, Equipment.Created, Equipment.IsEnable, Equipment.LastUpdated, Equipment.Remark, Equipment.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Equipments = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEquipment([FromBody]Equipment Equipment)
        {
            if (Equipment != null)
            {
                response = await _equipmentService.Create(Equipment);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Equipments = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEquipment([FromBody]Equipment Equipment)
        {
            if (Equipment != null)
            {
                Equipment newEquipment = await _equipmentService.GetByID(Equipment.EquipmentID);

                newEquipment.EquipmentName = Equipment.EquipmentName;
                newEquipment.Rate = Equipment.Rate;
                newEquipment.IsEnable = Equipment.IsEnable;
                newEquipment.Remark = Equipment.Remark;
                newEquipment.LastUpdated = DateTime.Now;
                newEquipment.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _equipmentService.Update(newEquipment);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Equipments = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEquipment(long ID)
        {
            if (ID != 0)
            {
                Equipment newEquipment = await _equipmentService.GetByID(ID);

                newEquipment.IsDelete = true;
                newEquipment.LastUpdated = DateTime.Now;

                response = await _equipmentService.Update(newEquipment);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Equipments = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllEquipments(bool isDisplayAll)
        {
            List<Equipment> EquipmentList = await _equipmentService.GetAllEquipments();

            if (EquipmentList != null)
            {
                EquipmentList = EquipmentList.OrderBy(x => x.EquipmentName).ToList();
                var data = EquipmentList.Select(x => new { x.EquipmentID, x.EquipmentName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
