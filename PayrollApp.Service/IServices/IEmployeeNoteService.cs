using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface IEmployeeNoteService
    {
        Task<EmployeeNote> GetByID(long CityID);
        Task<string> Create(EmployeeNote EmployeeNote);
        Task<string> Update(EmployeeNote EmployeeNote);
        Task<List<EmployeeNote>> GetAllEmployeeNotesByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
