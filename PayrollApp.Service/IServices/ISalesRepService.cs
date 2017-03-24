using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ISalesRepService
    {
        //Task<PagedData<SalesRep>> Get(int PageIndex, int PageSize);
        Task<PagedData<SalesRep>> Get(SearchDataTable search);
        Task<SalesRep> GetByID(long SalesRepID);
        Task<string> Create(SalesRep SalesRep);
        Task<string> Update(SalesRep SalesRep);

        Task<List<SalesRep>> GetAllSalesReps(bool displayAll = false, bool isDelete = false);
    }
}
