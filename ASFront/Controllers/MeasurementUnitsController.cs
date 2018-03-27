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
    public class MeasurementUnitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeasurementUnits
        public ActionResult Index()
        {
            return View(db.MeasurementUnits.ToList());
        }

        // GET: MeasurementUnits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementUnits measurementUnits = db.MeasurementUnits.Find(id);
            if (measurementUnits == null)
            {
                return HttpNotFound();
            }
            return View(measurementUnits);
        }

        // GET: MeasurementUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeasurementUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] MeasurementUnits measurementUnits)
        {
            if (ModelState.IsValid)
            {
                db.MeasurementUnits.Add(measurementUnits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(measurementUnits);
        }

        // GET: MeasurementUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementUnits measurementUnits = db.MeasurementUnits.Find(id);
            if (measurementUnits == null)
            {
                return HttpNotFound();
            }
            return View(measurementUnits);
        }

        // POST: MeasurementUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] MeasurementUnits measurementUnits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurementUnits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(measurementUnits);
        }

        // GET: MeasurementUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementUnits measurementUnits = db.MeasurementUnits.Find(id);
            if (measurementUnits == null)
            {
                return HttpNotFound();
            }
            return View(measurementUnits);
        }

        // POST: MeasurementUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeasurementUnits measurementUnits = db.MeasurementUnits.Find(id);
            db.MeasurementUnits.Remove(measurementUnits);
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
