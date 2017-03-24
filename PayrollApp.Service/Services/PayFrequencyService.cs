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
    public class PayFrequencyService : IPayFrequencyService, IDisposable
    {
        #region Variables

        private readonly IRepository<PayFrequency> _payFrequencyRepository;
        int response;

        #endregion

        #region _ctor

        public PayFrequencyService(IRepository<PayFrequency> payFrequencyRepository)
        {
            _payFrequencyRepository = payFrequencyRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<PayFrequency>> Get(SearchDataTable search)
        {
            PagedData<PayFrequency> pageData = new PagedData<PayFrequency>();

            var query = _payFrequencyRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long PayFrequencyID = 0, tempPayFrequencyID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempPayFrequencyID))
                    PayFrequencyID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.PayFrequencyID == PayFrequencyID ||
                    x.PayFrequencyName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "PayFrequencyID":
                            query = query.OrderBy(x => x.PayFrequencyID);
                            break;

                        case "PayFrequencyName":
                            query = query.OrderBy(x => x.PayFrequencyName);
                            break;

                        default:
                            query = query.OrderBy(x => x.PayFrequencyID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "PayFrequencyID":
                            query = query.OrderByDescending(x => x.PayFrequencyID);
                            break;

                        case "PayFrequencyName":
                            query = query.OrderByDescending(x => x.PayFrequencyName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.PayFrequencyID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<PayFrequency> GetByID(long PayFrequencyID)
        {
            var query = await _payFrequencyRepository.GetByIdAsync(PayFrequencyID); ;
            return query;
        }

        public async Task<string> Create(PayFrequency PayFrequency)
        {
            response = await _payFrequencyRepository.InsertAsync(PayFrequency);
            if (response == 1)
                return PayFrequency.PayFrequencyID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(PayFrequency PayFrequency)
        {
            response = await _payFrequencyRepository.UpdateAsync(PayFrequency);
            if (response == 1)
                return PayFrequency.PayFrequencyID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<PayFrequency>> GetAllPayFrequencies(bool displayAll = false, bool isDelete = false)
        {
            List<PayFrequency> PayFrequencyList = new List<PayFrequency>();

            var query = _payFrequencyRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                PayFrequencyList = await query.ToListAsync();
            else
                PayFrequencyList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return PayFrequencyList;
        }
        #endregion
    }
}
