
using System.Web.Mvc;
using System.Web.Routing;

namespace Market
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            //routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();

     
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "Market.Controllers" }
            );

            routes.MapRoute(
                "DefaultWithArea",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "Market.Areas.Admin.Controllers" }
            );
        }
    }
}
