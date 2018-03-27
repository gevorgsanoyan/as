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
    public class ScoringScoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringScores


        public ActionResult Index(int page = 1)
        {
            string IndicatorIDStr = Request.QueryString["IndicatorID"];
            int IndicatorID = 0;

            if (!string.IsNullOrWhiteSpace(IndicatorIDStr))
                Int32.TryParse(IndicatorIDStr, out IndicatorID);

            var items = new List<ScoringScoreView>();

            if (IndicatorID > 0)
            {
                items = (
                     from s in db.ScoringScores
                     join i in db.ScoringIndicators on s.indicatorID equals i.ID
                     where s.indicatorID == IndicatorID
                     select new ScoringScoreView
                     {
                         ID = s.ID,
                         Coefficient = s.Coefficient,
                         indicatorID = s.indicatorID,
                         IndicatorName = i.IndicatorName,
                         maxValue = s.maxValue,
                         minValue = s.minValue,
                         Score = s.Score
                     }
                  ).Distinct().ToList();
            }
            else
            {

                items = (
                from s in db.ScoringScores
                join
                i in db.ScoringIndicators on s.indicatorID equals i.ID
                select new ScoringScoreView
                {
                    ID = s.ID,
                    Coefficient = s.Coefficient,
                    indicatorID = s.indicatorID,
                    IndicatorName = i.IndicatorName,
                    maxValue = s.maxValue,
                    minValue = s.minValue,
                    Score = s.Score
                }
                ).Distinct().OrderBy(o => o.IndicatorName).ToList();
            }//if (IndicatorID > 0)


            ViewBag.indicator = new SelectList(db.ScoringIndicators, "ID", "IndicatorName");


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult Index(string indicator, int page = 1)
        {
            //string IndicatorIDStr = Request.QueryString["IndicatorID"];
            int IndicatorID = 0;

            //if (!string.IsNullOrWhiteSpace(IndicatorIDStr))
            //    Int32.TryParse(IndicatorIDStr, out IndicatorID);

            if (indicator != "")
                IndicatorID = int.Parse(indicator);

            var items = new List<ScoringScoreView>();

            if (IndicatorID > 0)
            {
                items = (
                     from s in db.ScoringScores
                     join i in db.ScoringIndicators on s.indicatorID equals i.ID
                     where s.indicatorID == IndicatorID
                     select new ScoringScoreView
                     {
                         ID = s.ID,
                         Coefficient = s.Coefficient,
                         indicatorID = s.indicatorID,
                         IndicatorName = i.IndicatorName,
                         maxValue = s.maxValue,
                         minValue = s.minValue,
                         Score = s.Score
                     }
                  ).Distinct().ToList();
            }
            else
            {

                items = (
                from s in db.ScoringScores
                join
                i in db.ScoringIndicators on s.indicatorID equals i.ID
                select new ScoringScoreView
                {
                    ID = s.ID,
                    Coefficient = s.Coefficient,
                    indicatorID = s.indicatorID,
                    IndicatorName = i.IndicatorName,
                    maxValue = s.maxValue,
                    minValue = s.minValue,
                    Score = s.Score
                }
                ).Distinct().OrderBy(o=>o.IndicatorName).ToList();
            }//if (IndicatorID > 0)


            ViewBag.indicator = new SelectList(db.ScoringIndicators, "ID", "IndicatorName");


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: ScoringScores/Details/5

        // GET: ScoringScores/Create
        public ActionResult Create()
        {

            ScoringScores item = new ScoringScores();
            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;


            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();    
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");
          




            return View(item);
        }

        // POST: ScoringScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringScores item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.ScoringScores.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");



            return View(item);
        }

        // GET: ScoringScores/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            ScoringScores item = db.ScoringScores.Find(id);

            if (item == null)
                return RedirectToAction("Create");

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");



            return View(item);
        }

        // POST: ScoringScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringScores item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");



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
