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
    public class GoldAssayesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GoldAssayes
        public ActionResult Index()
        {
            return View(db.GoldAssayes.ToList());
        }

        // GET: GoldAssayes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldAssayes goldAssayes = db.GoldAssayes.Find(id);
            if (goldAssayes == null)
            {
                return HttpNotFound();
            }
            return View(goldAssayes);
        }

        // GET: GoldAssayes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoldAssayes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoldAssayes goldAssayes)
        {
            if (ModelState.IsValid)
            {
                db.GoldAssayes.Add(goldAssayes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goldAssayes);
        }

        // GET: GoldAssayes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldAssayes goldAssayes = db.GoldAssayes.Find(id);
            if (goldAssayes == null)
            {
                return HttpNotFound();
            }
            return View(goldAssayes);
        }

        // POST: GoldAssayes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( GoldAssayes goldAssayes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldAssayes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goldAssayes);
        }

        // GET: GoldAssayes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldAssayes goldAssayes = db.GoldAssayes.Find(id);
            if (goldAssayes == null)
            {
                return HttpNotFound();
            }
            return View(goldAssayes);
        }

        // POST: GoldAssayes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldAssayes goldAssayes = db.GoldAssayes.Find(id);
            db.GoldAssayes.Remove(goldAssayes);
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
