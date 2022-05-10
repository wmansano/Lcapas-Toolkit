using Lcapas.UI.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lcapas.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

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
            //if (Response.StatusCode == 404)
            //{
            //    Response.ClearContent();
            //    Server.Transfer("~/Views/Error/Index.cshtml");
            //}

        }

        void Application_Error(object sender, System.EventArgs e)
        {
            //string url = string.Empty;

            //switch (Response.StatusCode)
            //{
            //    case 404:
            //        url = "~/Error/Index";
            //        break;

            //    case 405:
            //        url = "~/Error/Index";
            //        break;
            //    case 500:
            //        url = "~/Error/Index";
            //        break;
            //}

            //try
            //{
            //    Request.RequestContext.HttpContext.Server.TransferRequest(url);
            //    Response.Clear();
            //    Server.ClearError();
            //}
            //catch (Exception ex)
            //{
            //    string msg = ex.ToString();
            //}
        }
    }
}
