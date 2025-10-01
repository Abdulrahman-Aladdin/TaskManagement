using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Log.Information("Index page visited at {Time}", DateTime.Now);

            //var LogPath= System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/log.txt");
            var logg=new LoggerConfiguration().WriteTo.Debug().CreateLogger();
            logg.Information("Hello, Serilog!");
            try
            {
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred in Error action");
            }
            //ViewBag.LogPath = LogPath;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            try
            {
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred in Error action");
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}