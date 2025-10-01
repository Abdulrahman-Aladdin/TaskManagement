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
    public class TaskAssignmentsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: TaskAssignments
        public ActionResult Index()
        {
            var taskAssignments = db.TaskAssignments.Include(t => t.Member).Include(t => t.Task);
            return View(taskAssignments.ToList());
        }

        // GET: TaskAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);
            if (taskAssignment == null)
            {
                return HttpNotFound();
            }
            return View(taskAssignment);
        }

        // GET: TaskAssignments/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName");
            return View();
        }

        // POST: TaskAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId,MemberId,TaskId,CompletedHours,RemainingHours,Status")] TaskAssignment taskAssignment)
        {
            if (ModelState.IsValid)
            {
                db.TaskAssignments.Add(taskAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", taskAssignment.MemberId);
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskAssignment.TaskId);
            return View(taskAssignment);
        }

        // GET: TaskAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);
            if (taskAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", taskAssignment.MemberId);
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskAssignment.TaskId);
            return View(taskAssignment);
        }

        // POST: TaskAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentId,MemberId,TaskId,CompletedHours,RemainingHours,Status")] TaskAssignment taskAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", taskAssignment.MemberId);
            ViewBag.TaskId = new SelectList(db.Tasks, "TaskId", "TaskName", taskAssignment.TaskId);
            return View(taskAssignment);
        }

        // GET: TaskAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);
            if (taskAssignment == null)
            {
                return HttpNotFound();
            }
            return View(taskAssignment);
        }

        // POST: TaskAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);
            db.TaskAssignments.Remove(taskAssignment);
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
