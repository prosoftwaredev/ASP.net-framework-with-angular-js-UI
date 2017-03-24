using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ITitleService
    {
        Task<PagedData<Title>> Get(SearchDataTable search);
        Task<Title> GetByID(long TitleID);
        Task<string> Create(Title Title);
        Task<string> Update(Title Title);

        Task<Title> GetFirstTitle(bool displayAll = false, bool isDelete = false);
        Task<List<Title>> GetAllTitles(bool displayAll = false, bool isDelete = false);
    }
}
