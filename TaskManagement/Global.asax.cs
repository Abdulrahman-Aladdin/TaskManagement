using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TaskManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var LogPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/log.txt");

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug() // Log level (Debug, Info, Warning, Error)
                    .WriteTo.Debug()
                    .WriteTo.File(LogPath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            Log.Information("Application Starting up...");
            Database.SetInitializer(new Models.AppDbInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
