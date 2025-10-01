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

        public ActionResult MembersCompeletedHours()
        {
            var data = db.Members
            .Select(m => new
            {
                MemberName = m.MemberName,
                CompletedHours = m.TaskAssignments.Sum(a => a.CompletedHours)
            })
            .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MembersRemainingHours()
        {
            var data = db.Members
                .Select(m => new
                {
                    MemberName = m.MemberName,
                    RemainingHours = m.TaskAssignments
                    .Where(a => a.Status == TaskStatus.Active)
                    .Sum(a => a.RemainingHours)
                }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasksStatus()
        {
            var data = db.TaskAssignments
                .GroupBy(a => a.Status)
                .Select(g => new
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}