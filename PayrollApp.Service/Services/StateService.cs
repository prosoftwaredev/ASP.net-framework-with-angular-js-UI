using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class StateService : IStateService, IDisposable
    {
        #region Variables

        private readonly IRepository<State> _stateRepository;
        int response;

        #endregion

        #region _ctor

        public StateService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<State>> Get(SearchDataTable search)
        {
            PagedData<State> pageData = new PagedData<State>();

            var query = _stateRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long StateID = 0, tempStateID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempStateID))
                    StateID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.StateID == StateID ||
                    x.StateName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Country.CountryName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "StateID":
                            query = query.OrderBy(x => x.StateID);
                            break;

                        case "StateName":
                            query = query.OrderBy(x => x.StateName);
                            break;

                        default:
                            query = query.OrderBy(x => x.StateID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "StateID":
                            query = query.OrderByDescending(x => x.StateID);
                            break;

                        case "StateName":
                            query = query.OrderByDescending(x => x.StateName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.StateID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<State> GetByID(long StateID)
        {
            var query = await _stateRepository.GetByIdAsync(StateID); ;
            return query;
        }

        public async Task<string> Create(State State)
        {
            response = await _stateRepository.InsertAsync(State);
            if (response == 1)
                return State.StateID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(State State)
        {
            response = await _stateRepository.UpdateAsync(State);
            if (response == 1)
                return State.StateID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<State>> GetAllStates(bool displayAll = false, bool isDelete = false)
        {
            List<State> StateList = new List<State>();

            var query = _stateRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                StateList = await query.ToListAsync();
            else
                StateList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return StateList;
        }

        public async Task<List<State>> GetAllStatesByCountryID(long CountryID, bool displayAll = false, bool isDelete = false)
        {
            List<State> StateList = new List<State>();

            var query = _stateRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.CountryID == CountryID);

            if (displayAll)
                StateList = await query.ToListAsync();
            else
                StateList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return StateList;
        }

        #endregion
    }
}
