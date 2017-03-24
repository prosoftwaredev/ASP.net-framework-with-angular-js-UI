using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IPaymentTermService
    {
        //Task<PagedData<PaymentTerm>> Get(int PageIndex, int PageSize);
        Task<PagedData<PaymentTerm>> Get(SearchDataTable search);
        Task<PaymentTerm> GetByID(long PaymentTermID);
        Task<string> Create(PaymentTerm PaymentTerm);
        Task<string> Update(PaymentTerm PaymentTerm);

        Task<List<PaymentTerm>> GetAllPaymentTerms(bool displayAll = false, bool isDelete = false);
    }
}
