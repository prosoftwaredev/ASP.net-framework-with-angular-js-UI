using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeNoteService _employeeNoteService;
        private readonly IEmployeeLabourClassificationService _employeeLabourClassificationService;
        private readonly IEmployeeBlacklistService _employeeBlacklistService;
        private readonly IEmployeeCertificationService _employeeCertificationService;
        private readonly IEmployeeSkillService _employeeSkillService;
        private readonly ITitleService _titleService;
        private readonly IUserService _userService;
        string response;

        public EmployeeController() { }

        public EmployeeController(IEmployeeService employeeService, IEmployeeNoteService employeeNoteService, IEmployeeLabourClassificationService employeeLabourClassificationService, IEmployeeBlacklistService employeeBlacklistService, IEmployeeCertificationService employeeCertificationService, ITitleService titleService, IEmployeeSkillService employeeSkillService, IUserService userService)
        {
            _employeeService = employeeService;
            _employeeNoteService = employeeNoteService;
            _employeeLabourClassificationService = employeeLabourClassificationService;
            _employeeBlacklistService = employeeBlacklistService;
            _employeeCertificationService = employeeCertificationService;
            _employeeSkillService = employeeSkillService;
            _titleService = titleService;
            _userService = userService;
            UserHelper.UserService = _userService;
        }

        #region Employee
        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEmployeess(FormDataCollection form)
        {
            var draw = form.GetValues("draw").FirstOrDefault();
            var start = form.GetValues("start").FirstOrDefault();
            var length = form.GetValues("length").FirstOrDefault();
            var sortColumn = form.GetValues("columns[" + form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            SearchDataTable search = new SearchDataTable
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchValue = searchValue,
                RecordsTotal = recordsTotal
            };

            PagedData<Employee> pagedData = await _employeeService.Get(search);

            if (pagedData != null)
            {
                //var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.EmployeeID, x.FirstName, x.LastName, EmployeeTypeName= x.EmployeeType.EmployeeTypeName, EmailAddr = x.EmailMain, HomePhone = x.Phone, x.SIN, x.RefNumber, x.NACList, x.Notes, x.Created, x.IsEnable, x.Remark }) };
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.EmployeeID, x.PrintName, x.EmailMain, x.Phone, x.SIN, x.AccountNo, x.Created, x.IsEnable, x.IsNeverUse, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetEmployees(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string skillId = (Convert.ToString(form.Get("SkillID")) == "") ? null : Convert.ToString(form.Get("SkillID"));
            string certificationId = (Convert.ToString(form.Get("CertificationID")) == "") ? null : Convert.ToString(form.Get("CertificationID"));

            long SkillID = skillId == null ? 0 : Convert.ToInt64(skillId);
            long CertificationID = certificationId == null ? 0 : Convert.ToInt64(certificationId);

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            SearchEmployee search = new SearchEmployee
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SkillID = SkillID,
                CertificationID = CertificationID
            };

            PagedData<Employee> PagedData = await _employeeService.Get(search);

            if (PagedData != null)
            {
                var data = new { Items = PagedData.Items.Select(x => new { x.EmployeeID, PrintName = x.LastName + " " + x.FirstName + " " + x.MiddleName, x.EmailMain, x.Phone, x.SIN, x.AccountNo, x.Created, x.IsEnable, x.IsNeverUse, NACList = string.Join(", ", x.EmployeeBlacklists.Select(y => y.Customer.CompanyName).ToList()), Notes = string.Join(", ", x.EmployeeNotes.Select(y => y.Note).ToList()) }), Count = PagedData.Count };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Employees = "1")]

        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeByID(int EmployeeID)
        {
            if (EmployeeID <= 0)
                return NotFound();

            Employee Employee = await _employeeService.GetByID(EmployeeID);

            Title title = await _titleService.GetByID(Employee.TitleID);

            if (Employee != null)
            {
                var data = new { Employee.EmployeeID, Employee.City.State.CountryID, Employee.City.StateID, Employee.CityID, Employee.PayFrequencyID, Employee.TitleID, Employee.FirstName, Employee.MiddleName, Employee.LastName, Employee.AccountNo, Employee.EmailMain, Employee.EmailCC, Employee.Website, Employee.Other, Employee.Phone, Employee.Mobile, Employee.Fax, Employee.Address1, Employee.Address2, Employee.PostalCode, Employee.NextOfKin, Employee.NextOfKinContact, Employee.SIN, Employee.DOB, Employee.Balance, Employee.Withholding, Employee.Dormant, Employee.PayStubs, Employee.Garnishee, Country = Employee.City.State.Country.CountryName, State = Employee.City.State.StateName, City = Employee.City.CityName, Title = title.TitleName, Gender = title.Gender, PayFrequency = Employee.PayFrequency == null ? "NA" : Employee.PayFrequency.PayFrequencyName, Created = Employee.Created.Value.ToString("dd-MMM-yyyy hh:mm:ss tt"), Employee.IsEnable, LastUpdated = Employee.LastUpdated.HasValue ? Employee.LastUpdated.Value.ToString("dd-MMM-yyyy hh:mm:ss tt") : "", Employee.Remark, Employee.SortOrder, CreatedByName = await UserHelper.GetUserName(Employee.CreatedBy), LastUpdatedByName = await UserHelper.GetUserName(Employee.LastUpdatedBy), Employee.IsNeverUse }; //Employee.PrintName,
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployee([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                string strRefInitial = Employee.LastName.Substring(0, Employee.LastName.Length < 3 ? Employee.LastName.Length : 3);

                string strNewAccountNo = string.Empty;

                string strAccountNo = await _employeeService.GetAccountNumber(strRefInitial);

                if (!string.IsNullOrEmpty(strAccountNo))
                {
                    var alphas = from c in strAccountNo
                                 where !Char.IsDigit(c)
                                 select c;

                    var digits = from c in strAccountNo
                                 where Char.IsDigit(c)
                                 select c;


                    string strFirstThree = strAccountNo.Substring(0, alphas.Count() < 3 ? alphas.Count() : 3);
                    string strLastThree = strAccountNo.Substring(alphas.Count() < 3 ? alphas.Count() : 3, digits.Count() < 3 ? alphas.Count() : 3);


                    if (strLastThree.Length <= 3)
                    {
                        int intLastThree = Convert.ToInt32(strLastThree);
                        int decimalLength = 0;

                        if (intLastThree <= 9)
                            decimalLength = intLastThree.ToString("D").Length + 2;
                        else
                            if (intLastThree <= 99)
                                decimalLength = intLastThree.ToString("D").Length + 1;

                        intLastThree++;

                        string strNewLastThree = intLastThree.ToString("D" + decimalLength.ToString());

                        strNewAccountNo = strFirstThree + strNewLastThree;
                    }
                    else
                    {
                        int intLastThree = Convert.ToInt32(strLastThree);
                        intLastThree++;
                        strNewAccountNo = strFirstThree + intLastThree;
                    }
                }
                else
                {
                    strNewAccountNo = Employee.LastName.Substring(0, Employee.LastName.Length < 3 ? Employee.LastName.Length : 3) + "001";
                }

                Employee.AccountNo = strNewAccountNo.ToUpper();

                Title title = await _titleService.GetFirstTitle();

                if (Employee.TitleID == 0)
                    Employee.TitleID = title.TitleID;


                //bool isSinPresent = await _employeeService.GetSINNumber(Employee.SIN);
                bool isSinPresent = false;
                if (isSinPresent)
                {
                    return BadRequest("SIN number already present in database. Please enter any other SIN");
                }
                else
                {
                    Employee.XmlNote = XmlNote(Employee, true);

                    response = await _employeeService.Create(Employee);
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployee([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                Employee newEmployee = await _employeeService.GetByID(Employee.EmployeeID);

                if (newEmployee.SIN != Employee.SIN)
                    return BadRequest("You can not change the SIN");

                newEmployee.TitleID = Employee.TitleID;
                newEmployee.FirstName = Employee.FirstName;
                newEmployee.MiddleName = Employee.MiddleName;
                newEmployee.LastName = Employee.LastName;
                newEmployee.PrintName = Employee.PrintName;
                newEmployee.Gender = Employee.Gender;
                newEmployee.DOB = Employee.DOB;
                newEmployee.Phone = Employee.Phone;
                newEmployee.Mobile = Employee.Mobile;
                newEmployee.Fax = Employee.Fax;
                newEmployee.EmailMain = Employee.EmailMain;
                newEmployee.EmailCC = Employee.EmailCC;
                newEmployee.Other = Employee.Other;
                newEmployee.Address1 = Employee.Address1;
                newEmployee.Address2 = Employee.Address2;
                newEmployee.CityID = Employee.CityID;
                newEmployee.PostalCode = Employee.PostalCode;
                newEmployee.IsEnable = Employee.IsEnable;
                newEmployee.IsNeverUse = Employee.IsNeverUse;
                newEmployee.Remark = Employee.Remark;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;
                //added code snippet to update model values in single event.
                if (Employee.PayStubs != null)
                {
                    newEmployee.Balance = Employee.Balance;
                    newEmployee.Withholding = Employee.Withholding;
                    newEmployee.Dormant = Employee.Dormant;
                    newEmployee.PayStubs = Employee.PayStubs;
                    newEmployee.PayFrequencyID = Employee.PayFrequencyID;
                    newEmployee.Garnishee = Employee.Garnishee;
                    newEmployee.IsEnable = Employee.IsEnable;
                    newEmployee.Remark = Employee.Remark;
                }

                newEmployee.XmlNote = XmlNote(newEmployee, false);

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployeeIsEnable([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                Employee newEmployee = await _employeeService.GetByID(Employee.EmployeeID);

                newEmployee.IsEnable = Employee.IsEnable;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployeeIsNeverUse([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                Employee newEmployee = await _employeeService.GetByID(Employee.EmployeeID);

                newEmployee.IsNeverUse = Employee.IsNeverUse;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployeeAndPayroll([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                Employee newEmployee = await _employeeService.GetByID(Employee.EmployeeID);

                newEmployee.AccountNo = Employee.AccountNo;
                //newEmployee.SIN = Employee.SIN;
                newEmployee.Balance = Employee.Balance;
                newEmployee.Withholding = Employee.Withholding;
                newEmployee.Dormant = Employee.Dormant;
                newEmployee.PayStubs = Employee.PayStubs;
                newEmployee.PayFrequencyID = Employee.PayFrequencyID;
                newEmployee.Garnishee = Employee.Garnishee;
                newEmployee.IsEnable = Employee.IsEnable;
                newEmployee.Remark = Employee.Remark;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateContact([FromBody]Employee Employee)
        {
            if (Employee != null)
            {
                Employee newEmployee = await _employeeService.GetByID(Employee.EmployeeID);

                newEmployee.Phone = Employee.Phone;
                newEmployee.Mobile = Employee.Mobile;
                newEmployee.Fax = Employee.Fax;
                newEmployee.EmailMain = Employee.EmailMain;
                newEmployee.EmailCC = Employee.EmailCC;
                newEmployee.Other = Employee.Other;
                newEmployee.Address1 = Employee.Address1;
                newEmployee.Address2 = Employee.Address2;
                newEmployee.CityID = Employee.CityID;
                newEmployee.PostalCode = Employee.PostalCode;
                newEmployee.IsEnable = Employee.IsEnable;
                newEmployee.Remark = Employee.Remark;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployee(int ID)
        {
            if (ID != 0)
            {
                Employee newEmployee = await _employeeService.GetByID(ID);

                newEmployee.IsDelete = true;
                newEmployee.LastUpdated = DateTime.Now;
                newEmployee.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeService.Update(newEmployee);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Utility

        public string XmlNote(Employee employee, bool isFromCreate)
        {

            string xmlNote = string.Empty;
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ListID", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Note", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Garnishee", employee.Garnishee.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "BL", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ORNT", "");

            return xmlNote;
        }

        #endregion

        #region Notes

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeNote([FromBody]EmployeeNote EmployeeNote)
        {
            if (EmployeeNote != null)
            {
                Employee newEmployee = await _employeeService.GetByID(EmployeeNote.EmployeeID);
                string oldValue = Utility.GetElement(newEmployee.XmlNote, "Note");
                string newValue = Utility.SetElement(newEmployee.XmlNote, "Note", !string.IsNullOrEmpty(oldValue) ? oldValue + ", " + EmployeeNote.Note : EmployeeNote.Note);
                newEmployee.XmlNote = newValue;
                await _employeeService.Update(newEmployee);

                response = await _employeeNoteService.Create(EmployeeNote);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployeeNote(int ID)
        {
            if (ID != 0)
            {
                EmployeeNote newEmployeeNote = await _employeeNoteService.GetByID(ID);

                newEmployeeNote.IsDelete = true;
                newEmployeeNote.LastUpdated = DateTime.Now;
                newEmployeeNote.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeNoteService.Update(newEmployeeNote);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeNotesByEmployeeID(bool isDisplayAll, long EmployeeID)
        {
            List<EmployeeNote> EmployeeNoteList = await _employeeNoteService.GetAllEmployeeNotesByEmployeeID(EmployeeID);

            if (EmployeeNoteList != null)
            {
                EmployeeNoteList = EmployeeNoteList.OrderByDescending(x => x.EmployeeNoteID).ToList();
                foreach (var item in EmployeeNoteList)
                {
                    item.CreatedByName = await UserHelper.GetUserName(item.CreatedBy);
                }
                //var data = EmployeeNoteList.Select(async x => new { x.EmployeeNoteID, x.Note, x.Created, CreatedBy = x.CreatedBy != 0 ? await GetUserName(x.CreatedBy) : null });
                var data = EmployeeNoteList.Select(x => new { x.EmployeeNoteID, x.Note, x.Created, CreatedBy = x.CreatedByName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        #endregion

        #region Labour Classifications

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeLabourClassification([FromBody]EmployeeLabourClassification EmployeeLabourClassification)
        {
            if (EmployeeLabourClassification != null)
            {
                EmployeeLabourClassification newEmployeeLabourClassification = await _employeeLabourClassificationService.GetAllEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(EmployeeLabourClassification.EmployeeID, EmployeeLabourClassification.LabourClassificationID);

                if (newEmployeeLabourClassification == null)
                {
                    response = await _employeeLabourClassificationService.Create(EmployeeLabourClassification);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployeeLabourClassification([FromBody]EmployeeLabourClassification EmployeeLabourClassification)
        {
            if (EmployeeLabourClassification != null)
            {
                EmployeeLabourClassification newEmployeeLabourClassification = await _employeeLabourClassificationService.GetByID(EmployeeLabourClassification.EmployeeLabourClassificationID);

                newEmployeeLabourClassification.Rate = EmployeeLabourClassification.Rate;
                newEmployeeLabourClassification.LastUpdated = DateTime.Now;
                newEmployeeLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeLabourClassificationService.Update(newEmployeeLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteEmployeeLabourClassification([FromBody] EmployeeLabourClassification EmployeeLabourClassification)
        {
            if (EmployeeLabourClassification != null)
            {
                EmployeeLabourClassification newEmployeeLabourClassification = await _employeeLabourClassificationService.GetAllEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(EmployeeLabourClassification.EmployeeID, EmployeeLabourClassification.LabourClassificationID);

                if (newEmployeeLabourClassification != null)
                {
                    newEmployeeLabourClassification.IsDelete = true;
                    newEmployeeLabourClassification.LastUpdated = DateTime.Now;
                    newEmployeeLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _employeeLabourClassificationService.Update(newEmployeeLabourClassification);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmpLabourClassification(int ID)
        {
            if (ID != 0)
            {
                EmployeeLabourClassification newEmployeeLabourClassification = await _employeeLabourClassificationService.GetByID(ID);

                newEmployeeLabourClassification.IsDelete = true;
                newEmployeeLabourClassification.LastUpdated = DateTime.Now;
                newEmployeeLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeLabourClassificationService.Update(newEmployeeLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeLabourClassificationsByEmployeeID(bool isDisplayAll, long EmployeeID)
        {
            List<EmployeeLabourClassification> EmployeeLabourClassificationList = await _employeeLabourClassificationService.GetAllEmployeeLabourClassificationsByEmployeeID(EmployeeID);

            if (EmployeeLabourClassificationList != null)
            {
                EmployeeLabourClassificationList = EmployeeLabourClassificationList.OrderByDescending(x => x.EmployeeLabourClassificationID).ToList();
                var data = EmployeeLabourClassificationList.Select(x => new { EmployeeLabourClassificationID = x.EmployeeLabourClassificationID, x.EmployeeID, LabourClassificationID = x.LabourClassificationID, x.Rate, x.LabourClassification.LabourClassificationName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(bool isDisplayAll, long EmployeeID, long LabourClassificationID)
        {
            EmployeeLabourClassification EmployeeLabourClassification = await _employeeLabourClassificationService.GetAllEmployeeLabourClassificationsByLabourClassificationIDAndEmployeeID(EmployeeID, LabourClassificationID);

            var data = new { Rate = EmployeeLabourClassification == null ? 0 : EmployeeLabourClassification.Rate };
            return Ok(data);
        }

        #endregion

        #region Blacklist

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeBlacklist([FromBody]EmployeeBlacklist EmployeeBlacklist)
        {
            if (EmployeeBlacklist != null)
            {
                EmployeeBlacklist newEmployeeBlacklist = await _employeeBlacklistService.GetAllEmployeeBlacklistsByCustomerIDAndEmployeeID(EmployeeBlacklist.EmployeeID, EmployeeBlacklist.CustomerID);

                if (newEmployeeBlacklist == null)
                {
                    response = await _employeeBlacklistService.Create(EmployeeBlacklist);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteEmployeeBlacklist([FromBody] EmployeeBlacklist EmployeeBlacklist)
        {
            if (EmployeeBlacklist != null)
            {
                EmployeeBlacklist newEmployeeBlacklist = await _employeeBlacklistService.GetAllEmployeeBlacklistsByCustomerIDAndEmployeeID(EmployeeBlacklist.EmployeeID, EmployeeBlacklist.CustomerID);

                if (newEmployeeBlacklist != null)
                {
                    newEmployeeBlacklist.IsDelete = true;
                    newEmployeeBlacklist.LastUpdated = DateTime.Now;
                    newEmployeeBlacklist.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _employeeBlacklistService.Update(newEmployeeBlacklist);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmpBlacklist(int ID)
        {
            if (ID != 0)
            {
                EmployeeBlacklist newEmployeeBlacklist = await _employeeBlacklistService.GetByID(ID);

                newEmployeeBlacklist.IsDelete = true;
                newEmployeeBlacklist.LastUpdated = DateTime.Now;
                newEmployeeBlacklist.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeBlacklistService.Update(newEmployeeBlacklist);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeBlacklistsByEmployeeID(bool isDisplayAll, long EmployeeID)
        {
            List<EmployeeBlacklist> EmployeeBlacklistList = await _employeeBlacklistService.GetAllEmployeeBlacklistsByEmployeeID(EmployeeID);

            if (EmployeeBlacklistList != null)
            {
                EmployeeBlacklistList = EmployeeBlacklistList.OrderBy(x => x.CustomerID).ToList();
                var data = EmployeeBlacklistList.Select(x => new { x.EmployeeBlacklistID, x.EmployeeID, x.CustomerID, x.Customer.CustomerName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        #endregion

        #region Certification

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeCertification([FromBody]EmployeeCertification EmployeeCertification)
        {
            if (EmployeeCertification != null)
            {
                EmployeeCertification newEmployeeCertification = await _employeeCertificationService.GetAllEmployeeCertificationsByCertificationIDAndEmployeeID(EmployeeCertification.EmployeeID, EmployeeCertification.CertificationID);

                if (newEmployeeCertification == null)
                {
                    response = await _employeeCertificationService.Create(EmployeeCertification);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteEmployeeCertification([FromBody] EmployeeCertification EmployeeCertification)
        {
            if (EmployeeCertification != null)
            {
                EmployeeCertification newEmployeeCertification = await _employeeCertificationService.GetAllEmployeeCertificationsByCertificationIDAndEmployeeID(EmployeeCertification.EmployeeID, EmployeeCertification.CertificationID);

                if (newEmployeeCertification != null)
                {
                    newEmployeeCertification.IsDelete = true;
                    newEmployeeCertification.LastUpdated = DateTime.Now;
                    newEmployeeCertification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _employeeCertificationService.Update(newEmployeeCertification);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmpCertification(int ID)
        {
            if (ID != 0)
            {
                EmployeeCertification newEmployeeCertification = await _employeeCertificationService.GetByID(ID);

                newEmployeeCertification.IsDelete = true;
                newEmployeeCertification.LastUpdated = DateTime.Now;
                newEmployeeCertification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeCertificationService.Update(newEmployeeCertification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeCertificationsByEmployeeID(bool isDisplayAll, long EmployeeID)
        {
            List<EmployeeCertification> EmployeeCertificationList = await _employeeCertificationService.GetAllEmployeeCertificationsByEmployeeID(EmployeeID);

            if (EmployeeCertificationList != null)
            {
                EmployeeCertificationList = EmployeeCertificationList.OrderBy(x => x.CertificationID).ToList();
                var data = EmployeeCertificationList.Select(x => new { x.EmployeeCertificationID, x.EmployeeID, x.CertificationID, x.Certification.CertificationName, x.Picture });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "1,2")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateCertificationImage()
        {
            //Allow to update only picture
            System.Collections.Specialized.NameValueCollection formdata = System.Web.HttpContext.Current.Request.Form;

            Dictionary<string, string> dr = new Dictionary<string, string>();

            foreach (var Key in formdata.AllKeys)
            {
                int i = 0;
                string[] U = formdata.GetValues(Key);
                dr[Key] = U[i];
                i++;
            }

            EmployeeCertification employeeCertification = new EmployeeCertification();

            foreach (var item in dr)
            {
                if (item.Key == "EmployeeCertificationID")
                    employeeCertification.EmployeeCertificationID = Convert.ToInt64(item.Value);

                if (item.Key == "Picture")
                    employeeCertification.Picture = item.Value;
            }

            string folderNameForFile = string.Empty;
            string fileName = string.Empty;

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            if (employeeCertification == null)
                return NotFound();

            EmployeeCertification newEmployeeCertification = await _employeeCertificationService.GetByID(employeeCertification.EmployeeCertificationID);

            HttpPostedFile hpf = null;

            if (hfc.Count > 0)
            {
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid().ToString("N");
                        folderNameForFile = fileName.Substring(0, 2);

                        if (File.Exists(HttpContext.Current.Server.MapPath("~/Files/Certifications/" + employeeCertification.Picture)))
                            File.Delete(HttpContext.Current.Server.MapPath("~/Files/Certifications/" + employeeCertification.Picture));

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Certifications/" + folderNameForFile + "/" + fileName)))
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Certifications/" + folderNameForFile + "/" + fileName));

                        var uploadPath = HttpContext.Current.Server.MapPath("~/Files/Certifications/" + folderNameForFile + "/" + fileName);

                        DirectoryInfo info = new DirectoryInfo(uploadPath);

                        DirectorySecurity security = info.GetAccessControl();

                        security.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

                        info.SetAccessControl(security);

                        if (!File.Exists(uploadPath + "/" + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(uploadPath + "/" + Path.GetFileName(hpf.FileName));
                        }

                        newEmployeeCertification.Picture = folderNameForFile + "/" + fileName + "/" + Path.GetFileName(hpf.FileName);
                        newEmployeeCertification.LastUpdated = DateTime.Now;
                        newEmployeeCertification.LastUpdatedBy = RoleHelper.GetCurrentUserID;
                    }
                }
            }

            response = await _employeeCertificationService.Update(newEmployeeCertification);
            return Ok(response);
        }


        #endregion

        #region Skills

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployeeSkill([FromBody]EmployeeSkill EmployeeSkill)
        {
            if (EmployeeSkill != null)
            {
                EmployeeSkill newEmployeeSkill = await _employeeSkillService.GetAllEmployeeSkillsBySkillIDAndEmployeeID(EmployeeSkill.EmployeeID, EmployeeSkill.SkillID);

                if (newEmployeeSkill == null)
                {
                    response = await _employeeSkillService.Create(EmployeeSkill);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteEmployeeSkill([FromBody] EmployeeSkill EmployeeSkill)
        {
            if (EmployeeSkill != null)
            {
                EmployeeSkill newEmployeeSkill = await _employeeSkillService.GetAllEmployeeSkillsBySkillIDAndEmployeeID(EmployeeSkill.EmployeeID, EmployeeSkill.SkillID);

                if (newEmployeeSkill != null)
                {
                    newEmployeeSkill.IsDelete = true;
                    newEmployeeSkill.LastUpdated = DateTime.Now;
                    newEmployeeSkill.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _employeeSkillService.Update(newEmployeeSkill);
                    return Ok(response);
                }
                else
                    return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmpSkill(int ID)
        {
            if (ID != 0)
            {
                EmployeeSkill newEmployeeSkill = await _employeeSkillService.GetByID(ID);

                newEmployeeSkill.IsDelete = true;
                newEmployeeSkill.LastUpdated = DateTime.Now;
                newEmployeeSkill.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _employeeSkillService.Update(newEmployeeSkill);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeeSkillsByEmployeeID(bool isDisplayAll, long EmployeeID)
        {
            List<EmployeeSkill> EmployeeSkillList = await _employeeSkillService.GetAllEmployeeSkillsByEmployeeID(EmployeeID);

            if (EmployeeSkillList != null)
            {
                EmployeeSkillList = EmployeeSkillList.OrderBy(x => x.SkillID).ToList();
                var data = EmployeeSkillList.Select(x => new { x.EmployeeSkillID, x.EmployeeID, x.SkillID, x.Skill.SkillName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        #endregion


        #region Timeslip

        [HttpGet]
        public async Task<IHttpActionResult> GetAllEmployeesByCustomerID(bool isDisplayAll, long CustomerID)
        {
            List<EmployeeBlacklist> EmployeeBlacklistList = await _employeeBlacklistService.GetAllEmployeeBlacklistsByCustomerID(CustomerID);
            List<Employee> EmployeeList = await _employeeService.GetAllEmployees();
            List<long> BlacklistEmployeeIDList = EmployeeBlacklistList.Select(x => x.EmployeeID).ToList();

            var nonBlacklistEmployee = EmployeeList.Where(x => !BlacklistEmployeeIDList.Contains(x.EmployeeID)).ToList();

            if (nonBlacklistEmployee != null)
            {
                nonBlacklistEmployee = nonBlacklistEmployee.OrderBy(x => x.EmployeeID).ToList();
                var data = nonBlacklistEmployee.Select(x => new { x.EmployeeID, EmployeeName = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.AccountNo });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        #endregion
    }
}
