using PayrollApp.Core.Data.System;
using PayrollApp.Service.IServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace PayrollApp.Rest.Logger
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private readonly IExcLoggerService _exceptionLoggerServiceService;

        public UnhandledExceptionLogger(IExcLoggerService exceptionLoggerService)
        {
            _exceptionLoggerServiceService = exceptionLoggerService;
        }

        public UnhandledExceptionLogger() { }

        public async override Task LogAsync(ExceptionLoggerContext context, CancellationToken token)
        {
            var log = context.Exception;

            var controller = context.ExceptionContext.ControllerContext.Controller;

            var action = ((System.Web.Http.ApiController)context.ExceptionContext.ControllerContext.Controller).ActionContext.ActionDescriptor.ActionName;

            ExcLogger logger = new ExcLogger
            {
                Message = log.Message,
                Source = log.Source,
                HResult = Convert.ToString(log.HResult),
                StackTrace = log.StackTrace,
                InnerException = Convert.ToString(log.InnerException),
                Controller = Convert.ToString(controller),
                Action = action
            };

            await _exceptionLoggerServiceService.Create(logger);

            //StringBuilder sb = new StringBuilder();

            //sb.AppendLine("Controller: " + controller);
            //sb.AppendLine("\n");
            //sb.AppendLine("Action: " + action);
            //sb.AppendLine("\n");
            //sb.AppendLine("Message: " + log.Message);
            //sb.AppendLine("\n");
            //sb.AppendLine("HelpLink: " + log.HelpLink);
            //sb.AppendLine("\n");
            //sb.AppendLine("HResult: " + log.HResult);
            //sb.AppendLine("\n");
            //sb.AppendLine("Source: " + log.Source);
            //sb.AppendLine("\n");
            //sb.AppendLine("StackTrace: " + log.StackTrace);
            //sb.AppendLine("\n");
            //sb.AppendLine("InnerException: " + log.InnerException);
            //sb.AppendLine("\n");
            //sb.AppendLine("\n\n\n\n\n");


            //if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Logging")))
            //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Logging"));

            //if (!File.Exists(HttpContext.Current.Server.MapPath("~/Files/Users/" +  DateTime.Now.ToShortDateString() + ".txt")))
            //    File.Create(HttpContext.Current.Server.MapPath("~/Files/Users/" + DateTime.Now.ToShortDateString() + ".txt"));

            //string logFile = HttpContext.Current.Server.MapPath("~/Files/Logging/" + DateTime.Now.ToShortDateString() + ".txt");

            //File.AppendAllLines(logFile, new[] { sb.ToString() });
        }
    }
}