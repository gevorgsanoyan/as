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
    public class ScoringDecisionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringDecisions
      


        public ActionResult Index(int page = 1)
        {
         
            var items = db.ScoringDecisions.ToList().OrderBy(p => p.Decision);
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }


        // GET: ScoringDecisions/Details/5


        // GET: ScoringDecisions/Create
        public ActionResult Create()
        {
            ScoringDecisions item = new ScoringDecisions();



            return View(item);
        }

        // POST: ScoringDecisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ScoringDecisions scoringDecision)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.ScoringDecisions.Add(scoringDecision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoringDecision);
        }

        // GET: ScoringDecisions/Edit/5
      
        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringDecisions item = db.ScoringDecisions.Find(id);

            if (item == null)
                return RedirectToAction("Create");


         

            return View(item);
        }

        // POST: ScoringDecisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ScoringDecisions item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
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
