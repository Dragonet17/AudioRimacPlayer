using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AudioRimacPlayer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
       //     routes.MapRoute(
       //    name: "Song",
       //   url: "{RimacPlayer}/{Song}",
       //   defaults: new { controller = "Rimac", action = "Song" }
       //);
       //     routes.MapRoute(
       //     name: "Music",
       //    url: "{RimacPlayer}/{PlayMusic}/{id}",
       //    defaults: new { controller = "Rimac", action = "PlayMusic" }
       // );
       //     routes.MapRoute(
       //         name: "RimacPlayer",
       //         url: "{RimacPlayer}/{LogIn}",
       //         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
       //     );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Player", action = "Music", id = UrlParameter.Optional }
            );
        }
    }
}
