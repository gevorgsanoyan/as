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
using ASFront.ModelsView;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringScoreDecisionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringScoreDecisions  
        public ActionResult Index(int page = 1)
        {

            var items = (
            from sd in db.ScoringScoreDecisions
            join p in db.Products on sd.ProductID equals p.productId
            join d in db.ScoringDecisions on sd.DecisionID equals d.ID

            select new ScoringScoreDecisionsView
            {
                ID = sd.ID,
                Decision = d.Decision,
                ProductName = p.productName,
                maxValue = sd.maxValue,
                minValue = sd.minValue
            }
                 ).Distinct().ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        // GET: ScoringScoreDecisions/Details/5


        // GET: ScoringScoreDecisions/Create
        public ActionResult Create()
        {
            ScoringScoreDecisions item = new ScoringScoreDecisions();

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var Decision = db.ScoringDecisions.Select(p => new { Id = p.ID, Decision = p.Decision }).Distinct().ToList();

            ViewBag.Decision = new SelectList(Decision, "Id", "Decision");
            ViewBag.prod = new SelectList(prod, "productId", "productName");


            return View(item);
        }

        // POST: ScoringScoreDecisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringScoreDecisions scoringScoreDecisions)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.ScoringScoreDecisions.Add(scoringScoreDecisions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var Decision = db.ScoringDecisions.Select(p => new { Id = p.ID, Decision = p.Decision }).Distinct().ToList();

            ViewBag.Decision = new SelectList(Decision, "Id", "Decision");
            ViewBag.prod = new SelectList(prod, "productId", "productName");

            return View(scoringScoreDecisions);
        }

        // GET: ScoringScoreDecisions/Edit/5


        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringScoreDecisions item = db.ScoringScoreDecisions.Find(id);

            if (item == null)
                return RedirectToAction("Create");

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var Decision = db.ScoringDecisions.Select(p => new { Id = p.ID, Decision = p.Decision }).Distinct().ToList();

            ViewBag.Decision = new SelectList(Decision, "Id", "Decision");
            ViewBag.prod = new SelectList(prod, "productId", "productName");


            return View(item);
        }

        // POST: ScoringScoreDecisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringScoreDecisions scoringScoreDecisions)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(scoringScoreDecisions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var Decision = db.ScoringDecisions.Select(p => new { Id = p.ID, Decision = p.Decision }).Distinct().ToList();

            ViewBag.Decision = new SelectList(Decision, "Id", "Decision");
            ViewBag.prod = new SelectList(prod, "productId", "productName");

            return View(scoringScoreDecisions);
        }

        // GET: ScoringScoreDecisions/Delete/5


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
