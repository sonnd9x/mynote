using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyNotes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Index",
              url: "",
              defaults: new { controller = "Home", action = "Index" }
          );

            routes.MapRoute(
               name: "GetLink",
               url: "getlink",
               defaults: new { controller = "GetLink", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Notes",
               url: "{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               constraints: new { id = "[A-Z0-9]{8}-([A-Z0-9]{4}-){3}[A-Z0-9]{12}" }
           );

            routes.MapRoute(
               name: "Raw",
               url: "raw/{id}",
               defaults: new { controller = "Home", action = "Raw", id = UrlParameter.Optional },
               constraints: new { id = "[A-Z0-9]{8}-([A-Z0-9]{4}-){3}[A-Z0-9]{12}" }
           );

            routes.MapRoute(
               name: "Download",
               url: "dl/{id}",
               defaults: new { controller = "Home", action = "Download", id = UrlParameter.Optional },
               constraints: new { id = "[A-Z0-9]{8}-([A-Z0-9]{4}-){3}[A-Z0-9]{12}" }
           );
            routes.MapRoute(
                name: "Login",
                url: "dangnhap",
                defaults: new { controller = "Login", action = "DangNhap" }
            );
            routes.MapRoute(
                name: "Logout",
                url: "dangxuat",
                defaults: new { controller = "Login", action = "DangXuat" }
            );
            routes.MapRoute(
                name: "Admin",
                url: "admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "OnlyAction",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
