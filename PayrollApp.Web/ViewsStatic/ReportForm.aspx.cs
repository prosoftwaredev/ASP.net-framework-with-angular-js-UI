using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class ViewsStatic_Report : System.Web.UI.Page
{
    enum ReportType
    {
        CashMachineTxns= 1,
        CustomerSales = 2,
        DailyDispatch = 3,
        DailyDispatchByCustomer = 4,
        DailySales = 5,
        DormantAccounts = 6,
        MissingTimeslips = 7,
        WeeklyPayrollByEmployee = 8,
        WeeklySalesByCustomer = 9
    }

    private Dictionary<ReportType, string> _reportFiles = new Dictionary<ReportType, string>
    {
        {
          ReportType.CashMachineTxns, "CashMachineTxns"
        },
        {
          ReportType.CustomerSales, "CustomerSales"
        },
        {
          ReportType.DailyDispatch, "DailyDispatch"
        },
        {
          ReportType.DailyDispatchByCustomer, "DailyDispatchByCustomer"
        },
          {
          ReportType.DailySales, "DailySales"
        },
        {
          ReportType.DormantAccounts, "DormantAccounts"
        },
        {
          ReportType.MissingTimeslips, "MissingTimeslips"
        }
        ,
        {
          ReportType.WeeklyPayrollByEmployee, "WeeklyPayrollByEmployee"
        }
        ,
        {
          ReportType.WeeklySalesByCustomer, "WeeklySalesByCustomer"
        }
    };

    public interface IReportServerCredentials
    {
        System.Security.Principal.WindowsIdentity ImpersonationUser { get; }
        System.Net.ICredentials NetworkCredentials { get; }
        bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportType reportType;
            if (!Enum.TryParse(Request.QueryString["r"], out reportType))
            {
                throw new Exception(string.Format("Unsupported report type {0}", Request.QueryString["r"]));
            }

            if (!_reportFiles.ContainsKey(reportType))
                throw new Exception(string.Format("No file name specified for the report type {0}", reportType));

            string reportFileName = _reportFiles[reportType];

            mainReportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
            mainReportViewer.ServerReport.ReportPath = string.Format(ConfigurationManager.AppSettings["ReportPath"], reportFileName);
            mainReportViewer.ProcessingMode = ProcessingMode.Remote;
            mainReportViewer.ShowParameterPrompts = true;
            mainReportViewer.ShowRefreshButton = true;
            mainReportViewer.ShowWaitControlCancelLink = false;
            mainReportViewer.ShowBackButton = false;
            mainReportViewer.ShowCredentialPrompts = false;
            mainReportViewer.ShowPrintButton = true;
            //Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("DB_A1803F_mayur_admin", "!Matt12345", "ifc"); // e.g.: ("demo-001", "123456789", "ifc")
            Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("mcupryk-001", "mcupryk-001", "ifc"); // e.g.: ("demo-001", "123456789", "ifc")
            mainReportViewer.ServerReport.ReportServerCredentials = irsc;

            mainReportViewer.ServerReport.Refresh();
        }
    }

    public class CustomReportCredentials : IReportServerCredentials, Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get { return new System.Net.NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}