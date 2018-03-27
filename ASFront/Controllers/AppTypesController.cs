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
    public class AppTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppTypes
      
        public ActionResult Index(int page = 1)
        {


            var items = db.AppTypes.Distinct().ToList();


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        // GET: AppTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTypes appTypes = db.AppTypes.Find(id);
            if (appTypes == null)
            {
                return HttpNotFound();
            }
            return View(appTypes);
        }

        // GET: AppTypes/Create
        public ActionResult Create()
        {
            Models.AppTypes item = new AppTypes();
            return View(item);
        }

        // POST: AppTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppTypesId,appType")] AppTypes appTypes)
        {
            if (ModelState.IsValid)
            {
                db.AppTypes.Add(appTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appTypes);
        }

        // GET: AppTypes/Edit/5
    

        // POST: AppTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppTypesId,appType")] AppTypes appTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appTypes);
        }

        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Models.AppTypes item = db.AppTypes.Where(p => p.AppTypesId == id).FirstOrDefault();

            if (item.AppTypesId== 0)
                return RedirectToAction("Create");


            return View(item);
        }

        // GET: AppTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTypes appTypes = db.AppTypes.Find(id);
            if (appTypes == null)
            {
                return HttpNotFound();
            }
            return View(appTypes);
        }

        // POST: AppTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppTypes appTypes = db.AppTypes.Find(id);
            db.AppTypes.Remove(appTypes);
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
