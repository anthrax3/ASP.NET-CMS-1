using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "User",                                           // Route name
                "User/{userName}",                            // URL with parameters
                new { controller = "Users", action = "Details" }  // Parameter defaults
            );
            routes.MapRoute(
                "Project",                                           // Route name
                "Project/{projectName}",                            // URL with parameters
                new { controller = "Projects", action = "Details" }  // Parameter defaults
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
