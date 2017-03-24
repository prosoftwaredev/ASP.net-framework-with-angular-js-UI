using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollApp.Service.IServices
{
    public interface ILabourClassificationService
    {
        Task<PagedData<LabourClassification>> Get(SearchDataTable search);
        Task<LabourClassification> GetByID(long LabourClassificationID);
        Task<string> Create(LabourClassification LabourClassification);
        Task<string> Update(LabourClassification LabourClassification);

        Task<List<LabourClassification>> GetAllLabourClassifications(bool displayAll = false, bool isDelete = false);
    }
}
