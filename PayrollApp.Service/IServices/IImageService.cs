using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IImageService
    {
        Task<PagedData<Image>> Get(SearchDataTable search);
        Task<Image> GetByID(long ImageID);
        Task<List<Image>> GetAllImages(bool displayAll = false, bool isDelete = false);

    }
}
