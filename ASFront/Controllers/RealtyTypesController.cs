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
    public class RealtyTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RealtyTypes
        public ActionResult Index()
        {
            return View(db.RealtyTypes.ToList());
        }

        // GET: RealtyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyTypes realtyTypes = db.RealtyTypes.Find(id);
            if (realtyTypes == null)
            {
                return HttpNotFound();
            }
            return View(realtyTypes);
        }

        // GET: RealtyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealtyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] RealtyTypes realtyTypes)
        {
            if (ModelState.IsValid)
            {
                db.RealtyTypes.Add(realtyTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realtyTypes);
        }

        // GET: RealtyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyTypes realtyTypes = db.RealtyTypes.Find(id);
            if (realtyTypes == null)
            {
                return HttpNotFound();
            }
            return View(realtyTypes);
        }

        // POST: RealtyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] RealtyTypes realtyTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtyTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realtyTypes);
        }

        // GET: RealtyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyTypes realtyTypes = db.RealtyTypes.Find(id);
            if (realtyTypes == null)
            {
                return HttpNotFound();
            }
            return View(realtyTypes);
        }

        // POST: RealtyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealtyTypes realtyTypes = db.RealtyTypes.Find(id);
            db.RealtyTypes.Remove(realtyTypes);
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
