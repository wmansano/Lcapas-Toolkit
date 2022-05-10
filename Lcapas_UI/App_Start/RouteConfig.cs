using System.Web.Mvc;
using System.Web.Routing;

namespace Lcapas.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Landing",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Landing", action = "Landing", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ConfirmPayment",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Payment", action = "ConfirmPayment", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MakePayment",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Payment", action = "MakePayment", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Error",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Error", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
