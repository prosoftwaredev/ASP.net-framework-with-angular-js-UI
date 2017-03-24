using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEquipmentService
    {
        Task<PagedData<Equipment>> Get(SearchDataTable search);
        Task<Equipment> GetByID(long EquipmentID);
        Task<string> Create(Equipment Equipment);
        Task<string> Update(Equipment Equipment);

        Task<List<Equipment>> GetAllEquipments(bool displayAll = false, bool isDelete = false);
    }
}
