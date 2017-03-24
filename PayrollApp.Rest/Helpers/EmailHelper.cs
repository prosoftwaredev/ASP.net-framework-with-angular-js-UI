using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace PayrollApp.Rest.Helpers
{
    /// <summary>
    /// Email Helper
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// Gets the Payroll app hostname.
        /// </summary>
        public static string PayrollDomain
        {
            get { return ConfigurationManager.AppSettings["PayrollDomain"]; }
        }

        /// <summary>
        /// Gets the SMTP hostname.
        /// </summary>
        /// <value>
        /// The SMTP hostname.
        /// </value>
        public static string SmtpHostName
        {
            get { return ConfigurationManager.AppSettings["SmtpHostName"]; }
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public static string Email
        {
            get { return ConfigurationManager.AppSettings["Email"]; }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        /// <summary>
        /// Gets the name of the sender.
        /// </summary>
        /// <value>
        /// The name of the sender.
        /// </value>
        public static string SenderName
        {
            get { return ConfigurationManager.AppSettings["SenderName"]; }
        }

        /// <summary>
        /// Gets a value indicating whether [enable SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable SSL]; otherwise, <c>false</c>.
        /// </value>
        public static bool EnableSSL
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]); }
        }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public static int Port
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["Port"]); }
        }

        /// <summary>
        /// Gets the name of the web root domain.
        /// </summary>
        /// <value>
        /// The name of the web root domain.
        /// </value>
        public static string WebRootDomainName
        {
            get { return ConfigurationManager.AppSettings["WebRootDomainName"]; }
        }

       

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static bool SendMail(string email, string subject, string body)
        {
            bool success = false;

            SmtpClient smtpServer = new SmtpClient();
            MailMessage mail = new MailMessage();
            smtpServer.Credentials = new NetworkCredential(Email, Password);
            smtpServer.Host = Convert.ToString(SmtpHostName);
            smtpServer.Port = Port;
            smtpServer.EnableSsl = EnableSSL;
            mail.From = new MailAddress(SenderName);
            mail.To.Add(email);
            mail.Subject = subject.ToString();
            mail.IsBodyHtml = true;
            mail.Body = body;
            smtpServer.Send(mail);
            mail.Dispose();
            success = true;

            return true;
        }

        public static bool SendAccountEmail(List<string> emailParameters)
        {
            string subject = "Aactive Personnel Services Ltd. Account Information";
            string html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/EmailTemplates/usercreateemail.html"));

            html = html.Replace("[EMAIL]", emailParameters[0]);
            html = html.Replace("[NAME]", emailParameters[1]);
            html = html.Replace("[PASSWORD]", emailParameters[2]);

            html = string.Join(" ", Regex.Split(html, @"(?:\r\n|\n|\r)"));
            html = html.Replace(@"\", string.Empty);
            string body = html;

            return SendMail(emailParameters[0], subject, body);
        }

        public static bool SendForgotPasswordLink(string email, string name, string url)
        {
            string subject = "Aactive Personnel Services Ltd. Account Information";
            string html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/EmailTemplates/forgotpassword.html"));

            html = html.Replace("[EMAIL]", email);
            html = html.Replace("[NAME]", name);
            html = html.Replace("[LINK]", url);

            html = string.Join(" ", Regex.Split(html, @"(?:\r\n|\n|\r)"));
            html = html.Replace(@"\", string.Empty);
            string body = html;

            return SendMail(email, subject, body);
        }
    }
}