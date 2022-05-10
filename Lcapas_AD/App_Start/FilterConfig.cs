using Lcapas.Core.Logic;
using System;
using System.Web.Mvc;

namespace Lcapas.AD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CopyrightFilterAttribute());
            filters.Add(new AuthorizationRequired());
        }
    }

    public class CopyrightFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            string msg = "<!-- Lethbridge College -->";

            context.RequestContext.HttpContext.Response.Write(msg);
        }
    }

    public sealed class AuthorizationRequired : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = filterContext.ActionDescriptor.ActionName;

                bool AllowAnonymous = (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true));
                bool UserHasAccess = Functions.UserHasAccess(actionName, controllerName);

                if (!(UserHasAccess || AllowAnonymous))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "Error" }, { "action", "Unauthorised" } });
                    base.OnAuthorization(filterContext);                 
                }

                if (UserHasAccess)
                {
                    // Keep Login/Access history
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        Core.Library.UserAccessObj _UserAccess = Functions.GetUserAccess();

                        lcapasLogic.SaveLoginHistory(_UserAccess.UserId, actionName);
                    }
                }
            }
            catch (Exception ex)
            {
                // add custom error handling
                string msg = ex.ToString();
            }
        }
    }
}
