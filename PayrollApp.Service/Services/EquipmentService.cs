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
    public class EquipmentService : IEquipmentService, IDisposable
    {
        #region Variables

        private readonly IRepository<Equipment> _equipmentRepository;
        int response;

        #endregion

        #region _ctor

        public EquipmentService(IRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Equipment>> Get(SearchDataTable search)
        {
            PagedData<Equipment> pageData = new PagedData<Equipment>();

            var query = _equipmentRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long EquipmentID = 0, tempEquipmentID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempEquipmentID))
                    EquipmentID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.EquipmentID == EquipmentID ||
                    x.EquipmentName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "EquipmentID":
                            query = query.OrderBy(x => x.EquipmentID);
                            break;

                        case "EquipmentName":
                            query = query.OrderBy(x => x.EquipmentName);
                            break;

                        default:
                            query = query.OrderBy(x => x.EquipmentID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "EquipmentID":
                            query = query.OrderByDescending(x => x.EquipmentID);
                            break;

                        case "EquipmentName":
                            query = query.OrderByDescending(x => x.EquipmentName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.EquipmentID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Equipment> GetByID(long EquipmentID)
        {
            var query = await _equipmentRepository.GetByIdAsync(EquipmentID); ;
            return query;
        }

        public async Task<string> Create(Equipment Equipment)
        {
            response = await _equipmentRepository.InsertAsync(Equipment);
            if (response == 1)
                return Equipment.EquipmentID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Equipment Equipment)
        {
            response = await _equipmentRepository.UpdateAsync(Equipment);
            if (response == 1)
                return Equipment.EquipmentID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Equipment>> GetAllEquipments(bool displayAll = false, bool isDelete = false)
        {
            List<Equipment> EquipmentList = new List<Equipment>();

            var query = _equipmentRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                EquipmentList = await query.ToListAsync();
            else
                EquipmentList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return EquipmentList;
        }
        #endregion
    }
}
