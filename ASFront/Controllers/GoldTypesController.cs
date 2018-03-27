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
    public class GoldTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GoldTypes
        public ActionResult Index()
        {
            return View(db.GoldTypes.ToList());
        }

        // GET: GoldTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldTypes goldTypes = db.GoldTypes.Find(id);
            if (goldTypes == null)
            {
                return HttpNotFound();
            }
            return View(goldTypes);
        }

        // GET: GoldTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoldTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] GoldTypes goldTypes)
        {
            if (ModelState.IsValid)
            {
                db.GoldTypes.Add(goldTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goldTypes);
        }

        // GET: GoldTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldTypes goldTypes = db.GoldTypes.Find(id);
            if (goldTypes == null)
            {
                return HttpNotFound();
            }
            return View(goldTypes);
        }

        // POST: GoldTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] GoldTypes goldTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goldTypes);
        }

        // GET: GoldTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldTypes goldTypes = db.GoldTypes.Find(id);
            if (goldTypes == null)
            {
                return HttpNotFound();
            }
            return View(goldTypes);
        }

        // POST: GoldTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldTypes goldTypes = db.GoldTypes.Find(id);
            db.GoldTypes.Remove(goldTypes);
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
