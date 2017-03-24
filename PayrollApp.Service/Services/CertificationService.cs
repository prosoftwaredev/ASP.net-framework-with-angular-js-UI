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
    public class CertificationService : ICertificationService, IDisposable
    {
        #region Variables

        private readonly IRepository<Certification> _certificationRepository;
        private readonly IRepository<EmployeeCertification> _employeeCertificationRepository;
        int response;

        #endregion

        #region _ctor

        public CertificationService(IRepository<Certification> certificationRepository, IRepository<EmployeeCertification> employeeCertificationRepository)
        {
            _certificationRepository = certificationRepository;
            _employeeCertificationRepository = employeeCertificationRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD


        public async Task<PagedData<Certification>> Get(SearchDataTable search)
        {
            PagedData<Certification> pageData = new PagedData<Certification>();

            var query = _certificationRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long CertificationID = 0, tempCertificationID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempCertificationID))
                    CertificationID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.CertificationID == CertificationID ||
                    x.CertificationName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "CertificationID":
                            query = query.OrderBy(x => x.CertificationID);
                            break;

                        case "CertificationName":
                            query = query.OrderBy(x => x.CertificationName);
                            break;

                        default:
                            query = query.OrderBy(x => x.CertificationID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "CertificationID":
                            query = query.OrderByDescending(x => x.CertificationID);
                            break;

                        case "CertificationName":
                            query = query.OrderByDescending(x => x.CertificationName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.CertificationID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Certification> GetByID(long CertificationID)
        {
            var query = await _certificationRepository.GetByIdAsync(CertificationID); ;
            return query;
        }

        public async Task<string> Create(Certification Certification)
        {
            response = await _certificationRepository.InsertAsync(Certification);
            if (response == 1)
                return Certification.CertificationID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(Certification Certification)
        {
            response = await _certificationRepository.UpdateAsync(Certification);
            if (response == 1)
                return Certification.CertificationID.ToString();
            else
                return response.ToString();
        }

        #endregion

        #region Extra

        public async Task<List<Certification>> GetAllCertifications(bool displayAll = false, bool isDelete = false)
        {
            List<Certification> CertificationList = new List<Certification>();

            var query = _certificationRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CertificationList = await query.ToListAsync();
            else
                CertificationList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return CertificationList;
        }

        public async Task<List<Certification>> GetAllCertifications(long EmployeeID, bool displayAll = false, bool isDelete = false)
        {
            List<EmployeeCertification> EmployeeCertificationList = new List<EmployeeCertification>();

            var query1 = _employeeCertificationRepository.Table;

            query1 = query1.Where(x => x.IsDelete == isDelete);

            query1 = query1.Where(x => x.EmployeeID == EmployeeID);

            if (displayAll)
                EmployeeCertificationList = await query1.ToListAsync();
            else
                EmployeeCertificationList = await query1.Where(x => x.IsEnable == true).ToListAsync();

            List<long> CertificationIDList = EmployeeCertificationList.Select(x => x.CertificationID).ToList();

            List<Certification> CertificationList = new List<Certification>();

            var query2 = _certificationRepository.Table;

            //List<long> CertificationList = new List<long>();
            //if (displayAll)
            //var CertificationList = query2.Select(x => x.EmployeeCertifications.Where(z => z.EmployeeID == EmployeeID && z.IsDelete == isDelete).Select(y => y.CertificationID).ToList()).ToList().ToList();

            query2 = query2.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                CertificationList = await query2.ToListAsync();
            else
                CertificationList = await query2.Where(x => x.IsEnable == true).ToListAsync();

            var newList = CertificationList.Where(x => !CertificationIDList.Contains(x.CertificationID)).ToList();

            return newList;
        }

        #endregion
    }
}
