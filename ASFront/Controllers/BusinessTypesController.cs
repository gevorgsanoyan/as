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
   
    public class BusinessTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BusinessTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var businessType = db.BusinessType.Include(b => b.BusinessSector);
            return View(businessType.ToList());
        }

        // GET: BusinessTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessType.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            return View();
        }

        // POST: BusinessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BusinessSectorId,Name,note")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.BusinessType.Add(businessType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessType.BusinessSectorId);
            return View(businessType);
        }

        // GET: BusinessTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessType.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessType.BusinessSectorId);
            return View(businessType);
        }

        // POST: BusinessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BusinessSectorId,Name,note")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessType.BusinessSectorId);
            return View(businessType);
        }

        // GET: BusinessTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessType.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessType businessType = db.BusinessType.Find(id);
            db.BusinessType.Remove(businessType);
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
