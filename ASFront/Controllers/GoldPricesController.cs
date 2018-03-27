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
    public class GoldPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GoldPrices
        public ActionResult Index()
        {
            var goldPrices = db.GoldPrices.Include(g => g.GoldAssayes);
            return View(goldPrices.ToList());
        }

        // GET: GoldPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldPrices goldPrices = db.GoldPrices.Find(id);
            if (goldPrices == null)
            {
                return HttpNotFound();
            }
            return View(goldPrices);
        }

        // GET: GoldPrices/Create
        public ActionResult Create()
        {
            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay");
            return View();
        }

        // POST: GoldPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GoldAssayId,Price,Date")] GoldPrices goldPrices)
        {
            if (ModelState.IsValid)
            {
                db.GoldPrices.Add(goldPrices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldPrices.GoldAssayId);
            return View(goldPrices);
        }

        // GET: GoldPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldPrices goldPrices = db.GoldPrices.Find(id);
            if (goldPrices == null)
            {
                return HttpNotFound();
            }
            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldPrices.GoldAssayId);
            return View(goldPrices);
        }

        // POST: GoldPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GoldAssayId,Price,Date")] GoldPrices goldPrices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldPrices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldPrices.GoldAssayId);
            return View(goldPrices);
        }

        // GET: GoldPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldPrices goldPrices = db.GoldPrices.Find(id);
            if (goldPrices == null)
            {
                return HttpNotFound();
            }
            return View(goldPrices);
        }

        // POST: GoldPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldPrices goldPrices = db.GoldPrices.Find(id);
            db.GoldPrices.Remove(goldPrices);
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
