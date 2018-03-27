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
using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringIndicatorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringIndicators
        public ActionResult Index(int page = 1)
        {
            var items = (
                from si in db.ScoringIndicators
                join sit in db.ScoringIndicatorsTypes on si.IndicatorType equals sit.ID

                select new ScoringIndicatorsView
                {
                    IndicatorName = si.IndicatorName,
                    FormulaText = si.FormulaText,
                    IndicatorTypeName = sit.TypeName,
                    ID = si.ID,
                    IndicatorValue = si.IndicatorValue,
                    FormulaTextPriorityFixed = si.FormulaTextPriorityFixed


                }


                ).Distinct().ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }





        // GET: ScoringIndicators/Create
        public ActionResult Create()
        {
            var IndType = db.ScoringIndicatorsTypes.OrderBy(p => p.TypeName).Select(p => new { p.ID, p.TypeName }).Distinct().ToList();
            ViewBag.IndType = new SelectList(IndType, "ID", "TypeName");

            Models.ScoringIndicators item = new ScoringIndicators();

            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;
                                                 

            return View(item);
        }

        // POST: ScoringIndicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringIndicators item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.ScoringIndicators.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndType = db.ScoringIndicatorsTypes.OrderBy(p => p.TypeName).Select(p => new { p.ID, p.TypeName }).Distinct().ToList();
            ViewBag.IndType = new SelectList(IndType, "ID", "TypeName");

            return View(item);
        }



        // GET: ScoringIndicators/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            ScoringIndicators item = db.ScoringIndicators.Find(id);

            if (item == null)
                return RedirectToAction("Create");

            var IndType = db.ScoringIndicatorsTypes.OrderBy(p => p.TypeName).Select(p => new { p.ID, p.TypeName }).Distinct().ToList();
            ViewBag.IndType = new SelectList(IndType, "ID", "TypeName");

            return View(item);


        
        }

        // POST: ScoringIndicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringIndicators item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;


                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndType = db.ScoringIndicatorsTypes.OrderBy(p => p.TypeName).Select(p => new { p.ID, p.TypeName }).Distinct().ToList();
            ViewBag.IndType = new SelectList(IndType, "ID", "TypeName");

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
