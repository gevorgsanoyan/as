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
    [Authorize]
    public class ScoringIndicatorsTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringIndicatorsTypes
        public ActionResult Index(int page = 1)
        {
            var items = db.ScoringIndicatorsTypes.ToList();

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: ScoringIndicatorsTypes/Details/5
  

        // GET: ScoringIndicatorsTypes/Create
        public ActionResult Create()
        {
            Models.ScoringIndicatorsTypes item = new ScoringIndicatorsTypes();    

            return View(item);
        }

        // POST: ScoringIndicatorsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringIndicatorsTypes item)
        {
            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.ScoringIndicatorsTypes.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: ScoringIndicatorsTypes/Edit/5
        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringIndicatorsTypes item = db.ScoringIndicatorsTypes.Find(id);

            if (item == null)
                return RedirectToAction("Create");


        
            return View(item);
        }

        // POST: ScoringIndicatorsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringIndicatorsTypes item)
        {
             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: ScoringIndicatorsTypes/Delete/5
     
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
