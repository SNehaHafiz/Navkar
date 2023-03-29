using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

namespace TrackerMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            string strValue = ConfigurationManager.AppSettings["StartAppPage"].ToString();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");   
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = strValue, id = UrlParameter.Optional }
            );
        }
    }
}
