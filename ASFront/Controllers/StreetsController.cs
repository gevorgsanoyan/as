using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using ASFront.Classes;
using PagedList;

namespace ASFront.Controllers
{
    public class StreetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Streets
        public ActionResult Index(int page = 1)
        {
           
            var items = db.Streets.Where(s=>s.Id < 100).ToList().OrderBy(p => p.reg).ThenBy(p=> p.cName).ThenBy(p=> p.Street);
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        // GET: Streets/Details/5
    

        // GET: Streets/Create
        public ActionResult Create()
        {
            Streets streets = new Streets();

            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();


            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");

            return View(streets);
        }

        // POST: Streets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Streets streets)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Streets.Add(streets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();

            if (streets.reg != null)
            {
                cityes = (from c in db.comunities
                          let rCity = c.cName
                          where c.reg == streets.reg
                          select new { rCity }).Distinct().ToList();
            }


            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");

            return View(streets);
        }

        // GET: Streets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Streets streets = db.Streets.Find(id);
            if (streets == null)
            {
                return HttpNotFound();
            }


            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();
            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");

            return View(streets);
        }

        // POST: Streets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Streets streets)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(streets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();

            if (streets.reg != null)
            {
                cityes = (from c in db.comunities
                          let rCity = c.cName
                          where c.reg == streets.reg
                          select new { rCity }).Distinct().ToList();
            }

            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            return View(streets);
        }

        // GET: Streets/Delete/5
      
        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Streets streets = db.Streets.Find(id);
            db.Streets.Remove(streets);
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
