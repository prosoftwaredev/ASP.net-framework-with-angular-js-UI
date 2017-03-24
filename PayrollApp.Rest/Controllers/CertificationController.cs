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
    public class CertificationController : ApiController
    {
         private readonly ICertificationService _certificationService;
        string response;

        public CertificationController() { }

        public CertificationController(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        //[Authorize(Certifications = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCertifications(FormDataCollection form)
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

            PagedData<Certification> pagedData = await _certificationService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.CertificationID, x.CertificationName,  x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(Certifications = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCertificationByID(long CertificationID)
        {
            if (CertificationID <= 0)
                return NotFound();

            Certification Certification = await _certificationService.GetByID(CertificationID);

            if (Certification != null)
            {
                var data = new { Certification.CertificationID, Certification.CertificationName,  Certification.Created, Certification.IsEnable, Certification.LastUpdated, Certification.Remark, Certification.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(Certifications = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCertification([FromBody]Certification Certification)
        {
            if (Certification != null)
            {
                response = await _certificationService.Create(Certification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(Certifications = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCertification([FromBody]Certification Certification)
        {
            if (Certification != null)
            {
                Certification newCertification = await _certificationService.GetByID(Certification.CertificationID);

                newCertification.CertificationName = Certification.CertificationName;
                newCertification.IsEnable = Certification.IsEnable;
                newCertification.Remark = Certification.Remark;
                newCertification.LastUpdated = DateTime.Now;
                newCertification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _certificationService.Update(newCertification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Certifications = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCertification(long ID)
        {
            if (ID != 0)
            {
                Certification newCertification = await _certificationService.GetByID(ID);

                newCertification.IsDelete = true;
                newCertification.LastUpdated = DateTime.Now;

                response = await _certificationService.Update(newCertification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Certifications = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCertifications(bool isDisplayAll, long EmployeeID)
        {
            List<Certification> CertificationList = await _certificationService.GetAllCertifications(EmployeeID);

            if (CertificationList != null)
            {
                CertificationList = CertificationList.OrderBy(x => x.CertificationID).ToList();
                var data = CertificationList.Select(x => new { x.CertificationID, x.CertificationName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Certifications = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCertifications(bool isDisplayAll)
        {
            List<Certification> CertificationList = await _certificationService.GetAllCertifications();

            if (CertificationList != null)
            {
                CertificationList = CertificationList.OrderBy(x => x.CertificationName).ToList();
                var data = CertificationList.Select(x => new { x.CertificationID, x.CertificationName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
