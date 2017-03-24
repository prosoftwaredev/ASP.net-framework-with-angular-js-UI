using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace PayrollApp.Rest.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderEquipmentService _orderEquipmentService;
        private readonly IOrderTimeslipService _orderTimeslipService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ICustomerSiteJobLocationService _customerSiteJobLocationService;
        private readonly ILabourClassificationService _labourClassificationService;

        string response;

        public OrderController() { }

        public OrderController(IOrderService orderService, IOrderEquipmentService orderEquipmentService, IOrderTimeslipService orderTimeslipService, IUserService userService, ICustomerService customerService, ICustomerSiteJobLocationService customerSiteJobLocationService, ILabourClassificationService labourClassificationService)
        {
            _orderService = orderService;
            _userService = userService;
            _orderEquipmentService = orderEquipmentService;
            _orderTimeslipService = orderTimeslipService;
            _customerService = customerService;
            _customerSiteJobLocationService = customerSiteJobLocationService;
            _labourClassificationService = labourClassificationService;
            UserHelper.UserService = _userService;
        }

        #region Order

        [HttpPost]
        public async Task<IHttpActionResult> GetOrders(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            //string skillId = (Convert.ToString(form.Get("SkillID")) == "") ? null : Convert.ToString(form.Get("SkillID"));
            //string certificationId = (Convert.ToString(form.Get("CertificationID")) == "") ? null : Convert.ToString(form.Get("CertificationID"));

            //long SkillID = skillId == null ? 0 : Convert.ToInt64(skillId);
            //long CertificationID = certificationId == null ? 0 : Convert.ToInt64(certificationId);

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            SearchOrder search = new SearchOrder
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            PagedData<Order> PagedData = await _orderService.Get(search);

            if (PagedData != null)
            {

                var Items = PagedData.Items.Select(async x => new { x.OrderID, x.PONumber, x.Customer.CustomerName, x.Customer.CompanyName, x.ContactName, x.Phone, x.CustomerSite.CustomerSiteJobLocations.Where(y => y.CustomerSiteID == x.CustomerSiteID).First().JobLocation, x.WorkStartRsv, x.WorkEndRsv, x.Created, x.IsEnable, CreatedByName = await UserHelper.GetUserName(x.CreatedBy) });
                var result = await Task.WhenAll(Items);

                var data = new { Items = result, Count = PagedData.Count };


                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Orders = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrderByID(int OrderID)
        {
            if (OrderID <= 0)
                return NotFound();

            Order Order = await _orderService.GetByID(OrderID);

            if (Order != null)
            {
                var data = new { Order.OrderID, Order.CustomerSiteID, Order.CustomerID, Order.LabourClassificationID, Order.Customer.CustomerName, Order.Customer.CompanyName, Order.PONumber, Order.ContactName, Order.Phone, Order.People, Order.WorkStartRsv, Order.WorkEndRsv, Order.StartTimeRsv, Order.EndTimeRsv, Order.WorkStartCust, Order.WorkEndCust, Order.StartTimeCust, Order.EndTimeCust, Order.Reporting, Order.Comment, Order.DispatchNote, JobDuration = Order.JobDuration, Order.OTPerDay, Order.OTPerWeek, Created = Order.Created.Value.ToString("dd-MMM-yyyy hh:mm:ss tt"), Order.IsEnable, LastUpdated = Order.LastUpdated.HasValue ? Order.LastUpdated.Value.ToString("dd-MMM-yyyy hh:mm:ss tt") : "", Order.Remark, Order.SortOrder, CreatedByName = await UserHelper.GetUserName(Order.CreatedBy), LastUpdatedByName = await UserHelper.GetUserName(Order.LastUpdatedBy) }; //Order.PrintName,
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Orders = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrder([FromBody]Order Order)
        {
            if (Order != null)
            {
                //-----------------------------Check Require PO--------------------------
                Customer customer = await _customerService.GetByID(Order.CustomerID);

                if (customer.RequirePO && string.IsNullOrEmpty(Order.PONumber))
                    return BadRequest("The PO Number field cannot be empty");

                //-----------------------------Check Unique PO--------------------------

                bool IsPONumberAlreadyExist = await _orderService.GetUniquePONumber(Order.PONumber);
                if (customer.UniquePO && IsPONumberAlreadyExist)
                    return BadRequest("You'd entered PO Number is already exist... Please try any other PO Number.");


                //-----------------------------Check Job Location--------------------------

                if (Order.CustomerSiteJobLocationID <= 0)
                {
                    CustomerSiteJobLocation csjl = new CustomerSiteJobLocation { CustomerSiteID = Order.CustomerSiteID, JobLocation = Order.JobLocation, JobAddress = Order.JobAddress, JobNote = Order.JobNote };
                    response = await _customerSiteJobLocationService.Create(csjl);
                    Order.CustomerSiteJobLocationID = Convert.ToInt64(response);
                }
                else
                {
                    CustomerSiteJobLocation newCustomerSiteJobLocation = await _customerSiteJobLocationService.GetByID(Order.CustomerSiteJobLocationID);

                    newCustomerSiteJobLocation.JobLocation = Order.JobLocation;
                    newCustomerSiteJobLocation.JobAddress = Order.JobAddress;
                    newCustomerSiteJobLocation.JobNote = Order.JobNote;
                    newCustomerSiteJobLocation.LastUpdated = DateTime.Now;
                    newCustomerSiteJobLocation.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    response = await _customerSiteJobLocationService.Update(newCustomerSiteJobLocation);
                    Order.CustomerSiteJobLocationID = Convert.ToInt64(response);
                }


                Order.XmlNote = XmlNodeForOrder(Order, true);

                response = await _orderService.Create(Order);

                for (int i = 0; i < Order.People; i++)
                {
                    int BillState = -2;

                    OrderTimeslip ot = new OrderTimeslip { OrderID = Convert.ToInt64(response), WorkStartRsv = Order.WorkStartRsv, StartTimeRsv = Order.StartTimeRsv, LabourClassificationID = Order.LabourClassificationID, CustomerSiteJobLocationID = Order.CustomerSiteJobLocationID, DispatchNote = Order.DispatchNote, Comment = Order.Comment, Reporting = Order.Reporting, Phone = Order.Phone, Note = TStatus.Composite, DayOfWeek = TStatus.StartingDay, RollOverStart = TStatus.RollOverDate, RollOver = TStatus.IsToBeRolledOver, BillState = BillState };

                    ot.XmlNote = XmlNodeForTimeslip(TStatus.Composite, ot, true);

                    await _orderTimeslipService.Create(ot);
                }

                foreach (long EquipmentID in Order.EquipmentIDs)
                {
                    OrderEquipment oe = new OrderEquipment { OrderID = Convert.ToInt64(response), EquipmentID = EquipmentID };
                    await _orderEquipmentService.Create(oe);
                }

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }




        //[Authorize(Orders = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOrder([FromBody]Order Order)
        {
            if (Order != null)
            {
                Order newOrder = await _orderService.GetByID(Order.OrderID);

                newOrder.CustomerID = Order.CustomerID;
                newOrder.CustomerSiteID = Order.CustomerSiteID;
                newOrder.LabourClassificationID = Order.LabourClassificationID;
                newOrder.PONumber = Order.PONumber;
                newOrder.ContactName = Order.ContactName;
                newOrder.Phone = Order.Phone;
                newOrder.People = Order.People;
                newOrder.WorkStartRsv = Order.WorkStartRsv;
                newOrder.WorkEndRsv = Order.WorkEndRsv;
                newOrder.StartTimeRsv = Order.StartTimeRsv;
                newOrder.EndTimeRsv = Order.EndTimeRsv;
                newOrder.Reporting = Order.Reporting;
                newOrder.Comment = Order.Comment;
                newOrder.DispatchNote = Order.DispatchNote;
                newOrder.JobDuration = Order.JobDuration;
                newOrder.OTPerDay = Order.OTPerDay;
                newOrder.OTPerWeek = Order.OTPerWeek;

                newOrder.IsEnable = Order.IsEnable;
                newOrder.Remark = Order.Remark;
                newOrder.LastUpdated = DateTime.Now;
                newOrder.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                newOrder.XmlNote = XmlNodeForOrder(newOrder, false);

                response = await _orderService.Update(newOrder);

                List<OrderEquipment> OrderEquipmentList = await _orderEquipmentService.GetAllOrderEquipmentsByOrderID(Order.OrderID);

                foreach (var oe in OrderEquipmentList)
                {
                    OrderEquipment newOrderEquipment = await _orderEquipmentService.GetByID(oe.OrderEquipmentID);

                    newOrderEquipment.IsDelete = true;
                    newOrderEquipment.LastUpdated = DateTime.Now;
                    newOrderEquipment.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                    await _orderEquipmentService.Update(newOrderEquipment);
                }

                foreach (long EquipmentID in Order.EquipmentIDs)
                {
                    OrderEquipment oe = new OrderEquipment { OrderID = Convert.ToInt64(response), EquipmentID = EquipmentID };
                    await _orderEquipmentService.Create(oe);
                }

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Orders = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOrderIsEnable([FromBody]Order Order)
        {
            if (Order != null)
            {
                Order newOrder = await _orderService.GetByID(Order.OrderID);

                newOrder.IsEnable = Order.IsEnable;
                newOrder.LastUpdated = DateTime.Now;
                newOrder.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _orderService.Update(newOrder);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        //[Authorize(Orders = "1")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteOrder(int ID)
        {
            if (ID != 0)
            {
                Order newOrder = await _orderService.GetByID(ID);

                newOrder.IsDelete = true;
                newOrder.LastUpdated = DateTime.Now;
                newOrder.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                response = await _orderService.Update(newOrder);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Order Equipment

        [HttpGet]
        public async Task<IHttpActionResult> GetAllOrderEquipmentsByOrderID(bool isDisplayAll, long OrderID)
        {
            List<OrderEquipment> OrderEquipmentList = await _orderEquipmentService.GetAllOrderEquipmentsByOrderID(OrderID);

            if (OrderEquipmentList != null)
            {
                OrderEquipmentList = OrderEquipmentList.OrderBy(x => x.EquipmentID).ToList();
                var data = OrderEquipmentList.Select(x => new { x.OrderEquipmentID, x.OrderID, x.EquipmentID, x.Equipment.EquipmentName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        #endregion

        #region Order Timeslip

        [HttpPost]
        public async Task<IHttpActionResult> GetOrderTimeslips(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            SearchOrder search = new SearchOrder
            {
                GlobalSearch = globalSearch,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            PagedData<Order> PagedData = await _orderService.Get(search);

            if (PagedData != null)
            {
                var data = new { Items = PagedData.Items.Select(x => new { x.OrderID, x.PONumber, x.Customer.CustomerName, x.Customer.CompanyName, x.ContactName, x.Phone, x.CustomerSite.PrContactName, x.WorkStartRsv, x.WorkEndRsv, x.Created, x.IsEnable, Timeslips = x.OrderTimeslips.Select(y => new { y.OrderTimeslipID, LabourClassificationName = y.LabourClassification.LabourClassificationName, y.CustomerSiteJobLocation.JobLocation, y.CustomerSiteJobLocation.JobAddress, y.Reporting, EmployeeName = y.Employee != null ? y.Employee.FirstName + " " + y.Employee.MiddleName + " " + y.Employee.LastName : "", AccountNo = y.Employee != null ? y.Employee.AccountNo : "", }) }), Count = PagedData.Count };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }



        [HttpGet]
        public async Task<IHttpActionResult> GetAllOrderTimeslipsByOrderID(bool isDisplayAll, long OrderID)
        {
            List<OrderTimeslip> OrderTimeslipList = await _orderTimeslipService.GetAllOrderTimeslipsByOrderID(OrderID);

            if (OrderTimeslipList != null)
            {
                OrderTimeslipList = OrderTimeslipList.OrderBy(x => x.OrderTimeslipID).ToList();
                var data = OrderTimeslipList.Select(x => new { x.OrderTimeslipID, x.OrderID });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }



        public static int TempJobDuration { get; set; }
        public static long TempCustomerID { get; set; }
        public static DateTime TempWorkStartRsv { get; set; }
        public static TimeSpan TempStartTimeRsv { get; set; }
        public static string TempDayOfWeek { get; set; }
        public static string TempComposite { get; set; }
        public static long TempLabourClassificationID { get; set; }
        public static DateTime TempRollOverDate { get; set; }
        public static bool TempIsToBeRolledOver { get; set; }

        //[Authorize(Orders = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrderTimeslipByID(int OrderTimeslipID)
        {
            if (OrderTimeslipID <= 0)
                return NotFound();

            OrderTimeslip OrderTimeslip = await _orderTimeslipService.GetByID(OrderTimeslipID);

            if (OrderTimeslip != null)
            {
                TempJobDuration = OrderTimeslip.Order.JobDuration;
                TempCustomerID = OrderTimeslip.Order.CustomerID;
                TempWorkStartRsv = OrderTimeslip.WorkStartRsv.Value;
                TempStartTimeRsv = OrderTimeslip.StartTimeRsv.Value;
                TempDayOfWeek = OrderTimeslip.DayOfWeek;
                TempComposite = OrderTimeslip.Note;
                TempLabourClassificationID = OrderTimeslip.LabourClassificationID.Value;
                TempRollOverDate = OrderTimeslip.RollOverStart.Value;
                TempIsToBeRolledOver = OrderTimeslip.RollOver;

                DateTime TimeslipStart = Utility.NullDateValue;

                DateTime workStart = new DateTime(OrderTimeslip.WorkStartRsv.Value.Year, OrderTimeslip.WorkStartRsv.Value.Month, OrderTimeslip.WorkStartRsv.Value.Day, OrderTimeslip.StartTimeRsv.Value.Hours, OrderTimeslip.StartTimeRsv.Value.Minutes, OrderTimeslip.StartTimeRsv.Value.Seconds);

                if (OrderTimeslip.Order.JobDuration == 1)
                {
                    //update timeslip template xml tag in order
                }
                else
                {
                    //update timeslip template xml tag in order
                }

                TimeslipStart = Utility.ToEndOfDay(workStart);
                DayStatusWithData dayStatusWithData = await resetStatusWeek(OrderTimeslip.Note, OrderTimeslip.Order.JobDuration, OrderTimeslip.Order.CustomerID, TimeslipStart, OrderTimeslip.DayOfWeek, TimeslipStatus.DrawStyle.Timeslip);


                var data = new { OrderTimeslip.OrderTimeslipID, OrderTimeslip.OrderID, OrderTimeslip.EmployeeID, OrderTimeslip.Order.CustomerSiteID, OrderTimeslip.Order.CustomerID, OrderTimeslip.Order.Customer.CustomerName, OrderTimeslip.Order.Customer.CompanyName, OrderTimeslip.Order.PONumber, OrderTimeslip.CustomerSiteJobLocationID, OrderTimeslip.LabourClassificationID, OrderTimeslip.Phone, OrderTimeslip.WorkStartRsv, OrderTimeslip.WorkEndRsv, OrderTimeslip.StartTimeRsv, OrderTimeslip.EndTimeRsv, OrderTimeslip.RollOver, OrderTimeslip.RollOverStart, OrderTimeslip.RollOverTime, OrderTimeslip.Reporting, OrderTimeslip.Comment, OrderTimeslip.DispatchNote, OrderTimeslip.PayRate, OrderTimeslip.InvoiceRate, OrderTimeslip.HrsOT, OrderTimeslip.HrsReg, OrderTimeslip.HrsTotal, OrderTimeslip.GrossPay, OrderTimeslip.WithHolding, OrderTimeslip.NetPay, OrderTimeslip.ItemsDue, OrderTimeslip.Advances, OrderTimeslip.CreditBalance, OrderTimeslip.BalanceFwd, OrderTimeslip.NetPay1, OrderTimeslip.ReturnItems, OrderTimeslip.AvailableBalance, OrderTimeslip.PayNow, OrderTimeslip.ClosingBalance, Created = OrderTimeslip.Created.Value.ToString("dd-MMM-yyyy hh:mm:ss tt"), OrderTimeslip.IsEnable, LastUpdated = OrderTimeslip.LastUpdated.HasValue ? OrderTimeslip.LastUpdated.Value.ToString("dd-MMM-yyyy hh:mm:ss tt") : "", OrderTimeslip.Remark, OrderTimeslip.SortOrder, CreatedByName = await UserHelper.GetUserName(OrderTimeslip.Order.CreatedBy), LastUpdatedByName = await UserHelper.GetUserName(OrderTimeslip.Order.LastUpdatedBy), DayStatusData = dayStatusWithData };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }


        //[Authorize(Orders = "1")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOrderTimeslip([FromBody]OrderTimeslip OrderTimeslip)
        {
            if (OrderTimeslip != null)
            {
                OrderTimeslip newOrderTimeslip = await _orderTimeslipService.GetByID(OrderTimeslip.OrderTimeslipID);

                newOrderTimeslip.StartTimeRsv = OrderTimeslip.StartTimeRsv;
                newOrderTimeslip.WorkStartRsv = OrderTimeslip.WorkStartRsv;
                newOrderTimeslip.CustomerSiteJobLocationID = OrderTimeslip.CustomerSiteJobLocationID;
                newOrderTimeslip.DispatchNote = OrderTimeslip.DispatchNote;
                newOrderTimeslip.Comment = OrderTimeslip.Comment;
                newOrderTimeslip.Reporting = OrderTimeslip.Reporting;
                newOrderTimeslip.Phone = OrderTimeslip.Phone;
                newOrderTimeslip.LabourClassificationID = OrderTimeslip.LabourClassificationID;
                newOrderTimeslip.RollOver = OrderTimeslip.RollOver;
                newOrderTimeslip.RollOverStart = OrderTimeslip.RollOverStart;

                newOrderTimeslip.Note = TStatus.Composite;

                newOrderTimeslip.EmployeeID = OrderTimeslip.EmployeeID;
                newOrderTimeslip.PayRate = OrderTimeslip.PayRate;
                newOrderTimeslip.InvoiceRate = OrderTimeslip.InvoiceRate;
                newOrderTimeslip.HrsOT = OrderTimeslip.HrsOT;
                newOrderTimeslip.HrsReg = OrderTimeslip.HrsReg;
                newOrderTimeslip.HrsTotal = OrderTimeslip.HrsTotal;
                newOrderTimeslip.GrossPay = OrderTimeslip.GrossPay;
                newOrderTimeslip.NetPay = OrderTimeslip.NetPay;
                newOrderTimeslip.WithHolding = OrderTimeslip.WithHolding;
                newOrderTimeslip.ItemsDue = OrderTimeslip.ItemsDue;
                newOrderTimeslip.Advances = OrderTimeslip.Advances;
                newOrderTimeslip.CreditBalance = OrderTimeslip.CreditBalance;
                newOrderTimeslip.BalanceFwd = OrderTimeslip.BalanceFwd;
                newOrderTimeslip.NetPay1 = OrderTimeslip.NetPay1;
                newOrderTimeslip.ReturnItems = OrderTimeslip.ReturnItems;
                newOrderTimeslip.AvailableBalance = OrderTimeslip.AvailableBalance;
                newOrderTimeslip.PayNow = OrderTimeslip.PayNow;
                newOrderTimeslip.ClosingBalance = OrderTimeslip.ClosingBalance;
                newOrderTimeslip.BillState = OrderTimeslip.BillState;

                newOrderTimeslip.IsEnable = OrderTimeslip.IsEnable;
                newOrderTimeslip.Remark = OrderTimeslip.Remark;
                newOrderTimeslip.LastUpdated = DateTime.Now;
                newOrderTimeslip.LastUpdatedBy = RoleHelper.GetCurrentUserID;

                newOrderTimeslip.XmlNote = XmlNodeForTimeslip(newOrderTimeslip.Note, newOrderTimeslip, false);

                response = await _orderTimeslipService.Update(newOrderTimeslip);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public async Task<IHttpActionResult> GetTimeslipStatus(int JobDuration, long CustomerID, DateTime WorkStartRsv, TimeSpan StartTimeRsv, long LabourClassificationID, string dayOfWeek)
        {
            DateTime TimeslipStart = Utility.NullDateValue;

            DateTime workStart = new DateTime(WorkStartRsv.Year, WorkStartRsv.Month, WorkStartRsv.Day, StartTimeRsv.Hours, StartTimeRsv.Minutes, StartTimeRsv.Seconds);

            if (JobDuration == 1)
            {
                //update timeslip template xml tag in order
            }
            else
            {
                //update timeslip template xml tag in order
            }

            TimeslipStart = Utility.ToEndOfDay(workStart);
            DayStatusWithData dayStatusWithData = await resetStatusWeek(JobDuration, CustomerID, LabourClassificationID, TimeslipStart, dayOfWeek, TimeslipStatus.DrawStyle.Order);

            return Ok(dayStatusWithData);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetTimeslipStatusAfterClick([FromBody] RequestData RequestData)
        {
            int JobDuration = RequestData.JobDuration;
            long CustomerID = RequestData.CustomerID;
            DateTime WorkStartRsv = RequestData.WorkStartRsv;
            TimeSpan StartTimeRsv = RequestData.StartTimeRsv;
            long LabourClassificationID = RequestData.LabourClassificationID;
            string dayOfWeek = RequestData.DayOfWeek;
            List<DayStatus> DayStatusList = RequestData.DayStatusList;

            TStatus.SetCurrentStatus(DayStatusList);

            DayStatusData.DayStatusList = TStatus.DayStatusList;

            return Ok(DayStatusData);
        }

        public byte[] CustomerCurrentWorkWeek
        {
            get
            {
                return null;
            }

        }



        public static DayStatusWithData DayStatusData { get; set; }
        public static TimeslipStatus TStatus { get; set; }

        DayStatusWithData dayStatusWithData = new DayStatusWithData();

        private async Task<DayStatusWithData> resetStatusWeek(int JobDuration, long CustomerID, long LabourClassificationID, DateTime workStart, string dayOfWeek, TimeslipStatus.DrawStyle drawStyle)
        {
            List<DayStatus> dayStatus = new List<DayStatus>();
            byte[] customerStatusWeek = null;

            if (CustomerID > 0)
                customerStatusWeek = CustomerCurrentWorkWeek;

            bool isOneDay = true;

            if (JobDuration == 1)
                isOneDay = true;
            else
                isOneDay = false;


            string labourClassificationName = string.Empty;
            if (CustomerID > 0)
            {
                LabourClassification labourClassification = await _labourClassificationService.GetByID(LabourClassificationID);
                labourClassificationName = labourClassification.LabourClassificationName;

                bool isDefaultToRollover = (LabourClassificationID > 0) && TimeslipStatus.IsRolloverByDefaultOnNewTimeslip(isOneDay, labourClassificationName);

                TimeslipStatus timeslipStatus = null;

                if (drawStyle == TimeslipStatus.DrawStyle.Order)
                    timeslipStatus = new TimeslipStatus(TimeslipStatus.GetDefaultComposite(labourClassificationName, isOneDay, workStart, customerStatusWeek, dayOfWeek), TimeslipStatus.RESERVED, customerStatusWeek, dayOfWeek, TimeslipStatus.DrawStyle.Order);
                else
                    timeslipStatus = new TimeslipStatus(TimeslipStatus.GetDefaultComposite(labourClassificationName, isOneDay, workStart, customerStatusWeek, dayOfWeek), TimeslipStatus.RESERVED, customerStatusWeek, dayOfWeek, TimeslipStatus.DrawStyle.Timeslip);


                dayStatusWithData.DayStatusList = timeslipStatus.DayStatusList;

                dayStatusWithData.ConfirmedThrough = timeslipStatus.ConfirmedThrough;
                dayStatusWithData.RollOverDate = timeslipStatus.RollOverDate;
                dayStatusWithData.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;

                //if (drawStyle == TimeslipStatus.DrawStyle.Order)
                //{
                //    dayStatusWithData.RollOverDate = timeslipStatus.RollOverDate;
                //    dayStatusWithData.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;
                //}
                //else
                //{
                //    dayStatusWithData.RollOverDate = TempRollOverDate;
                //    dayStatusWithData.IsToBeRolledOver = TempIsToBeRolledOver;
                //}


                TStatus = timeslipStatus;
                DayStatusData = dayStatusWithData;

                if (isDefaultToRollover)
                {
                    timeslipStatus.RollOverDate = TimeslipStatus.GetDefaultRolloverDate(isOneDay, workStart, timeslipStatus.EndOfWeek);

                    dayStatusWithData.DayStatusList = timeslipStatus.GetRolloverDayStatus(timeslipStatus.RollOverDate);

                    dayStatusWithData.ConfirmedThrough = timeslipStatus.ConfirmedThrough;
                    dayStatusWithData.RollOverDate = timeslipStatus.RollOverDate;
                    dayStatusWithData.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;

                    TStatus = timeslipStatus;
                    DayStatusData = dayStatusWithData;
                    return dayStatusWithData;

                }

                return dayStatusWithData;
            }

            return null;
        }


        private async Task<DayStatusWithData> resetStatusWeek(string Composite, int JobDuration, long CustomerID, DateTime workStart, string dayOfWeek, TimeslipStatus.DrawStyle drawStyle)
        {
            List<DayStatus> dayStatus = new List<DayStatus>();
            byte[] customerStatusWeek = null;

            if (CustomerID > 0)
                customerStatusWeek = CustomerCurrentWorkWeek;

            bool isOneDay = true;

            if (JobDuration == 1)
                isOneDay = true;
            else
                isOneDay = false;


            string labourClassificationName = string.Empty;
            if (CustomerID > 0)
            {
                TimeslipStatus timeslipStatus = new TimeslipStatus(Composite, TimeslipStatus.RESERVED, customerStatusWeek, TimeslipStatus.SetDeafultDayOfWeek(dayOfWeek), TimeslipStatus.DrawStyle.Timeslip);


                dayStatusWithData.DayStatusList = timeslipStatus.DayStatusList;

                dayStatusWithData.ConfirmedThrough = timeslipStatus.ConfirmedThrough;
                dayStatusWithData.RollOverDate = timeslipStatus.RollOverDate;
                dayStatusWithData.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;

                if (TempIsToBeRolledOver == false)
                    dayStatusWithData.IsToBeRolledOver = false;
                else
                    dayStatusWithData.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;

                if (TempRollOverDate == Utility.NullDateValue)
                    dayStatusWithData.RollOverDate = Utility.NullDateValue;
                else
                    dayStatusWithData.RollOverDate = timeslipStatus.RollOverDate;


                TStatus = timeslipStatus;
                DayStatusData = dayStatusWithData;

                return dayStatusWithData;
            }

            return null;
        }


        private async Task<DayStatusWithData> resetStatusWeekForRollover(long OrderTimeslipID, string Composite, int JobDuration, long CustomerID, long LabourClassificationID, DateTime workStart, string dayOfWeek, TimeslipStatus.DrawStyle drawStyle)
        {
            DayStatusWithData dayStatusWithDataNew = new DayStatusWithData();
            List<DayStatus> dayStatus = new List<DayStatus>();
            byte[] customerStatusWeek = null;

            if (CustomerID > 0)
                customerStatusWeek = CustomerCurrentWorkWeek;

            bool isOneDay = true;

            if (JobDuration == 1)
                isOneDay = true;
            else
                isOneDay = false;


            string labourClassificationName = string.Empty;
            if (CustomerID > 0)
            {
                LabourClassification labourClassification = await _labourClassificationService.GetByID(LabourClassificationID);
                labourClassificationName = labourClassification.LabourClassificationName;

                bool isDefaultToRollover = (LabourClassificationID > 0) && TimeslipStatus.IsRolloverByDefaultOnNewTimeslip(isOneDay, labourClassificationName);


                TimeslipStatus timeslipStatus = new TimeslipStatus(Composite, TimeslipStatus.RESERVED, customerStatusWeek, TimeslipStatus.SetDeafultDayOfWeek(dayOfWeek), TimeslipStatus.DrawStyle.Order);


                dayStatusWithDataNew.DayStatusList = timeslipStatus.DayStatusList;

                dayStatusWithDataNew.ConfirmedThrough = timeslipStatus.ConfirmedThrough;
                dayStatusWithDataNew.RollOverDate = timeslipStatus.RollOverDate;
                dayStatusWithDataNew.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;


                TStatus = timeslipStatus;
                DayStatusData = dayStatusWithDataNew;

                if (isDefaultToRollover)
                {
                    timeslipStatus.RollOverDate = TimeslipStatus.GetDefaultRolloverDate(isOneDay, workStart, timeslipStatus.EndOfWeek);

                    dayStatusWithDataNew.DayStatusList = timeslipStatus.GetRolloverDayStatus(timeslipStatus.RollOverDate);

                    dayStatusWithDataNew.ConfirmedThrough = timeslipStatus.ConfirmedThrough;
                    dayStatusWithDataNew.RollOverDate = timeslipStatus.RollOverDate;
                    dayStatusWithDataNew.IsToBeRolledOver = timeslipStatus.IsToBeRolledOver;

                    TStatus = timeslipStatus;
                    DayStatusData = dayStatusWithDataNew;

                    if (!dayStatusRolloverDataList.ContainsKey(OrderTimeslipID))
                        dayStatusRolloverDataList.Add(OrderTimeslipID, TStatus);

                    return dayStatusWithDataNew;

                }

                if (!dayStatusRolloverDataList.ContainsKey(OrderTimeslipID))
                    dayStatusRolloverDataList.Add(OrderTimeslipID, TStatus);

                return dayStatusWithDataNew;
            }

            return null;
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetTimeslipStatus(DateTime WorkStartRsv, TimeSpan StartTimeRsv)
        {
            DateTime TimeslipStart = Utility.NullDateValue;

            DateTime workStart = new DateTime(WorkStartRsv.Year, WorkStartRsv.Month, WorkStartRsv.Day, StartTimeRsv.Hours, StartTimeRsv.Minutes, StartTimeRsv.Seconds);



            TimeslipStart = Utility.ToEndOfDay(workStart);

            //DayStatusWithData dayStatusWithData = await resetStatusWeek(TempComposite, TempJobDuration, TempCustomerID, TimeslipStart, TempDayOfWeek, TimeslipStatus.DrawStyle.Timeslip);

            DayStatusWithData dayStatusWithData = await resetStatusWeek(TempJobDuration, TempCustomerID, TempLabourClassificationID, TimeslipStart, TempDayOfWeek, TimeslipStatus.DrawStyle.Timeslip);

            return Ok(dayStatusWithData);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetRollOverDate(bool RollOver)
        {
            if (RollOver)
            {
                TStatus.RollOverDate = TimeslipStatus.GetDefaultRolloverDate(TStatus.IsOneDay, TStatus.ConfirmedThrough, TStatus.EndOfWeek);
                //TStatus. = true;
                TimeslipStatus timeslipStatus = TStatus;
            }
            else
            {
                TStatus.RollOverDate = TimeslipStatus.GetDefaultRolloverDate(TStatus.IsOneDay, Utility.NullDateValue, TStatus.EndOfWeek);
                //TStatus.IsToBeRolledOver = false;
                TimeslipStatus timeslipStatus = TStatus;
            }
            return Ok(TStatus.RollOverDate);
        }


        [HttpGet]
        public async Task<IHttpActionResult> ReCalculateConfirmedThrough()
        {
            TStatus.getReCalculateConfirmedThrough(TStatus.DayStatusList);

            DayStatusData.DayStatusList = TStatus.DayStatusList;

            return Ok(DayStatusData);
        }



        #endregion

        #region Rollover

        class RolloverParam
        {
            public string Composite { get; set; }
            public int JobDuration { get; set; }
            public long CustomerID { get; set; }
            public long LabourClassificationID { get; set; }
            public DateTime workStart { get; set; }
            public string dayOfWeek { get; set; }
            public TimeslipStatus.DrawStyle drawStyle { get; set; }
        }

        private static Dictionary<long, TimeslipStatus> dayStatusRolloverDataList { get; set; }

        private static Dictionary<long, Order> OrderDictionary { get; set; }

        private static long PrevOrderId = 0;

        public static long CurrentOrderId = 0;

        [HttpPost]
        public async Task<IHttpActionResult> MakeRollover([FromBody] RequestData RequestData)
        {
            DateTime _today = Utility.NullDateValue;
            DateTime _tomorrow = Utility.NullDateValue;

            DateTime dispatchDate = RequestData.DispatchDate;

            _today = Utility.ToStartOfDay(dispatchDate);
            _tomorrow = _today.AddDays(1.0);

            DateTime tomorrow = _tomorrow;

            foreach (var item in OrderDictionary)
            {
                long orderID = (long)item.Key;

                List<OrderTimeslip> oTimeslipList = await _orderTimeslipService.GetAllOrderTimeslipsByOrderID(orderID);

                oTimeslipList = oTimeslipList.Where(x => x.IsDelete == false).ToList();

                oTimeslipList = oTimeslipList.Where(x => x.EmployeeID != null && x.LabourClassificationID != null && x.CustomerSiteJobLocationID != null).ToList();


                foreach (var ot in oTimeslipList)
                {
                    //DateTime TimeslipStart = Utility.NullDateValue;

                    //DateTime workStart = new DateTime(dispatchDate.Year, dispatchDate.Month, dispatchDate.Day, ot.StartTimeRsv.Value.Hours, ot.StartTimeRsv.Value.Minutes, ot.StartTimeRsv.Value.Seconds);


                    //TimeslipStart = Utility.ToEndOfDay(workStart);
                    //DayStatusWithData dayStatusWithData = await resetStatusWeek(ot.Order.JobDuration, ot.Order.CustomerID, ot.LabourClassificationID.Value, TimeslipStart, ot.DayOfWeek, TimeslipStatus.DrawStyle.Order);
                    //string Composite = TStatus.Composite;

                    var TStatusOld = dayStatusRolloverDataList[ot.OrderTimeslipID];

                    TStatusOld.IsToRollTomorrow = ot.RollOverStart == _tomorrow;

                    if (TStatusOld.IsToRollTomorrow)
                    {
                        if (PrevOrderId != orderID)
                        {
                            var order = OrderDictionary[orderID];

                            Order tempOrder = new Order { Comment = order.Comment, ContactName = order.ContactName, CustomerID = order.CustomerID, CustomerSiteID = order.CustomerSiteID, CustomerSiteJobLocationID = order.CustomerSiteJobLocationID, DispatchNote = order.DispatchNote, EndTimeCust = order.EndTimeCust, EndTimeRsv = order.EndTimeRsv, IsDelete = order.IsDelete, IsEnable = order.IsEnable, JobAddress = order.JobAddress, JobDuration = order.JobDuration, JobLocation = order.JobLocation, JobNote = order.JobNote, LabourClassificationID = order.LabourClassificationID, OTPerDay = order.OTPerDay, OTPerWeek = order.OTPerWeek, People = order.People, Phone = order.Phone, PONumber = order.PONumber, Remark = order.Remark, Reporting = order.Reporting, SortOrder = order.SortOrder, StartTimeCust = order.StartTimeCust, StartTimeRsv = order.StartTimeRsv, WorkEndCust = order.WorkEndCust, WorkEndRsv = order.WorkEndRsv, WorkStartCust = order.WorkStartCust, WorkStartRsv = order.WorkStartRsv };
                            response = await _orderService.Create(tempOrder);
                            CurrentOrderId = Convert.ToInt64(response);

                            if (Convert.ToInt64(response) > 0)
                            {
                                OrderTimeslip oTimeslip = new OrderTimeslip { OrderID = Convert.ToInt64(response), AccountNo = ot.AccountNo, Address = ot.Address, Advances = ot.Advances, AvailableBalance = ot.AvailableBalance, BalanceFwd = ot.BalanceFwd, BillState = ot.BillState, ClosingBalance = ot.ClosingBalance, Comment = ot.Comment, CompanyName = ot.CompanyName, CreditBalance = ot.CreditBalance, CustomerSiteJobLocationID = ot.CustomerSiteJobLocationID, DayOfWeek = ot.DayOfWeek, DispatchNote = ot.DispatchNote, EmployeeID = ot.EmployeeID, EmployeeName = ot.EmployeeName, EndTimeRsv = ot.EndTimeRsv, GrossPay = ot.GrossPay, HrsOT = ot.HrsOT, HrsReg = ot.HrsReg, HrsTotal = ot.HrsTotal, InvoiceRate = ot.InvoiceRate, IsDelete = ot.IsDelete, IsEnable = ot.IsEnable, IsOneDay = ot.IsOneDay, JobLocation = ot.JobLocation, ItemsDue = ot.ItemsDue, LabourClassificationID = ot.LabourClassificationID, NetPay = ot.NetPay, NetPay1 = ot.NetPay1, Note = TStatus.Composite, PayNow = ot.PayNow, PayRate = ot.PayRate, Phone = ot.Phone, Remark = ot.Remark, Reporting = ot.Reporting, ReturnItems = ot.ReturnItems, Roll = ot.Roll, RollOver = ot.RollOver, RollOverStart = ot.RollOverStart.Value.AddDays(7), RollOverTime = ot.RollOverTime, SortOrder = ot.SortOrder, StartTimeRsv = ot.StartTimeRsv, Stat = ot.Stat, Week = ot.Week, WeekEnd = ot.WeekEnd, WeekStart = ot.WeekStart, WithHolding = ot.WithHolding, WorkEndRsv = ot.WorkEndRsv, WorkStartRsv = ot.WorkStartRsv, XmlNote = ot.XmlNote };
                                await _orderTimeslipService.Create(oTimeslip);
                            }
                        }
                        else
                        {
                            OrderTimeslip oTimeslip = new OrderTimeslip { OrderID = CurrentOrderId, AccountNo = ot.AccountNo, Address = ot.Address, Advances = ot.Advances, AvailableBalance = ot.AvailableBalance, BalanceFwd = ot.BalanceFwd, BillState = ot.BillState, ClosingBalance = ot.ClosingBalance, Comment = ot.Comment, CompanyName = ot.CompanyName, CreditBalance = ot.CreditBalance, CustomerSiteJobLocationID = ot.CustomerSiteJobLocationID, DayOfWeek = ot.DayOfWeek, DispatchNote = ot.DispatchNote, EmployeeID = ot.EmployeeID, EmployeeName = ot.EmployeeName, EndTimeRsv = ot.EndTimeRsv, GrossPay = ot.GrossPay, HrsOT = ot.HrsOT, HrsReg = ot.HrsReg, HrsTotal = ot.HrsTotal, InvoiceRate = ot.InvoiceRate, IsDelete = ot.IsDelete, IsEnable = ot.IsEnable, IsOneDay = ot.IsOneDay, JobLocation = ot.JobLocation, ItemsDue = ot.ItemsDue, LabourClassificationID = ot.LabourClassificationID, NetPay = ot.NetPay, NetPay1 = ot.NetPay1, Note = TStatus.Composite, PayNow = ot.PayNow, PayRate = ot.PayRate, Phone = ot.Phone, Remark = ot.Remark, Reporting = ot.Reporting, ReturnItems = ot.ReturnItems, Roll = ot.Roll, RollOver = ot.RollOver, RollOverStart = ot.RollOverStart.Value.AddDays(7), RollOverTime = ot.RollOverTime, SortOrder = ot.SortOrder, StartTimeRsv = ot.StartTimeRsv, Stat = ot.Stat, Week = ot.Week, WeekEnd = ot.WeekEnd, WeekStart = ot.WeekStart, WithHolding = ot.WithHolding, WorkEndRsv = ot.WorkEndRsv, WorkStartRsv = ot.WorkStartRsv, XmlNote = ot.XmlNote };
                            await _orderTimeslipService.Create(oTimeslip);
                        }

                        PrevOrderId = orderID;

                    }
                }
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetRollovers(FormDataCollection form)
        {
            int pageNumber = Convert.ToInt32(form.Get("pageNumber"));
            int pageSize = Convert.ToInt32(form.Get("pageSize"));

            string globalSearch = (Convert.ToString(form.Get("globalSearch")) == "") ? null : Convert.ToString(form.Get("globalSearch"));

            string dispatchDate = (Convert.ToString(form.Get("dispatchDate")) == "") ? null : Convert.ToString(form.Get("dispatchDate"));

            PagedData<OrderTimeslip> PagedData = new PagedData<OrderTimeslip>();

            if (!string.IsNullOrEmpty(dispatchDate))
            {
                DateTime today = dispatchDate == null ? DateTime.Now : DateTime.ParseExact(dispatchDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                TimeslipStatus.DayOfWeek = "SATURDAY";

                DateTime startOfWeek = Utility.StartOfWeek(today);

                DateTime endOfWeek = Utility.EndOfWeek(today);

                int status_index = ((System.TimeSpan)Utility.ToStartOfDay(today).Subtract(startOfWeek)).Days;

                SearchOrder search = new SearchOrder
                {
                    GlobalSearch = globalSearch,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Index = status_index,
                    SOW = startOfWeek,
                    EOW = endOfWeek
                };

                PagedData = await _orderTimeslipService.Get(search);
            }
            else
            {
                //SearchOrder search = new SearchOrder
                //{
                //    GlobalSearch = globalSearch,
                //    PageNumber = pageNumber,
                //    PageSize = pageSize
                //};

                //PagedData = await _orderTimeslipService.GetAll(search);
            }

            if (PagedData != null)
            {
                Dictionary<long, Order> orderDictionary = new Dictionary<long, Order>();
                List<OrderTimeslip> orderTimeslipList = new List<OrderTimeslip>();
                dayStatusRolloverDataList = new Dictionary<long, TimeslipStatus>();

                foreach (var x in PagedData.Items)
                {
                    if (!orderDictionary.ContainsKey(x.OrderID))
                    {
                        orderDictionary.Add(x.OrderID, x.Order);
                    }

                    DateTime TimeslipStart = Utility.NullDateValue;

                    DateTime workStart = new DateTime(x.WorkStartRsv.Value.Year, x.WorkStartRsv.Value.Month, x.WorkStartRsv.Value.Day, x.StartTimeRsv.Value.Hours, x.StartTimeRsv.Value.Minutes, x.StartTimeRsv.Value.Seconds);

                    TimeslipStart = Utility.ToEndOfDay(workStart);

                    DayStatusWithData dayStatusData = new DayStatusWithData();
                    dayStatusData = await resetStatusWeekForRollover(x.OrderTimeslipID, x.Note, x.Order.JobDuration, x.Order.CustomerID, x.LabourClassificationID.Value, TimeslipStart, x.DayOfWeek, TimeslipStatus.DrawStyle.Order);

                    string createByName = await UserHelper.GetUserName(x.CreatedBy);

                    var add = x.Order.Customer.CustomerSites.Where(y => y.IsPrimary == true).FirstOrDefault();

                    string address = add.PrAddress1 + ", " + add.PrAddress2 + ", " + add.PrCity.CityName + ", " + add.PrCity.State.StateName + ", " + add.PrCity.State.Country.CountryName;


                    orderTimeslipList.Add(new OrderTimeslip
                    {
                        OrderID = x.OrderID,
                        OrderTimeslipID = x.OrderTimeslipID,
                        CompanyName = x.Order.Customer.CompanyName,
                        Address = address,
                        Phone = x.Phone,
                        CreatedByName = createByName,
                        EmployeeName = x.Employee.FirstName + " " + x.Employee.MiddleName + " " + x.Employee.LastName,
                        JobLocation = x.CustomerSiteJobLocation.JobLocation,
                        StartTimeRsv = x.StartTimeRsv,
                        DayStatusData = dayStatusData,
                        RollOverStart = x.RollOverStart,
                        AccountNo = x.Employee.AccountNo
                    });
                }

                OrderDictionary = orderDictionary;

                var data = new { Items = orderTimeslipList, Count = PagedData.Count };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> ChangeStatusOnRollover([FromBody] RequestData RequestData)
        {
            long OrderTimeslipID = RequestData.OrderTimeslipID;
            List<DayStatus> DayStatusList = RequestData.DayStatusList;

            var TStatus = dayStatusRolloverDataList[OrderTimeslipID];

            TStatus.SetCurrentStatus(DayStatusList);

            DayStatusData.DayStatusList = TStatus.DayStatusList;

            OrderTimeslip newOrderTimeslip = await _orderTimeslipService.GetByID(OrderTimeslipID);

            newOrderTimeslip.Note = TStatus.Composite;

            response = await _orderTimeslipService.Update(newOrderTimeslip);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IHttpActionResult> YelloButtonClick([FromBody] RequestData RequestData)
        {
            DateTime _today = Utility.NullDateValue;
            DateTime _tomorrow = Utility.NullDateValue;

            long OrderTimeslipID = RequestData.OrderTimeslipID;
            List<DayStatus> DayStatusList = RequestData.DayStatusList;
            DateTime dispatchDate = RequestData.DispatchDate;

            var TStatus = dayStatusRolloverDataList[OrderTimeslipID];

            _today = Utility.ToStartOfDay(dispatchDate);
            _tomorrow = _today.AddDays(1.0);

            DateTime tomorrow = _tomorrow;

            if (tomorrow.DayOfWeek == System.DayOfWeek.Saturday)
            {
                if (tomorrow.CompareTo(TStatus.LastDayInTimeslip) <= 0)
                    setStatus(tomorrow, TimeslipStatus.OFF, TStatus);
                tomorrow = tomorrow.AddDays(1.0); // set to Sunday
            }
            if (tomorrow.DayOfWeek == System.DayOfWeek.Sunday)
            {
                if (tomorrow.CompareTo(TStatus.LastDayInTimeslip) <= 0)
                    setStatus(tomorrow, TimeslipStatus.OFF, TStatus);
                tomorrow = tomorrow.AddDays(1.0); // set to Monday
            }
            if (tomorrow > TStatus.LastDayInTimeslip)
                TStatus.RollOverDate = tomorrow;

            TimeslipStatus TStatusNew = setStatus(tomorrow, TimeslipStatus.REQUESTED, TStatus);



            TStatusNew.SetCurrentStatus(DayStatusList);

            DayStatusData.DayStatusList = TStatusNew.DayStatusList;

            OrderTimeslip newOrderTimeslip = await _orderTimeslipService.GetByID(OrderTimeslipID);

            newOrderTimeslip.Note = TStatusNew.Composite;

            response = await _orderTimeslipService.Update(newOrderTimeslip);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> BlackButtonClick([FromBody] RequestData RequestData)
        {
            DateTime _today = Utility.NullDateValue;
            DateTime _tomorrow = Utility.NullDateValue;

            long OrderTimeslipID = RequestData.OrderTimeslipID;
            List<DayStatus> DayStatusList = RequestData.DayStatusList;
            DateTime dispatchDate = RequestData.DispatchDate;

            var TStatus = dayStatusRolloverDataList[OrderTimeslipID];

            _today = Utility.ToStartOfDay(dispatchDate);
            _tomorrow = _today.AddDays(1.0);

            TimeslipStatus TStatusNew = setStatus(_tomorrow, TimeslipStatus.FINISHED, TStatus);


            TStatusNew.SetCurrentStatus(DayStatusList);

            DayStatusData.DayStatusList = TStatusNew.DayStatusList;

            OrderTimeslip newOrderTimeslip = await _orderTimeslipService.GetByID(OrderTimeslipID);

            newOrderTimeslip.Note = TStatusNew.Composite;

            response = await _orderTimeslipService.Update(newOrderTimeslip);
            return Ok(response);
        }


        private byte _selectedLabelInitialStatus = 0;

        private TimeslipStatus setStatus(DateTime day, byte newStatus, TimeslipStatus TStatus)
        {
            bool isRoll = !TStatus.IsDateInWeek(day);
            int index = TStatus.getIndexOfDate(day, isRoll);

            _selectedLabelInitialStatus = isRoll ? TStatus.getStatusRollByIndex(index) : TStatus.getStatusByIndex(index);

            if (isRoll)
                TStatus.setStatusToRollToByIndex(index, newStatus);
            else if (_selectedLabelInitialStatus != TimeslipStatus.SCRATCHED)
                TStatus.setStatusByIndex(index, newStatus);

            if (!isRoll && (newStatus == TimeslipStatus.FINISHED) && (_selectedLabelInitialStatus == TimeslipStatus.SCRATCHED))
            {
                bool wasToBeRolled = TStatus.IsToBeRolledOver;

                TStatus.finish(TStatus.getDateOfIndex(index, false));

                TStatus.recalculateConfirmedThrough();

                //TStatus.getReCalculateConfirmedThrough(TStatus.DayStatusList);

                //DayStatusData.DayStatusList = TStatus.DayStatusList;
            }

            return TStatus;
        }


        #endregion

        #region Utility

        public string XmlNodeForTimeslip(string composite, OrderTimeslip ot, bool isFromCreate)
        {

            string xmlNote = string.Empty;
            xmlNote = CommonHelper.CreateXmlData(composite, "Place", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Msg", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DispatchNote", ot.DispatchNote);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ReportTo", ot.Reporting);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Phone", ot.Phone);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Dispatcher", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Completer", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Ammender", "");

            return xmlNote;
        }

        public string XmlNodeForOrder(Order order, bool isFromCreate)
        {

            string xmlNote = string.Empty;
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Phone", order.Phone);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "PlacedBy", order.LastUpdatedByName);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "SkillReqd", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "ReportTo", order.Reporting);
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "EquipReqd", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Offset", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "Duration", order.JobDuration.ToString());
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "TimeslipsWeekly", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "QtyReqd", "");
            xmlNote = CommonHelper.CreateXmlData(xmlNote, "DispatchNote", order.DispatchNote);


            return xmlNote;
        }

        #endregion
    }
}
