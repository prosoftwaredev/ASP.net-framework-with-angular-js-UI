using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class ExcLoggerController : ApiController
    {
        private readonly IExcLoggerService _ExcLoggerService;
        string response;

        public ExcLoggerController() { }

        public ExcLoggerController(IExcLoggerService ExcLoggerService)
        {
            _ExcLoggerService = ExcLoggerService;
        }

        //[Authorize(ExcLoggers = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetExcLoggers(FormDataCollection form)
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

            PagedData<ExcLogger> pagedData = await _ExcLoggerService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.ExcLoggerID, x.Message, x.Controller, x.Action, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(ExcLoggers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetExcLoggerByID(int ExcLoggerID)
        {
            if (ExcLoggerID <= 0)
                return NotFound();

            ExcLogger ExcLogger = await _ExcLoggerService.GetByID(ExcLoggerID);

            if (ExcLogger != null)
            {
                var data = new { ExcLogger.ExcLoggerID, ExcLogger.Message, ExcLogger.Source, ExcLogger.HResult, ExcLogger.StackTrace, ExcLogger.InnerException, ExcLogger.Controller, ExcLogger.Action, ExcLogger.Created, ExcLogger.IsEnable, ExcLogger.LastUpdated, ExcLogger.Remark, ExcLogger.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(ExcLoggers = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateExcLogger([FromBody]ExcLogger ExcLogger)
        {
            if (ExcLogger != null)
            {
                response = await _ExcLoggerService.Create(ExcLogger);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(ExcLoggers = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateExcLogger([FromBody]ExcLogger ExcLogger)
        {
            if (ExcLogger != null)
            {
                ExcLogger newExcLogger = await _ExcLoggerService.GetByID(ExcLogger.ExcLoggerID);

                newExcLogger.Message = ExcLogger.Message;
                newExcLogger.IsEnable = ExcLogger.IsEnable;
                newExcLogger.Remark = ExcLogger.Remark;
                newExcLogger.LastUpdated = DateTime.Now;
                newExcLogger.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _ExcLoggerService.Update(newExcLogger);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(ExcLoggers = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteExcLogger(int ID)
        {
            if (ID != 0)
            {
                ExcLogger newExcLogger = await _ExcLoggerService.GetByID(ID);

                newExcLogger.IsDelete = true;
                newExcLogger.LastUpdated = DateTime.Now;

                response = await _ExcLoggerService.Update(newExcLogger);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
