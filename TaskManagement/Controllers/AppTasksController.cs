using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class AppTasksController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: AppTasks
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // GET: AppTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTask appTask = db.Tasks.Find(id);
            if (appTask == null)
            {
                return HttpNotFound();
            }
            return View(appTask);
        }

        // GET: AppTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,TaskName,ExpectedHours")] AppTask appTask)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(appTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appTask);
        }

        // GET: AppTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTask appTask = db.Tasks.Find(id);
            if (appTask == null)
            {
                return HttpNotFound();
            }
            return View(appTask);
        }

        // POST: AppTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,TaskName,ExpectedHours")] AppTask appTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appTask);
        }

        // GET: AppTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTask appTask = db.Tasks.Find(id);
            if (appTask == null)
            {
                return HttpNotFound();
            }
            return View(appTask);
        }

        // POST: AppTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppTask appTask = db.Tasks.Find(id);
            db.Tasks.Remove(appTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
