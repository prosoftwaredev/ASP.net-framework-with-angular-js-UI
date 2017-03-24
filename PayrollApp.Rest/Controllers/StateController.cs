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
    public class StateController : ApiController
    {
        private readonly IStateService _stateService;
        string response;

        public StateController() { }

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        //[Authorize(States = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStates(FormDataCollection form)
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

            PagedData<State> pagedData = await _stateService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.StateID, x.Country.CountryName, x.StateName, x.StateCode, x.Created, x.IsEnable }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


       //[Authorize(States = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStateByID(int StateID)
        {
            if (StateID <= 0)
                return NotFound();

            State State = await _stateService.GetByID(StateID);

            if (State != null)
            {
                var data = new { State.StateID, State.CountryID, State.StateName, State.StateCode, State.Created, State.IsEnable, State.LastUpdated, State.Remark, State.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

       //[Authorize(States = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateState([FromBody]State State)
        {
            if (State != null)
            {
                response = await _stateService.Create(State);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       //[Authorize(States = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateState([FromBody]State State)
        {
            if (State != null)
            {
                State newState = await _stateService.GetByID(State.StateID);

                newState.CountryID = State.CountryID;
                newState.StateName = State.StateName;
                newState.StateCode = State.StateCode;
                newState.IsEnable = State.IsEnable;
                newState.Remark = State.Remark;
                newState.LastUpdated = DateTime.Now;
                newState.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _stateService.Update(newState);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(States = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteState(int ID)
        {
            if (ID != 0)
            {
                State newState = await _stateService.GetByID(ID);

                newState.IsDelete = true;
                newState.LastUpdated = DateTime.Now;

                response = await _stateService.Update(newState);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(States = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllStates(bool isDisplayAll)
        {
            List<State> StateList = await _stateService.GetAllStates();

            if (StateList != null)
            {
                StateList = StateList.OrderBy(x => x.StateName).ToList();
                var data = StateList.Select(x => new { x.StateID, x.StateName, x.StateCode, x.CountryID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllStatesByCountryID(bool isDisplayAll, long CountryID)
        {
            List<State> StateList = await _stateService.GetAllStatesByCountryID(CountryID);

            if (StateList != null)
            {
                StateList = StateList.OrderBy(x => x.StateName).ToList();
                var data = StateList.Select(x => new { x.StateID, x.StateName, x.StateCode, x.CountryID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
