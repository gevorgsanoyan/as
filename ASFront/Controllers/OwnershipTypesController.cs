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
    [Authorize]
    public class OwnershipTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OwnershipTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.OwnershipType.ToList());
        }

        // GET: OwnershipTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnershipType ownershipType = db.OwnershipType.Find(id);
            if (ownershipType == null)
            {
                return HttpNotFound();
            }
            return View(ownershipType);
        }

        // GET: OwnershipTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnershipTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,note")] OwnershipType ownershipType)
        {
            if (ModelState.IsValid)
            {
                db.OwnershipType.Add(ownershipType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ownershipType);
        }

        // GET: OwnershipTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnershipType ownershipType = db.OwnershipType.Find(id);
            if (ownershipType == null)
            {
                return HttpNotFound();
            }
            return View(ownershipType);
        }

        // POST: OwnershipTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,note")] OwnershipType ownershipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownershipType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownershipType);
        }

        // GET: OwnershipTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnershipType ownershipType = db.OwnershipType.Find(id);
            if (ownershipType == null)
            {
                return HttpNotFound();
            }
            return View(ownershipType);
        }

        // POST: OwnershipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnershipType ownershipType = db.OwnershipType.Find(id);
            db.OwnershipType.Remove(ownershipType);
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
