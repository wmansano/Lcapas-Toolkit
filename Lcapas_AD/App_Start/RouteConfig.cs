using System.Web.Mvc;
using System.Web.Routing;

namespace Lcapas.AD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DisplayMessage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Transcripts", action = "DisplayMessage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Transcripts",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Transcripts", action = "Transcripts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ErrorHandler",
                url: "{controller}/{action}/{errMsg}",
                defaults: new { controller = "Error", action = "Error", errMsg = UrlParameter.Optional }
            );
        }
    }
}
