using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class AnalyticsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Analytics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StatuspiChart()
        {
            //var data =
            return View();


        public ActionResult TaskStatusData()
        {
                var data = db.TaskAssignments
                            .GroupBy(t => t.Status)
                            .Select(g => new TaskStatusChartViewModel
                            {
                                Status = g.Key.ToString(),
                                Count = g.Count()
                            }).ToList();

            // Highcharts expects { name, y }
            var chartData = data.Select(d => new { name = d.Status, y = d.Count }).ToList();

            ViewBag.TaskStatusChartData = chartData;
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberHoursData()
        {
            var data = db.TaskAssignments
                .GroupBy(a => a.Member.MemberName)
                .Select(g => new MemberHoursChartViewModel
                {
                    MemberName = g.Key,
                    CompletedHours = g.Sum(x => x.CompletedHours),
                    RemainingHours = g.Sum(x => x.RemainingHours)
                }).ToList();

            ViewBag.MemberHoursChartData = data;
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}