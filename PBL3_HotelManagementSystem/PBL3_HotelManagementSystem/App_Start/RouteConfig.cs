﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PBL3_HotelManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Accounts/Login",
                defaults: new { controller = "Accounts", action = "Login" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/Index",
                defaults: new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
            name: "AdminGetServices",
            url: "Admin/GetServices",
            defaults: new { controller = "Admin", action = "GetServices" }
        );

            routes.MapRoute(
                name: "GetServicesData",
                url: "Admin/GetServicesData",
                defaults: new { controller = "Admin", action = "GetServicesData" }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
