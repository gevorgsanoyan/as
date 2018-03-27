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
    public class AgroAssetTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgroAssetTypes
        public ActionResult Index()
        {
            return View(db.AgroAssetTypes.ToList());
        }

        // GET: AgroAssetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAssetTypes agroAssetTypes = db.AgroAssetTypes.Find(id);
            if (agroAssetTypes == null)
            {
                return HttpNotFound();
            }
            return View(agroAssetTypes);
        }

        // GET: AgroAssetTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroAssetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,note1,note2,note3,note4")] AgroAssetTypes agroAssetTypes)
        {
            if (ModelState.IsValid)
            {
                db.AgroAssetTypes.Add(agroAssetTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agroAssetTypes);
        }

        // GET: AgroAssetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAssetTypes agroAssetTypes = db.AgroAssetTypes.Find(id);
            if (agroAssetTypes == null)
            {
                return HttpNotFound();
            }
            return View(agroAssetTypes);
        }

        // POST: AgroAssetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,note1,note2,note3,note4")] AgroAssetTypes agroAssetTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agroAssetTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agroAssetTypes);
        }

        // GET: AgroAssetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAssetTypes agroAssetTypes = db.AgroAssetTypes.Find(id);
            if (agroAssetTypes == null)
            {
                return HttpNotFound();
            }
            return View(agroAssetTypes);
        }

        // POST: AgroAssetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgroAssetTypes agroAssetTypes = db.AgroAssetTypes.Find(id);
            db.AgroAssetTypes.Remove(agroAssetTypes);
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
