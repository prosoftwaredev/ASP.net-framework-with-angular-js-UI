using PayrollApp.Core.Data.Entities;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class EmployeeNoteService : IEmployeeNoteService, IDisposable
    {
        #region Variables

        private readonly IRepository<EmployeeNote> _employeeNoteRepository;
        int response;

        #endregion

        #region _ctor

        public EmployeeNoteService(IRepository<EmployeeNote> employeeNoteRepository)
        {
            _employeeNoteRepository = employeeNoteRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<EmployeeNote> GetByID(long EmployeeNoteID)
        {
            var query = await _employeeNoteRepository.GetByIdAsync(EmployeeNoteID); ;
            return query;
        }

        public async Task<List<EmployeeNote>> GetAllEmployeeNotesByEmployeeID(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeNote> EmployeeNoteList = new List<EmployeeNote>();

            var query = _employeeNoteRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            query = query.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeNoteList = await query.ToListAsync();
            else
                EmployeeNoteList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EmployeeNoteList;
        }

        public async Task<string> Create(EmployeeNote EmployeeNote)
        {
            response = await _employeeNoteRepository.InsertAsync(EmployeeNote);
            if (response == 1)
                return EmployeeNote.EmployeeNoteID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(EmployeeNote EmployeeNote)
        {
            response = await _employeeNoteRepository.UpdateAsync(EmployeeNote);
            if (response == 1)
                return EmployeeNote.EmployeeNoteID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
