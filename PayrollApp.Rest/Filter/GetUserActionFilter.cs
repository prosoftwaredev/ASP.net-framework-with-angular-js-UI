using PayrollApp.Core.Data.System;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PayrollApp.Rest.Filter
{
    public class GetUserActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)actionContext.RequestContext.Principal;

            if (principal.Claims.Count() > 0)
            {
                var id = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier);
                string newID = id.Select(x => x.Value).Take(1).Single();
                RoleHelper.GetCurrentUserID = Convert.ToInt64(newID);
            }
            else
            {
                RoleHelper.GetCurrentUserID = 1; //temp
            }


            //if (User is ClaimsPrincipal)
            //{
            //    var user = User as ClaimsPrincipal;
            //    var claims = user.Claims.ToList();
            //    var id = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier);
            //    string newID = id.Select(x => x.Value).Single();
            //}
        }
 
        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    var objectContent = actionExecutedContext.Response.Content as ObjectContent;
        //    if (objectContent != null)
        //    {
        //        var type = objectContent.ObjectType; //type of the returned object
        //        var value = objectContent.Value; //holding the returned value
        //    }
        //}
    }
}