using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LondonUbfWebDrive
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "parameter_catch_all",
                routeTemplate: "api/{controller}/{*path}",
                defaults: new { path = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
