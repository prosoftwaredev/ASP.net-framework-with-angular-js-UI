using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class PreferenceService : IPreferenceService, IDisposable
    {
        #region Variables

        private readonly IRepository<Preference> _preferenceRepository;
        int response;

        #endregion

        #region _ctor

        public PreferenceService(IRepository<Preference> preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Preference>> Get(SearchDataTable search)
        {
            PagedData<Preference> pageData = new PagedData<Preference>();

            var query = _preferenceRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long PreferenceID = 0, tempPreferenceID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempPreferenceID))
                    PreferenceID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                    Created = Convert.ToDateTime(search.SearchValue);
                else
                        if (search.SearchValue.ToLower() == "yes")
                    isEnable = true;
                else
                            if (search.SearchValue.ToLower() == "no")
                    isEnable = false;

                query = query.Where(x => x.PreferenceID == PreferenceID ||
                    x.PreferenceName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "PreferenceID":
                            query = query.OrderBy(x => x.PreferenceID);
                            break;

                        case "PreferenceName":
                            query = query.OrderBy(x => x.PreferenceName);
                            break;

                        default:
                            query = query.OrderBy(x => x.PreferenceID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "PreferenceID":
                            query = query.OrderByDescending(x => x.PreferenceID);
                            break;

                        case "PreferenceName":
                            query = query.OrderByDescending(x => x.PreferenceName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.PreferenceID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Preference> GetByID(long PreferenceID)
        {
            var query = await _preferenceRepository.GetByIdAsync(PreferenceID); ;
            return query;
        }

        public async Task<string> Create(Preference Preference)
        {
            response = await _preferenceRepository.InsertAsync(Preference);
            if (response == 1)
                return Preference.PreferenceID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Preference Preference)
        {
            response = await _preferenceRepository.UpdateAsync(Preference);
            if (response == 1)
                return Preference.PreferenceID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Preference>> GetAllPreferences(bool displayAll = false, bool isDelete = false)
        {
            List<Preference> PreferenceList = new List<Preference>();

            var query = _preferenceRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                PreferenceList = await query.ToListAsync();
            else
                PreferenceList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return PreferenceList;
        }
        #endregion
    }
}
