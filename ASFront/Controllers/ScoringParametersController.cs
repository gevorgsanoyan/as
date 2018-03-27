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
using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringParametersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringParameters
        public ActionResult Index(int page = 1)
        {
            //var items = (from sp in db.ScoringParameters
            //             join si in db.ScoringIndicators on sp.indicatorID equals si.ID
            //             select new ScoringParametersViewModels() { ID = sp.ID, IndicatorName = si.IndicatorName, InputParameterName = sp.InputParameterName, InputParameterValue = sp.InputParameterValue }).ToList();

            var items = db.ScoringParameters.ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }



        // GET: ScoringParameters/Create
        public ActionResult Create()
        {


            ScoringParameters item = new ScoringParameters();


            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;



            string table = null; ;
            var SourceTable = CommonFunction.GetTablesNames();
            var SourceField = CommonFunction.GetColumnNames(table);

            ViewBag.SourceTable = new SelectList(SourceTable, "ID", "Name");
            ViewBag.SourceField = new SelectList(SourceField, "ID", "Name");


            return View(item);

        }

        // POST: ScoringParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScoringParameters item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;


                db.ScoringParameters.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string colum = item.SourceTable;
            var SourceTable = CommonFunction.GetTablesNames();
            var SourceField = CommonFunction.GetColumnNames(colum);

            ViewBag.SourceTable = new SelectList(SourceTable, "ID", "Name");
            ViewBag.SourceField = new SelectList(SourceField, "ID", "Name");
            return View(item);
        }

        // GET: ScoringParameters/Edit/5
        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            ScoringParameters item = db.ScoringParameters.Find(id);

            if (item == null)
                return RedirectToAction("Create");


            string colum = item.SourceTable;
            var SourceTable = CommonFunction.GetTablesNames();
            var SourceField = CommonFunction.GetColumnNames(colum);

            ViewBag.SourceTable = new SelectList(SourceTable, "ID", "Name");
            ViewBag.SourceField = new SelectList(SourceField, "ID", "Name");

            return View(item);
        }

        // POST: ScoringParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScoringParameters item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;


                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string colum = item.SourceTable;
            var SourceTable = CommonFunction.GetTablesNames();
            var SourceField = CommonFunction.GetColumnNames(colum);

            ViewBag.SourceTable = new SelectList(SourceTable, "ID", "Name");
            ViewBag.SourceField = new SelectList(SourceField, "ID", "Name");

            return View(item);
        }

        // GET: ScoringParameters/Delete/5
     
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
