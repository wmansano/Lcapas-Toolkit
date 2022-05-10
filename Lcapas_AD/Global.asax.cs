using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Lcapas.Core.Models.Lcappsdb;
using System;
using Lcapas.AD.Controllers;
using System.Web;

namespace Lcapas.AD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_EndRequest(object sender, System.EventArgs e)
        {
            // If the user is not authorised to see this page or access this function, send them to the error page.
            //if (Response.StatusCode >= 200 && Response.StatusCode < 300)
            //{
            //    var routeData = new RouteData();
            //    routeData.Values.Add("controller", "Error");
            //    routeData.Values.Add("action", "Index");
            //    IController controller = new ErrorController();
            //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            //    Response.End();
            //}
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");
        //    routeData.Values.Add("action", "Index");
        //    IController controller = new ErrorController();
        //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

        //    Response.End();
        //}

        /// <summary>
        /// Set current database environment based on web server path containing 'prod','test' or 'dev'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void Session_Start(object sender, EventArgs e)
        //{
        //    Core.Library.Authentication.UserAccess _UserAccess = new Core.Library.Authentication.UserAccess();

        //    _UserAccess.Env = Core.Logic.Functions.GetEnvironment();

        //    Core.Logic.Functions.SaveUserAccess(_UserAccess);
        //}

        //public void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Server.ClearError();

        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");
        //    routeData.Values.Add("action", "Error");
        //    routeData.Values.Add("exception", exception);

        //    if (exception.GetType() == typeof(HttpException))
        //    {
        //        routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
        //    }
        //    else
        //    {
        //        routeData.Values.Add("statusCode", 500);
        //    }

        //    Response.TrySkipIisCustomErrors = true;
        //    IController controller = new ErrorController();
        //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //    Response.End();
        //}
    }
}
