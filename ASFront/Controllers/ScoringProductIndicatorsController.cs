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
using Resources;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringProductIndicatorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringProductIndicators

        public ActionResult Index(int page = 1)
        {

            var items = (
      from pi in db.ScoringProductIndicators
      join p in db.Products on pi.ProductID equals p.productId
      join i in db.ScoringIndicators on pi.IndicatorID equals i.ID

      select new ScoringProductIndicatorsView
      {
          ID = pi.ID,
          IndicatorName = i.IndicatorName,
          ProductName = p.productName

      }
                ).Distinct().ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }
        // GET: ScoringProductIndicators/Details/5


        // GET: ScoringProductIndicators/Create
        public ActionResult Create()
        {
            ScoringProductIndicators item = new ScoringProductIndicators();


            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();



            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");



            return View(item);
        }

        // POST: ScoringProductIndicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringProductIndicators scoringProductIndicators)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                if (!(db.ScoringProductIndicators.Any(p => p.ProductID == scoringProductIndicators.ProductID && p.IndicatorID == scoringProductIndicators.IndicatorID)))
                {

                    db.ScoringProductIndicators.Add(scoringProductIndicators);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", Messages.ProductIndicatorsExist);
                }
            }

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();



            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");


            return View(scoringProductIndicators);
        }

        // GET: ScoringProductIndicators/Edit/5

        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringProductIndicators item = db.ScoringProductIndicators.Where(p => p.ID == id).FirstOrDefault();

            if (item == null)
                return RedirectToAction("Create");

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();



            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");



            return View(item);
        }

        // POST: ScoringProductIndicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringProductIndicators scoringProductIndicators)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(scoringProductIndicators).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();



            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");


            return View(scoringProductIndicators);
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
