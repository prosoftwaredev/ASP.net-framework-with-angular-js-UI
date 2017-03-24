using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ICertificationService
    {
        Task<PagedData<Certification>> Get(SearchDataTable search);
        Task<Certification> GetByID(long CertificationID);
        Task<string> Create(Certification Certification);
        Task<string> Update(Certification Certification);

        Task<List<Certification>> GetAllCertifications(bool displayAll = false, bool isDelete = false);
        Task<List<Certification>> GetAllCertifications(long EmployeeID, bool displayAll = false, bool isDelete = false);
    }
}
