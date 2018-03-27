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
    public class NameofBanksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NameofBanks
        public ActionResult Index()
        {
            return View(db.NameofBanks.ToList());
        }

        // GET: NameofBanks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameofBanks nameofBanks = db.NameofBanks.Find(id);
            if (nameofBanks == null)
            {
                return HttpNotFound();
            }
            return View(nameofBanks);
        }

        // GET: NameofBanks/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NameofBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,note")] NameofBanks nameofBanks)
        {
            if (ModelState.IsValid)
            {
                db.NameofBanks.Add(nameofBanks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nameofBanks);
        }

        // GET: NameofBanks/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameofBanks nameofBanks = db.NameofBanks.Find(id);
            if (nameofBanks == null)
            {
                return HttpNotFound();
            }
            return View(nameofBanks);
        }

        // POST: NameofBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,note")] NameofBanks nameofBanks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nameofBanks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameofBanks);
        }

        // GET: NameofBanks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameofBanks nameofBanks = db.NameofBanks.Find(id);
            if (nameofBanks == null)
            {
                return HttpNotFound();
            }
            return View(nameofBanks);
        }

        // POST: NameofBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            NameofBanks nameofBanks = db.NameofBanks.Find(id);
            db.NameofBanks.Remove(nameofBanks);
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
