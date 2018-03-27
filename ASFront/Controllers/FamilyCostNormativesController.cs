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
    public class FamilyCostNormativesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FamilyCostNormatives
        public ActionResult Index()
        {
            var familyCostNormatives = db.FamilyCostNormatives.Include(f => f.Branches);
            return View(familyCostNormatives.ToList());
        }

        // GET: FamilyCostNormatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyCostNormatives familyCostNormatives = db.FamilyCostNormatives.Find(id);
            if (familyCostNormatives == null)
            {
                return HttpNotFound();
            }
            return View(familyCostNormatives);
        }

        // GET: FamilyCostNormatives/Create
        public ActionResult Create()
        {
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch");
            return View();
        }

        // POST: FamilyCostNormatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrancheId,maxAge,Cost,note1,note2,note3,note4")] FamilyCostNormatives familyCostNormatives)
        {
            if (ModelState.IsValid)
            {
                db.FamilyCostNormatives.Add(familyCostNormatives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", familyCostNormatives.BrancheId);
            return View(familyCostNormatives);
        }

        // GET: FamilyCostNormatives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyCostNormatives familyCostNormatives = db.FamilyCostNormatives.Find(id);
            if (familyCostNormatives == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", familyCostNormatives.BrancheId);
            return View(familyCostNormatives);
        }

        // POST: FamilyCostNormatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrancheId,maxAge,Cost,note1,note2,note3,note4")] FamilyCostNormatives familyCostNormatives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyCostNormatives).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", familyCostNormatives.BrancheId);
            return View(familyCostNormatives);
        }

        // GET: FamilyCostNormatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyCostNormatives familyCostNormatives = db.FamilyCostNormatives.Find(id);
            if (familyCostNormatives == null)
            {
                return HttpNotFound();
            }
            return View(familyCostNormatives);
        }

        // POST: FamilyCostNormatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyCostNormatives familyCostNormatives = db.FamilyCostNormatives.Find(id);
            db.FamilyCostNormatives.Remove(familyCostNormatives);
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
