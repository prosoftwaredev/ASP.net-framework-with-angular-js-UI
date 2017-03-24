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
    public class LabourClassificationService : ILabourClassificationService, IDisposable
    {
        #region Variables

        private readonly IRepository<LabourClassification> _labourClassificationRepository;
        int response;

        #endregion

        #region _ctor

        public LabourClassificationService(IRepository<LabourClassification> labourClassificationRepository)
        {
            _labourClassificationRepository = labourClassificationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<LabourClassification>> Get(SearchDataTable search)
        {
            PagedData<LabourClassification> pageData = new PagedData<LabourClassification>();

            var query = _labourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long LabourClassificationID = 0, tempLabourClassificationID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempLabourClassificationID))
                    LabourClassificationID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.LabourClassificationID == LabourClassificationID ||
                    x.LabourClassificationName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "LabourClassificationID":
                            query = query.OrderBy(x => x.LabourClassificationID);
                            break;

                        case "LabourClassificationName":
                            query = query.OrderBy(x => x.LabourClassificationName);
                            break;

                        default:
                            query = query.OrderBy(x => x.LabourClassificationID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "LabourClassificationID":
                            query = query.OrderByDescending(x => x.LabourClassificationID);
                            break;

                        case "LabourClassificationName":
                            query = query.OrderByDescending(x => x.LabourClassificationName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.LabourClassificationID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<LabourClassification> GetByID(long LabourClassificationID)
        {
            var query = await _labourClassificationRepository.GetByIdAsync(LabourClassificationID); ;
            return query;
        }

        public async Task<string> Create(LabourClassification LabourClassification)
        {
            response = await _labourClassificationRepository.InsertAsync(LabourClassification);
            if (response == 1)
                return LabourClassification.LabourClassificationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(LabourClassification LabourClassification)
        {
            response = await _labourClassificationRepository.UpdateAsync(LabourClassification);
            if (response == 1)
                return LabourClassification.LabourClassificationID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<LabourClassification>> GetAllLabourClassifications(bool displayAll = false, bool isDelete = false)
        {
            List<LabourClassification> LabourClassificationList = new List<LabourClassification>();

            var query = _labourClassificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                LabourClassificationList = await query.ToListAsync();
            else
                LabourClassificationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return LabourClassificationList;
        }
        #endregion
    }
}
