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
    public class releationTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: releationTypes
        public ActionResult Index()
        {
            return View(db.releationType.ToList());
        }

        // GET: releationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            releationType releationType = db.releationType.Find(id);
            if (releationType == null)
            {
                return HttpNotFound();
            }
            return View(releationType);
        }

        // GET: releationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: releationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "releationTypeId,relType,note1,note2")] releationType releationType)
        {
            if (ModelState.IsValid)
            {
                db.releationType.Add(releationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(releationType);
        }

        // GET: releationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            releationType releationType = db.releationType.Find(id);
            if (releationType == null)
            {
                return HttpNotFound();
            }
            return View(releationType);
        }

        // POST: releationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "releationTypeId,relType,note1,note2")] releationType releationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releationType);
        }

        // GET: releationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            releationType releationType = db.releationType.Find(id);
            if (releationType == null)
            {
                return HttpNotFound();
            }
            return View(releationType);
        }

        // POST: releationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            releationType releationType = db.releationType.Find(id);
            db.releationType.Remove(releationType);
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
