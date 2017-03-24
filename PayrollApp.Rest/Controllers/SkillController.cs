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
    public class SkillController : ApiController
    {
          private readonly ISkillService _skillService;
        string response;

        public SkillController() { }

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        //[Authorize(Skills = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSkills(FormDataCollection form)
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

            PagedData<Skill> pagedData = await _skillService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.SkillID, x.SkillName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(Skills = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSkillByID(long SkillID)
        {
            if (SkillID <= 0)
                return NotFound();

            Skill Skill = await _skillService.GetByID(SkillID);

            if (Skill != null)
            {
                var data = new { Skill.SkillID, Skill.SkillName, Skill.Created, Skill.IsEnable, Skill.LastUpdated, Skill.Remark, Skill.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Skills = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateSkill([FromBody]Skill Skill)
        {
            if (Skill != null)
            {
                response = await _skillService.Create(Skill);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Skills = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateSkill([FromBody]Skill Skill)
        {
            if (Skill != null)
            {
                Skill newSkill = await _skillService.GetByID(Skill.SkillID);

                newSkill.SkillName = Skill.SkillName;
                newSkill.IsEnable = Skill.IsEnable;
                newSkill.Remark = Skill.Remark;
                newSkill.LastUpdated = DateTime.Now;
                newSkill.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _skillService.Update(newSkill);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Skills = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteSkill(long ID)
        {
            if (ID != 0)
            {
                Skill newSkill = await _skillService.GetByID(ID);

                newSkill.IsDelete = true;
                newSkill.LastUpdated = DateTime.Now;

                response = await _skillService.Update(newSkill);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Skills = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllSkills(bool isDisplayAll)
        {
            List<Skill> SkillList = await _skillService.GetAllSkills();

            if (SkillList != null)
            {
                SkillList = SkillList.OrderBy(x => x.SkillName).ToList();
                var data = SkillList.Select(x => new { x.SkillID, x.SkillName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Skills = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllSkills(bool isDisplayAll, long EmployeeID)
        {
            List<Skill> SkillList = await _skillService.GetAllSkills(EmployeeID);

            if (SkillList != null)
            {
                SkillList = SkillList.OrderBy(x => x.SkillID).ToList();
                var data = SkillList.Select(x => new { x.SkillID, x.SkillName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

    }
}
