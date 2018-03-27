using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using ASFront.ModelsView;
using ASFront.Classes;
using PagedList;
using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringIndicatorsParametersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringIndicatorsParameters
        public ActionResult Index(int page = 1)
        {
            string IndicatorIDStr = Request.QueryString["IndicatorID"];
            int IndicatorID = 0;

            if (!string.IsNullOrWhiteSpace(IndicatorIDStr))
                Int32.TryParse(IndicatorIDStr, out IndicatorID);

            var items = new List<ScoringIndicatorsParametersViewModel>();

            if (IndicatorID > 0)

            {
                items = (from ip in db.ScoringIndicatorsParameters
                         join i in db.ScoringIndicators on ip.IndicatorID equals i.ID
                         join p in db.ScoringParameters on ip.ParameterID equals p.ID
                         where ip.IndicatorID == IndicatorID
                         select new ScoringIndicatorsParametersViewModel { ID = ip.ID, IndicatorName = i.IndicatorName, ParameterName = p.InputParameterName }
                            ).ToList();
            }
            else
            {
                items = (from ip in db.ScoringIndicatorsParameters
                         join i in db.ScoringIndicators on ip.IndicatorID equals i.ID
                         join p in db.ScoringParameters on ip.ParameterID equals p.ID
                         select new ScoringIndicatorsParametersViewModel { ID = ip.ID, IndicatorName = i.IndicatorName, ParameterName = p.InputParameterName }
                            ).ToList();
            }


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }


        // GET: ScoringIndicatorsParameters/Create
        public ActionResult Create()
        {
            ScoringIndicatorsParameters item = new ScoringIndicatorsParameters();

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var ParameterID = db.ScoringParameters.Select(p => new { ID = p.ID, Name = p.InputParameterName }).OrderBy(p => p.Name).Distinct().ToList();

            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");
            ViewBag.Param = new SelectList(ParameterID, "ID", "Name");


            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;
            return View(item);
        }

        // POST: ScoringIndicatorsParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ScoringIndicatorsParameters item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.ScoringIndicatorsParameters.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var IndicatorID = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var ParameterID = db.ScoringParameters.Select(p => new { ID = p.ID, Name = p.InputParameterName }).OrderBy(p => p.Name).Distinct().ToList();

            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");
            ViewBag.Param = new SelectList(ParameterID, "ID", "Name");

            return View(item);
        }

        // GET: ScoringIndicatorsParameters/Edit/5
        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringIndicatorsParameters item = db.ScoringIndicatorsParameters.Where(p=> p.ID==   id).FirstOrDefault();

            if (item == null)
                return RedirectToAction("Create");




            var indicators = db.ScoringIndicators.Select(p => new { ID = p.ID, Name = p.IndicatorName }).OrderBy(p => p.Name).Distinct().ToList();
            var parameters = db.ScoringParameters.Select(p => new { ID = p.ID, Name = p.InputParameterName }).OrderBy(p => p.Name).Distinct().ToList();

            ViewBag.Ind = new SelectList(indicators, "ID", "Name");
            ViewBag.Param = new SelectList(parameters, "ID", "Name");

            return View(item);
        }

        // POST: ScoringIndicatorsParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringIndicatorsParameters item)
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
            var ParameterID = db.ScoringParameters.Select(p => new { ID = p.ID, Name = p.InputParameterName }).OrderBy(p => p.Name).Distinct().ToList();

            ViewBag.Ind = new SelectList(IndicatorID, "ID", "Name");
            ViewBag.Param = new SelectList(ParameterID, "ID", "Name");

            return View(item);
        }

        // GET: ScoringIndicatorsParameters/Delete/5

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
