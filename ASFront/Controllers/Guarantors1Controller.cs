using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;

namespace ASFront.Controllers
{
    public class Guarantors1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guarantors1
        public ActionResult Index()
        {
            var guarantors = db.Guarantors.Include(g => g.applications).Include(g => g.clients);
            return View(guarantors.ToList());
        }

        // GET: Guarantors1/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            return View(guarantors);
        }

        // GET: Guarantors1/Create
        public ActionResult Create()
        {
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            return View();
        }

        // POST: Guarantors1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,applicationId,clientId")] Guarantors guarantors)
        {
            if (ModelState.IsValid)
            {
                db.Guarantors.Add(guarantors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // GET: Guarantors1/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // POST: Guarantors1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,applicationId,clientId")] Guarantors guarantors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guarantors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // GET: Guarantors1/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            return View(guarantors);
        }

        // POST: Guarantors1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Guarantors guarantors = db.Guarantors.Find(id);
            db.Guarantors.Remove(guarantors);
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
