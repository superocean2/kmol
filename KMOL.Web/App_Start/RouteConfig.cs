using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KMOL.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Detail",
                url: "detail/{id}/{l}/{date}/{name}/{controller}/{action}",
                defaults: new { controller = "Detail", action = "Index" }
            );
            routes.MapRoute(
               name: "NotFound",
               url: "pagenotfound/{url}/{controller}/{action}",
               defaults: new { controller = "NotFoundPage", action = "Index"}
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
