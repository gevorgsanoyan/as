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
    public class MovableEstateTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovableEstateTypes
        public ActionResult Index()
        {
            return View(db.MovableEstateTypes.ToList());
        }

        // GET: MovableEstateTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstateTypes movableEstateTypes = db.MovableEstateTypes.Find(id);
            if (movableEstateTypes == null)
            {
                return HttpNotFound();
            }
            return View(movableEstateTypes);
        }

        // GET: MovableEstateTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovableEstateTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] MovableEstateTypes movableEstateTypes)
        {
            if (ModelState.IsValid)
            {
                db.MovableEstateTypes.Add(movableEstateTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movableEstateTypes);
        }

        // GET: MovableEstateTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstateTypes movableEstateTypes = db.MovableEstateTypes.Find(id);
            if (movableEstateTypes == null)
            {
                return HttpNotFound();
            }
            return View(movableEstateTypes);
        }

        // POST: MovableEstateTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] MovableEstateTypes movableEstateTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movableEstateTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movableEstateTypes);
        }

        // GET: MovableEstateTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstateTypes movableEstateTypes = db.MovableEstateTypes.Find(id);
            if (movableEstateTypes == null)
            {
                return HttpNotFound();
            }
            return View(movableEstateTypes);
        }

        // POST: MovableEstateTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovableEstateTypes movableEstateTypes = db.MovableEstateTypes.Find(id);
            db.MovableEstateTypes.Remove(movableEstateTypes);
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
