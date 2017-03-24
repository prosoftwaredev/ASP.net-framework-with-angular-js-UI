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
    public class SalesRepService : ISalesRepService, IDisposable
    {
        #region Variables

        private readonly IRepository<SalesRep> _salesRepRepository;
        int response;

        #endregion

        #region _ctor

        public SalesRepService(IRepository<SalesRep> salesRepRepository)
        {
            _salesRepRepository = salesRepRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<SalesRep>> Get(SearchDataTable search)
        {
            PagedData<SalesRep> pageData = new PagedData<SalesRep>();

            var query = _salesRepRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long SalesRepID = 0, tempSalesRepID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempSalesRepID))
                    SalesRepID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.SalesRepID == SalesRepID ||
                    x.SalesRepName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "SalesRepID":
                            query = query.OrderBy(x => x.SalesRepID);
                            break;

                        case "SalesRepName":
                            query = query.OrderBy(x => x.SalesRepName);
                            break;

                        default:
                            query = query.OrderBy(x => x.SalesRepID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "SalesRepID":
                            query = query.OrderByDescending(x => x.SalesRepID);
                            break;

                        case "SalesRepName":
                            query = query.OrderByDescending(x => x.SalesRepName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.SalesRepID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<SalesRep> GetByID(long SalesRepID)
        {
            var query = await _salesRepRepository.GetByIdAsync(SalesRepID); ;
            return query;
        }

        public async Task<string> Create(SalesRep SalesRep)
        {
            response = await _salesRepRepository.InsertAsync(SalesRep);
            if (response == 1)
                return SalesRep.SalesRepID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(SalesRep SalesRep)
        {
            response = await _salesRepRepository.UpdateAsync(SalesRep);
            if (response == 1)
                return SalesRep.SalesRepID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<SalesRep>> GetAllSalesReps(bool displayAll = false, bool isDelete = false)
        {
            List<SalesRep> SalesRepList = new List<SalesRep>();

            var query = _salesRepRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                SalesRepList = await query.ToListAsync();
            else
                SalesRepList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return SalesRepList;
        }
        #endregion
    }
}
