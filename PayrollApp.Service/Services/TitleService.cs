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
    public class TitleService : ITitleService, IDisposable
    {
        #region Variables

        private readonly IRepository<Title> _titleRepository;
        int response;

        #endregion

        #region _ctor

        public TitleService(IRepository<Title> titleRepository)
        {
            _titleRepository = titleRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Title>> Get(SearchDataTable search)
        {
            PagedData<Title> pageData = new PagedData<Title>();

            var query = _titleRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long TitleID = 0, tempTitleID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempTitleID))
                    TitleID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.TitleID == TitleID ||
                    x.TitleName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Gender.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "TitleID":
                            query = query.OrderBy(x => x.TitleID);
                            break;

                        case "TitleName":
                            query = query.OrderBy(x => x.TitleName);
                            break;

                        default:
                            query = query.OrderBy(x => x.TitleID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "TitleID":
                            query = query.OrderByDescending(x => x.TitleID);
                            break;

                        case "TitleName":
                            query = query.OrderByDescending(x => x.TitleName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.TitleID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Title> GetByID(long TitleID)
        {
            var query = await _titleRepository.GetByIdAsync(TitleID); ;
            return query;
        }

        public async Task<string> Create(Title Title)
        {
            response = await _titleRepository.InsertAsync(Title);
            if (response == 1)
                return Title.TitleID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Title Title)
        {
            response = await _titleRepository.UpdateAsync(Title);
            if (response == 1)
                return Title.TitleID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<Title> GetFirstTitle(bool displayAll = false, bool isDelete = false)
        {
            Title Title = new Title();

            var query = _titleRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                Title = await query.FirstOrDefaultAsync();
            else
                Title = await query.Where(x => x.IsEnable == true).FirstOrDefaultAsync();

            return Title;
        }

        public async Task<List<Title>> GetAllTitles(bool displayAll = false, bool isDelete = false)
        {
            List<Title> TitleList = new List<Title>();

            var query = _titleRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                TitleList = await query.ToListAsync();
            else
                TitleList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return TitleList;
        }
        #endregion
    }
}
