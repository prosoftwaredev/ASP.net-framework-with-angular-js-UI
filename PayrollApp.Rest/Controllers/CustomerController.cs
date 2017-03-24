using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerSiteService _customerSiteService;
        private readonly ICustomerSiteNoteService _customerSiteNoteService;
        private readonly ICustomerSiteLabourClassificationService _customerSiteLabourClassificationService;
        private readonly ILabourClassificationService _labourClassificationService;
        private readonly IUserService _userService;
        private readonly ICustomerSiteJobLocationService _customerSiteJobLocationService;
        string response;
        UserHelper UserHelper = null;

        public CustomerController() { }

        public CustomerController(ICustomerService customerService, ICustomerSiteService customerSiteService, ICustomerSiteNoteService customerSiteNoteService, IUserService userService, ICustomerSiteLabourClassificationService customerSiteLabourClassificationService, ILabourClassificationService labourClassificationService, ICustomerSiteJobLocationService customerSiteJobLocationService)
        {
            _customerService = customerService;
            _customerSiteService = customerSiteService;
            _customerSiteNoteService = customerSiteNoteService;
            _userService = userService;
            _customerSiteLabourClassificationService = customerSiteLabourClassificationService;
            _labourClassificationService = labourClassificationService;
            _customerSiteJobLocationService = customerSiteJobLocationService;
            UserHelper = new UserHelper();
            UserHelper.UserService1 = _userService;

        }

        #region Customer

        //[Authorize(Customers = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerss(FormDataCollection form)
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

            PagedData<Customer> pagedData = await _customerService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.CustomerID, x.CustomerName, x.CompanyName, x.Created, x.IsEnable, x.Remark }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> GetCustomers(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            SearchCustomerSite search = new SearchCustomerSite
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            PagedData<TempCustomer> PagedData = await _customerService.Get(search);

            if (PagedData != null)
            {
                var data = new { Items = PagedData.Items.Select(x => new { x.CustomerID, x.AccountNo, x.CustomerName, x.PrContactName, x.PrEmail, x.PrMobile, x.PrPhone, x.PrFax, x.Created, x.IsEnable, x.Delinquent, x.SitesCount }), Count = PagedData.Count };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Customers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerByID(long CustomerID)
        {
            if (CustomerID <= 0)
                return NotFound();

            Customer Customer = await _customerService.GetByID(CustomerID);

            if (Customer != null)
            {
                var data = new { Customer.CustomerID, Customer.CustomerName, Customer.CompanyName, Customer.AccountNo, Customer.SalesRepID, Customer.PaymentTermID, Customer.RequirePO, Customer.UniquePO, Customer.PayByCC, Customer.Delinquent, Customer.InvoiceDiscountMessage, Customer.HideCustomerName, Customer.Created, Customer.IsEnable, Customer.LastUpdated, Customer.Remark, Customer.SortOrder, SalesRepName = Customer.SalesRep == null ? "NA" : Customer.SalesRep.SalesRepName, PaymentTermName = Customer.PaymentTerm == null ? "NA" : Customer.PaymentTerm.PaymentTermName, CreatedByName = await UserHelper.GetUserName1(Customer.CreatedBy), LastUpdatedByName = await UserHelper.GetUserName1(Customer.LastUpdatedBy) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody]Customer Customer)
        {
            if (Customer != null)
            {
                string prContactName = Customer.CustomerName.Replace(" ", "");

                string strRefInitial = prContactName.Substring(0, prContactName.Length < 3 ? prContactName.Length : 3);

                string strNewAccountNo = string.Empty;

                if (string.IsNullOrWhiteSpace(Customer.AccountNo))
                {
                    //TODO: Should move to seperate utility function class
                    string strAccountNo = await _customerService.GetAccountNumber(strRefInitial);

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
                        strNewAccountNo = prContactName.Substring(0, prContactName.Length < 3 ? prContactName.Length : 3) + "001";
                    }

                    Customer.AccountNo = strNewAccountNo;
                }

                Customer.AccountNo = Customer.AccountNo.ToUpper();

                Customer.XmlNote = XmlNote(Customer, true);

                response = await _customerService.Create(Customer);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromBody]Customer Customer)
        {
            if (Customer != null)
            {
                Customer newCustomer = await _customerService.GetByID(Customer.CustomerID);

                newCustomer.CustomerName = Customer.CustomerName;
                newCustomer.CompanyName = Customer.CompanyName;
                newCustomer.SalesRepID = Customer.SalesRepID;
                newCustomer.PaymentTermID = Customer.PaymentTermID;
                newCustomer.RequirePO = Customer.RequirePO;
                newCustomer.UniquePO = Customer.UniquePO;
                newCustomer.PayByCC = Customer.PayByCC;
                newCustomer.Delinquent = Customer.Delinquent;
                newCustomer.InvoiceDiscountMessage = Customer.InvoiceDiscountMessage;
                newCustomer.HideCustomerName = Customer.HideCustomerName;
                newCustomer.IsEnable = Customer.IsEnable;
                newCustomer.Remark = Customer.Remark;
                newCustomer.LastUpdated = DateTime.Now;
                newCustomer.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                newCustomer.XmlNote = XmlNote(Customer, false);

                response = await _customerService.Update(newCustomer);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerIsEnable([FromBody]Customer Customer)
        {
            if (Customer != null)
            {
                Customer newCustomer = await _customerService.GetByID(Customer.CustomerID);

                newCustomer.IsEnable = Customer.IsEnable;
                newCustomer.LastUpdated = DateTime.Now;
                newCustomer.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerService.Update(newCustomer);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerDelinquent([FromBody]Customer Customer)
        {
            if (Customer != null)
            {
                Customer newCustomer = await _customerService.GetByID(Customer.CustomerID);

                newCustomer.Delinquent = Customer.Delinquent;
                newCustomer.LastUpdated = DateTime.Now;
                newCustomer.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerService.Update(newCustomer);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Customers = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer(long ID)
        {
            if (ID != 0)
            {
                Customer newCustomer = await _customerService.GetByID(ID);

                newCustomer.IsDelete = true;
                newCustomer.LastUpdated = DateTime.Now;

                response = await _customerService.Update(newCustomer);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers(bool isDisplayAll)
        {
            List<Customer> CustomerList = await _customerService.GetAllCustomers();

            if (CustomerList != null)
            {
                CustomerList = CustomerList.OrderBy(x => x.CustomerName).ToList();
                var data = CustomerList.Select(x => new { x.CustomerID, x.CustomerName, x.CompanyName, x.RequirePO });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers(bool isDisplayAll, long CustomerSiteID)
        {
            List<Customer> CustomerList = await _customerService.GetAllCustomers(CustomerSiteID);

            if (CustomerList != null)
            {
                CustomerList = CustomerList.OrderBy(x => x.CustomerID).ToList();
                var data = CustomerList.Select(x => new { x.CustomerID, x.CustomerName, x.CompanyName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        #endregion

        #region Customer Site

        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerSites(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            SearchCustomerSite search = new SearchCustomerSite
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            PagedData<CustomerSite> PagedData = await _customerSiteService.Get(search);

            if (PagedData != null)
            {
                var data = new { Items = PagedData.Items.Select(x => new { x.CustomerID, x.CustomerSiteID, x.Customer.AccountNo, x.Customer.CustomerName, x.PrContactName, x.PrEmail, x.PrMobile, x.PrPhone, x.PrFax, x.Created, x.IsEnable, x.Customer.Delinquent, x.IsPrimary }), Count = PagedData.Count };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Customers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerSiteByID(long CustomerSiteID)
        {
            if (CustomerSiteID <= 0)
                return NotFound();

            CustomerSite CustomerSite = await _customerSiteService.GetByID(CustomerSiteID);

            if (CustomerSite != null)
            {
                var data = new
                {
                    CustomerSite.CustomerSiteID,
                    CustomerSite.CustomerID,
                    CustomerSite.AccountNo,
                    CustomerSite.SiteName,
                    CustomerSite.SiteDescription,
                    CustomerSite.PrAddress1,
                    CustomerSite.PrAddress2,
                    CustomerSite.PrContactName,
                    CustomerSite.PrEmail,
                    CustomerSite.PrFax,
                    CustomerSite.PrPhone,
                    CustomerSite.PrMobile,
                    CustomerSite.PrPostalCode,
                    CustomerSite.InAddress1,
                    CustomerSite.InAddress2,
                    CustomerSite.InContactName,
                    CustomerSite.InEmail,
                    CustomerSite.InFax,
                    CustomerSite.InPhone,
                    CustomerSite.InMobile,
                    CustomerSite.InPostalCode,
                    CustomerSite.InvoiceAutomatically,
                    CustomerSite.InvoiceCombine,
                    CustomerSite.InvoiceViaEmail,
                    CustomerSite.InvoiceViaFax,
                    CustomerSite.InvoiceViaMail,
                    CustomerSite.CertificateNo,
                    CustomerSite.OTPerDay,
                    CustomerSite.OTPerWeek,
                    CustomerSite.TimeslipMsg,
                    CustomerSite.Reminder,
                    Created = CustomerSite.Created.Value.ToString("dd-MMM-yyyy hh:mm:ss tt"),
                    CustomerSite.IsEnable,
                    LastUpdated = CustomerSite.LastUpdated.HasValue ? CustomerSite.LastUpdated.Value.ToString("dd-MMM-yyyy hh:mm:ss tt") : "",
                    CustomerSite.Remark,
                    CustomerSite.SortOrder,
                    CreatedByName = await UserHelper.GetUserName1(CustomerSite.CreatedBy),
                    LastUpdatedByName = await UserHelper.GetUserName1(CustomerSite.LastUpdatedBy),
                    PrCountryID = CustomerSite.PrCity == null ? 0 : CustomerSite.PrCity.State.CountryID,
                    PrStateID = CustomerSite.PrCity == null ? 0 : CustomerSite.PrCity.StateID,
                    PrCityID = CustomerSite.PrCityID == null ? 0 : CustomerSite.PrCityID,
                    InCountryID = CustomerSite.InCity == null ? 0 : CustomerSite.InCity.State.CountryID,
                    InStateID = CustomerSite.InCity == null ? 0 : CustomerSite.InCity.StateID,
                    InCityID = CustomerSite.InCityID == null ? 0 : CustomerSite.InCityID,

                    PrCountryName = CustomerSite.PrCity == null ? "NA" : CustomerSite.PrCity.State.Country.CountryName,
                    PrStateName = CustomerSite.PrCity == null ? "NA" : CustomerSite.PrCity.State.StateName,
                    PrCityName = CustomerSite.PrCity == null ? "NA" : CustomerSite.PrCity.CityName,
                    InCountryName = CustomerSite.InCity == null ? "NA" : CustomerSite.InCity.State.Country.CountryName,
                    InStateName = CustomerSite.InCity == null ? "NA" : CustomerSite.InCity.State.StateName,
                    InCityName = CustomerSite.InCity == null ? "NA" : CustomerSite.InCity.CityName,
                };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Customers = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomerSite([FromBody]CustomerSite CustomerSite)
        {
            if (CustomerSite != null)
            {
                string prContactName = CustomerSite.PrContactName.Replace(" ", "");

                string strRefInitial = prContactName.Substring(0, prContactName.Length < 3 ? prContactName.Length : 3);

                string strNewAccountNo = string.Empty;

                string strAccountNo = await _customerSiteService.GetAccountNumber(strRefInitial);

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
                    strNewAccountNo = prContactName.Substring(0, prContactName.Length < 3 ? prContactName.Length : 3) + "001";
                }

                CustomerSite.AccountNo = strNewAccountNo.ToUpper();


                bool isPrimarySiteAvailable = await _customerSiteService.GetIsPrimaryCustomerSite(CustomerSite.CustomerID, false);
                if (isPrimarySiteAvailable)
                    CustomerSite.IsPrimary = false;
                else
                    CustomerSite.IsPrimary = true;

                string newValue = string.Empty;
                Customer newCustomer = await _customerService.GetByID(CustomerSite.CustomerID);
                newValue = Utility.SetElement(newCustomer.XmlNote, "Phone", CustomerSite.PrPhone);
                newValue = Utility.SetElement(newValue, "PlacedBy", await UserHelper.GetUserName1(newCustomer.CreatedBy));

                newValue = Utility.SetElement(newValue, "Certificate", CustomerSite.CertificateNo);
                newValue = Utility.SetElement(newValue, "DailyHrsBeforeOT", CustomerSite.OTPerDay);
                newValue = Utility.SetElement(newValue, "WeeklyHrsBeforeOT", CustomerSite.OTPerWeek);
                newValue = Utility.SetElement(newValue, "TimeslipMsg", CustomerSite.TimeslipMsg);

                newCustomer.XmlNote = newValue;
                await _customerService.Update(newCustomer);

                response = await _customerSiteService.Create(CustomerSite);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateContact([FromBody]CustomerSite CustomerSite)
        {
            if (CustomerSite != null)
            {
                CustomerSite newCustomerSite = await _customerSiteService.GetByID(CustomerSite.CustomerSiteID);

                newCustomerSite.SiteName = CustomerSite.SiteName;
                newCustomerSite.SiteDescription = CustomerSite.SiteDescription;
                newCustomerSite.PrContactName = CustomerSite.PrContactName;
                newCustomerSite.PrAddress1 = CustomerSite.PrAddress1;
                newCustomerSite.PrAddress2 = CustomerSite.PrAddress2;
                newCustomerSite.PrCityID = CustomerSite.PrCityID;
                newCustomerSite.PrPostalCode = CustomerSite.PrPostalCode;
                newCustomerSite.PrPhone = CustomerSite.PrPhone;
                newCustomerSite.PrMobile = CustomerSite.PrMobile;
                newCustomerSite.PrFax = CustomerSite.PrFax;
                newCustomerSite.PrEmail = CustomerSite.PrEmail;

                newCustomerSite.InContactName = CustomerSite.InContactName;
                newCustomerSite.InAddress1 = CustomerSite.InAddress1;
                newCustomerSite.InAddress2 = CustomerSite.InAddress2;
                newCustomerSite.InCityID = CustomerSite.InCityID;
                newCustomerSite.InPostalCode = CustomerSite.InPostalCode;
                newCustomerSite.InPhone = CustomerSite.InPhone;
                newCustomerSite.InMobile = CustomerSite.InMobile;
                newCustomerSite.InFax = CustomerSite.InFax;
                newCustomerSite.InEmail = CustomerSite.InEmail;

                newCustomerSite.InvoiceViaMail = CustomerSite.InvoiceViaMail;
                newCustomerSite.InvoiceViaFax = CustomerSite.InvoiceViaFax;
                newCustomerSite.InvoiceViaEmail = CustomerSite.InvoiceViaEmail;
                newCustomerSite.InvoiceAutomatically = CustomerSite.InvoiceAutomatically;
                newCustomerSite.InvoiceCombine = CustomerSite.InvoiceCombine;

                newCustomerSite.IsEnable = CustomerSite.IsEnable;
                newCustomerSite.Remark = CustomerSite.Remark;
                newCustomerSite.LastUpdated = DateTime.Now;
                newCustomerSite.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteService.Update(newCustomerSite);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOvertime([FromBody]CustomerSite CustomerSite)
        {
            if (CustomerSite != null)
            {
                CustomerSite newCustomerSite = await _customerSiteService.GetByID(CustomerSite.CustomerSiteID);

                newCustomerSite.CertificateNo = CustomerSite.CertificateNo;
                newCustomerSite.OTPerDay = CustomerSite.OTPerDay;
                newCustomerSite.OTPerWeek = CustomerSite.OTPerWeek;
                newCustomerSite.TimeslipMsg = CustomerSite.TimeslipMsg;

                newCustomerSite.LastUpdated = DateTime.Now;
                newCustomerSite.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                string newValue = string.Empty;
                Customer newCustomer = await _customerService.GetByID(CustomerSite.CustomerID);
                newValue = Utility.SetElement(newCustomer.XmlNote, "Phone", CustomerSite.PrPhone);
                newValue = Utility.SetElement(newValue, "PlacedBy", await UserHelper.GetUserName1(newCustomer.CreatedBy));

                newValue = Utility.SetElement(newValue, "Certificate", CustomerSite.CertificateNo);
                newValue = Utility.SetElement(newValue, "DailyHrsBeforeOT", CustomerSite.OTPerDay);
                newValue = Utility.SetElement(newValue, "WeeklyHrsBeforeOT", CustomerSite.OTPerWeek);
                newValue = Utility.SetElement(newValue, "TimeslipMsg", CustomerSite.TimeslipMsg);

                newCustomer.XmlNote = newValue;
                await _customerService.Update(newCustomer);

                response = await _customerSiteService.Update(newCustomerSite);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Customers = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateReminder([FromBody]CustomerSite CustomerSite)
        {
            if (CustomerSite != null)
            {
                CustomerSite newCustomerSite = await _customerSiteService.GetByID(CustomerSite.CustomerSiteID);

                newCustomerSite.Reminder = CustomerSite.Reminder;

                newCustomerSite.LastUpdated = DateTime.Now;
                newCustomerSite.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteService.Update(newCustomerSite);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerSiteIsEnable([FromBody]CustomerSite CustomerSite)
        {
            if (CustomerSite != null)
            {
                CustomerSite newCustomerSite = await _customerSiteService.GetByID(CustomerSite.CustomerSiteID);

                newCustomerSite.IsEnable = CustomerSite.IsEnable;
                newCustomerSite.LastUpdated = DateTime.Now;
                newCustomerSite.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteService.Update(newCustomerSite);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomerSitesByCustomerID(bool isDisplayAll, long CustomerID)
        {
            List<CustomerSite> CustomerSiteList = await _customerSiteService.GetAllCustomerSitesByCustomerID(CustomerID);

            if (CustomerSiteList != null)
            {
                CustomerSiteList = CustomerSiteList.OrderByDescending(x => x.CustomerSiteID).ToList();
                var data = CustomerSiteList.Select(x => new { x.CustomerSiteID, x.PrContactName, x.IsPrimary, x.SiteName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Customers = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPrimarySiteIDFromCustomerID(long CustomerID)
        {
            if (CustomerID <= 0)
                return NotFound();

            long CustomerSiteID = await _customerSiteService.GetIsPrimaryCustomerSite(CustomerID);
            return Ok(CustomerSiteID);
        }

        #endregion

        #region Customer Site Notes

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomerSiteNote([FromBody]CustomerSiteNote CustomerSiteNote)
        {
            if (CustomerSiteNote != null)
            {
                string newValue = string.Empty;
                Customer newCustomer = await _customerService.GetByID(CustomerSiteNote.CustomerID);
                string oldValue = Utility.GetElement(newCustomer.XmlNote, "Note");
                newValue = Utility.SetElement(newCustomer.XmlNote, "Note", !string.IsNullOrEmpty(oldValue) ? oldValue + ", " + CustomerSiteNote.Note : CustomerSiteNote.Note);
                newCustomer.XmlNote = newValue;
                await _customerService.Update(newCustomer);

                response = await _customerSiteNoteService.Create(CustomerSiteNote);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerSiteNote(int ID)
        {
            if (ID != 0)
            {
                CustomerSiteNote newCustomerSiteNote = await _customerSiteNoteService.GetByID(ID);

                newCustomerSiteNote.IsDelete = true;
                newCustomerSiteNote.LastUpdated = DateTime.Now;
                newCustomerSiteNote.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteNoteService.Update(newCustomerSiteNote);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomerSiteNotesByCustomerSiteID(bool isDisplayAll, long CustomerSiteID)
        {
            List<CustomerSiteNote> CustomerSiteNoteList = await _customerSiteNoteService.GetAllCustomerSiteNotesByCustomerSiteID(CustomerSiteID);

            if (CustomerSiteNoteList != null)
            {
                CustomerSiteNoteList = CustomerSiteNoteList.OrderByDescending(x => x.CustomerSiteNoteID).ToList();
                foreach (var item in CustomerSiteNoteList)
                {
                    item.CreatedByName = await UserHelper.GetUserName1(item.CreatedBy);
                }
                //var data = CustomerSiteNoteList.Select(async x => new { x.CustomerSiteNoteID, x.Note, x.Created, CreatedBy = x.CreatedBy != 0 ? await GetUserName(x.CreatedBy) : null });
                var data = CustomerSiteNoteList.Select(x => new { x.CustomerSiteNoteID, x.Note, x.Created, CreatedBy = x.CreatedByName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        #endregion

        #region Customer Site Labour Classifications

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomerSiteLabourClassification([FromBody]CustomerSiteLabourClassification CustomerSiteLabourClassification)
        {
            if (CustomerSiteLabourClassification != null)
            {

                var labourClassifications = await _labourClassificationService.GetAllLabourClassifications();

                foreach (var lc in labourClassifications)
                {
                    CustomerSiteLabourClassification cslc = new CustomerSiteLabourClassification
                    {
                        CustomerSiteID = CustomerSiteLabourClassification.CustomerSiteID,
                        PayRate = CustomerSiteLabourClassification.PayRate,
                        InvoiceRate = CustomerSiteLabourClassification.InvoiceRate,
                        LabourClassificationID = lc.LabourClassificationID,
                        IsExpire = false
                    };
                    response = await _customerSiteLabourClassificationService.Create(cslc);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerSiteLabourClassification([FromBody]CustomerSiteLabourClassification CustomerSiteLabourClassification)
        {
            if (CustomerSiteLabourClassification != null)
            {
                CustomerSiteLabourClassification newCustomerSiteLabourClassification = await _customerSiteLabourClassificationService.GetByID(CustomerSiteLabourClassification.CustomerSiteLabourClassificationID);

                newCustomerSiteLabourClassification.PayRate = CustomerSiteLabourClassification.PayRate;
                newCustomerSiteLabourClassification.InvoiceRate = CustomerSiteLabourClassification.InvoiceRate;
                newCustomerSiteLabourClassification.LastUpdated = DateTime.Now;
                newCustomerSiteLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteLabourClassificationService.Update(newCustomerSiteLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteCustomerSiteLabourClassification([FromBody] CustomerSiteLabourClassification CustomerSiteLabourClassification)
        {
            if (CustomerSiteLabourClassification != null)
            {
                CustomerSiteLabourClassification newCustomerSiteLabourClassification = await _customerSiteLabourClassificationService.GetAllCustomerSiteLabourClassificationsByLabourClassificationIDAndCustomerSiteID(CustomerSiteLabourClassification.CustomerSiteID, CustomerSiteLabourClassification.LabourClassificationID);

                if (newCustomerSiteLabourClassification != null)
                {
                    newCustomerSiteLabourClassification.IsDelete = true;
                    newCustomerSiteLabourClassification.LastUpdated = DateTime.Now;
                    newCustomerSiteLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _customerSiteLabourClassificationService.Update(newCustomerSiteLabourClassification);
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
        public async Task<IHttpActionResult> DeleteCustSiteLabourClassification(long ID)
        {
            if (ID != 0)
            {
                CustomerSiteLabourClassification newCustomerSiteLabourClassification = await _customerSiteLabourClassificationService.GetByID(ID);

                newCustomerSiteLabourClassification.IsDelete = true;
                newCustomerSiteLabourClassification.LastUpdated = DateTime.Now;
                newCustomerSiteLabourClassification.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteLabourClassificationService.Update(newCustomerSiteLabourClassification);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomerSiteLabourClassificationsByCustomerSiteID(bool isDisplayAll, long CustomerSiteID)
        {
            List<CustomerSiteLabourClassification> CustomerSiteLabourClassificationList = await _customerSiteLabourClassificationService.GetAllCustomerSiteLabourClassificationsByCustomerSiteID(CustomerSiteID);

            if (CustomerSiteLabourClassificationList != null)
            {
                CustomerSiteLabourClassificationList = CustomerSiteLabourClassificationList.OrderByDescending(x => x.CustomerSiteLabourClassificationID).ToList();
                var data = CustomerSiteLabourClassificationList.Select(x => new { CustomerSiteLabourClassificationID = x.CustomerSiteLabourClassificationID, x.CustomerSiteID, LabourClassificationID = x.LabourClassificationID, x.PayRate, x.InvoiceRate, x.LabourClassification.LabourClassificationName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetRatesByCustomerSiteIDAndLabourClassificationID(long CustomerSiteID, long LabourClassificationID)
        {
            CustomerSiteLabourClassification CustomerSiteLabourClassification = await _customerSiteLabourClassificationService.GetAllCustomerSiteLabourClassificationsByLabourClassificationIDAndCustomerSiteID(CustomerSiteID, LabourClassificationID);

            if (CustomerSiteLabourClassification != null)
            {
                var data = new { CustomerSiteLabourClassification.PayRate, CustomerSiteLabourClassification.InvoiceRate };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        #endregion

        #region Customer Site Job Location

        //[Authorize(Employees = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomerSiteJobLocation([FromBody]CustomerSiteJobLocation CustomerSiteJobLocation)
        {
            if (CustomerSiteJobLocation != null)
            {
                response = await _customerSiteJobLocationService.Create(CustomerSiteJobLocation);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Employees = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerSiteJobLocation([FromBody]CustomerSiteJobLocation CustomerSiteJobLocation)
        {
            if (CustomerSiteJobLocation != null)
            {
                CustomerSiteJobLocation newCustomerSiteJobLocation = await _customerSiteJobLocationService.GetByID(CustomerSiteJobLocation.CustomerSiteJobLocationID);

                newCustomerSiteJobLocation.JobLocation = CustomerSiteJobLocation.JobLocation;
                newCustomerSiteJobLocation.JobAddress = CustomerSiteJobLocation.JobAddress;
                newCustomerSiteJobLocation.JobNote = CustomerSiteJobLocation.JobNote;
                newCustomerSiteJobLocation.LastUpdated = DateTime.Now;
                newCustomerSiteJobLocation.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteJobLocationService.Update(newCustomerSiteJobLocation);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        //[Authorize(Employees = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustSiteJobLocation(long ID)
        {
            if (ID != 0)
            {
                CustomerSiteJobLocation newCustomerSiteJobLocation = await _customerSiteJobLocationService.GetByID(ID);

                newCustomerSiteJobLocation.IsDelete = true;
                newCustomerSiteJobLocation.LastUpdated = DateTime.Now;
                newCustomerSiteJobLocation.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _customerSiteJobLocationService.Update(newCustomerSiteJobLocation);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomerSiteJobLocationsByCustomerSiteID(bool isDisplayAll, long CustomerSiteID)
        {
            List<CustomerSiteJobLocation> CustomerSiteJobLocationList = await _customerSiteJobLocationService.GetAllCustomerSiteJobLocationsByCustomerSiteID(CustomerSiteID);

            if (CustomerSiteJobLocationList != null)
            {
                CustomerSiteJobLocationList = CustomerSiteJobLocationList.OrderByDescending(x => x.CustomerSiteJobLocationID).ToList();
                var data = CustomerSiteJobLocationList.Select(x => new { CustomerSiteJobLocationID = x.CustomerSiteJobLocationID, x.CustomerSiteID, x.JobAddress, x.JobLocation, x.JobNote });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        #endregion

        #region Utility

        public string XmlNote(Customer customer, bool isFromCreate)
        {

            string xmlNote = string.Empty;
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ListID", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "SalesRepListID", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "TermsListID", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Order_Template", true);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Phone", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "PlacedBy", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "SkillReqd", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ReportTo", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "EquipReqd", String.Join(",", customer.EquipmentIDs));
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Offset", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Duration ", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "TimeslipsWeekly", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "QtyReqd", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DispatchNote", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ID_Place", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DailyHrsBeforeOT", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "WeeklyHrsBeforeOT", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Comments", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Order_Template", false);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "OT", true);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Certificate", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DailyHrsBeforeOT", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "WeeklyHrsBeforeOT", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "TimeslipMsg", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "OT", false);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "POReqd", customer.RequirePO.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "CC", customer.PayByCC.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DELQ", customer.Delinquent.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DiscMsg", customer.InvoiceDiscountMessage.ToString());

            if (isFromCreate)
                xmlNote = CommonHelper.CreateXmlData(xmlNote, "Note", "");

            xmlNote = CommonHelper.CreateXmlData(xmlNote, "POUnique", customer.UniquePO.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "HideEmpNm", customer.HideCustomerName.ToString());

            return xmlNote;
        }

        #endregion
    }
}
