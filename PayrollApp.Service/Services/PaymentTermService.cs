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
    public class PaymentTermService : IPaymentTermService, IDisposable
    {
        #region Variables

        private readonly IRepository<PaymentTerm> _paymentTermRepository;
        int response;

        #endregion

        #region _ctor

        public PaymentTermService(IRepository<PaymentTerm> paymentTermRepository)
        {
            _paymentTermRepository = paymentTermRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<PaymentTerm>> Get(SearchDataTable search)
        {
            PagedData<PaymentTerm> pageData = new PagedData<PaymentTerm>();

            var query = _paymentTermRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long PaymentTermID = 0, tempPaymentTermID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempPaymentTermID))
                    PaymentTermID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.PaymentTermID == PaymentTermID ||
                    x.PaymentTermName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "PaymentTermID":
                            query = query.OrderBy(x => x.PaymentTermID);
                            break;

                        case "PaymentTermName":
                            query = query.OrderBy(x => x.PaymentTermName);
                            break;

                        default:
                            query = query.OrderBy(x => x.PaymentTermID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "PaymentTermID":
                            query = query.OrderByDescending(x => x.PaymentTermID);
                            break;

                        case "PaymentTermName":
                            query = query.OrderByDescending(x => x.PaymentTermName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.PaymentTermID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<PaymentTerm> GetByID(long PaymentTermID)
        {
            var query = await _paymentTermRepository.GetByIdAsync(PaymentTermID); ;
            return query;
        }

        public async Task<string> Create(PaymentTerm PaymentTerm)
        {
            response = await _paymentTermRepository.InsertAsync(PaymentTerm);
            if (response == 1)
                return PaymentTerm.PaymentTermID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(PaymentTerm PaymentTerm)
        {
            response = await _paymentTermRepository.UpdateAsync(PaymentTerm);
            if (response == 1)
                return PaymentTerm.PaymentTermID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<PaymentTerm>> GetAllPaymentTerms(bool displayAll = false, bool isDelete = false)
        {
            List<PaymentTerm> PaymentTermList = new List<PaymentTerm>();

            var query = _paymentTermRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                PaymentTermList = await query.ToListAsync();
            else
                PaymentTermList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return PaymentTermList;
        }
        #endregion
    }
}
