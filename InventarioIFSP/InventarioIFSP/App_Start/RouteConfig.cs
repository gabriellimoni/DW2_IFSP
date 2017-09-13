using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventarioIFSP
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
                  name: "Actions",
                  url: "Home/Principal",
                  defaults: new { controller = "Home", action = "Principal" }
              );

            routes.MapRoute(
                name: "Login",
                url: "Usuario/Login",
                defaults: new { controller = "Usuario", action = "Login" }
            );

            routes.MapRoute(
                name: "EnviaLogin",
                url: "Usuario/EnviaLogin",
                defaults: new { controller = "Usuario", action = "EnviaLogin" }
            );
        }
    }
}
